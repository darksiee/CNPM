using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class BaoCaoController : Controller
{
    private readonly PharmacyDbContext _context;

    public BaoCaoController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    // 1. Thống kê doanh thu theo ngày, tuần, tháng, năm
    [HttpGet]
    public async Task<IActionResult> GetDoanhThu(string loaiThongKe, DateTime? tuNgay, DateTime? denNgay)
    {
        tuNgay = tuNgay.HasValue ? tuNgay.Value : DateTime.Now.AddDays(-30); // Default to 30 days ago
        denNgay = denNgay.HasValue ? denNgay.Value : DateTime.Now;           // Default to today

        var query = _context.TblPhieuThus
            .Where(pt => pt.DTgLap >= tuNgay && pt.DTgLap <= denNgay)
            .Join(_context.TblCtphieuThus,
                pt => pt.PkSMaPt,
                ct => ct.PkFkSMaPt,
                (pt, ct) => new { pt.DTgLap, ct.ISl, ct.PkFkSMaSp })
            .Join(_context.TblSanPhams,
                ct => ct.PkFkSMaSp,
                sp => sp.PkSMaSp,
                (ct, sp) => new { DTgLap = ct.DTgLap.Value, ThanhTien = ct.ISl * sp.FDonGiaBan });

        var result = await (loaiThongKe switch
        {
            "ngay" => query
                .GroupBy(x => x.DTgLap.Date)
                .Select(g => new { ThoiGian = g.Key, TongDoanhThu = g.Sum(x => x.ThanhTien) })
                .OrderBy(x => x.ThoiGian)
                .ToListAsync()
                .ContinueWith(t => t.Result.Select(x => new { ThoiGian = x.ThoiGian.ToString("yyyy-MM-dd"), x.TongDoanhThu }).ToList()),
            "tuan" => query
                .GroupBy(x => EF.Functions.DateFromParts(x.DTgLap.Year, 1, 1).AddDays(EF.Functions.DateDiffWeek(tuNgay.Value, x.DTgLap) * 7))
                .Select(g => new { ThoiGian = g.Key, TongDoanhThu = g.Sum(x => x.ThanhTien) })
                .OrderBy(x => x.ThoiGian)
                .ToListAsync()
                .ContinueWith(t => t.Result.Select(x => new { ThoiGian = x.ThoiGian.ToString("yyyy-MM-dd"), x.TongDoanhThu }).ToList()),
            "thang" => query
                .GroupBy(x => new { x.DTgLap.Year, x.DTgLap.Month })
                .Select(g => new { ThoiGian = new { g.Key.Year, g.Key.Month }, TongDoanhThu = g.Sum(x => x.ThanhTien) }) // Temporary object
                .OrderBy(x => x.ThoiGian.Year).ThenBy(x => x.ThoiGian.Month)
                .ToListAsync()
                .ContinueWith(t => t.Result.Select(x => new { ThoiGian = $"{x.ThoiGian.Month}/{x.ThoiGian.Year}", x.TongDoanhThu }).ToList()), // Format client-side
            "nam" => query
                .GroupBy(x => x.DTgLap.Year)
                .Select(g => new { ThoiGian = g.Key, TongDoanhThu = g.Sum(x => x.ThanhTien) })
                .OrderBy(x => x.ThoiGian)
                .ToListAsync()
                .ContinueWith(t => t.Result.Select(x => new { ThoiGian = x.ThoiGian.ToString(), x.TongDoanhThu }).ToList()),
            _ => query
                .GroupBy(x => x.DTgLap.Date)
                .Select(g => new { ThoiGian = g.Key, TongDoanhThu = g.Sum(x => x.ThanhTien) })
                .OrderBy(x => x.ThoiGian)
                .ToListAsync()
                .ContinueWith(t => t.Result.Select(x => new { ThoiGian = x.ThoiGian.ToString("yyyy-MM-dd"), x.TongDoanhThu }).ToList())
        });

        return Json(result);
    }
    // 2. Thống kê xuất kho theo thời gian
    [HttpGet]
    public async Task<IActionResult> GetXuatKho(DateTime? tuNgay, DateTime? denNgay)
    {
        tuNgay = tuNgay.HasValue ? tuNgay.Value : DateTime.Now.AddDays(-30);
        denNgay = denNgay.HasValue ? denNgay.Value : DateTime.Now;

        var xuatKho = await _context.TblPhieuXuatKhos
            .Where(px => px.DTgLap >= tuNgay && px.DTgLap <= denNgay)
            .Join(_context.TblCtphieuXuatKhos,
                px => px.PkSMaPx,
                ct => ct.PkFkSMaPx,
                (px, ct) => new { px.DTgLap, ct.ISlx, ct.PkFkSMaSp })
            .Join(_context.TblSanPhams,
                ct => ct.PkFkSMaSp,
                sp => sp.PkSMaSp,
                (ct, sp) => new { DTgLap = ct.DTgLap.Value, ct.ISlx, sp.STenSp }) // Unwrap DTgLap here
            .GroupBy(x => x.DTgLap.Date) // Use unwrapped DTgLap
            .Select(g => new
            {
                Ngay = g.Key,
                TongSoLuongXuat = g.Sum(x => x.ISlx),
                ChiTiet = g.Select(x => new { x.STenSp, x.ISlx }).ToList()
            })
            .OrderBy(x => x.Ngay)
            .ToListAsync();

        return Json(xuatKho);
    }

    [HttpGet]
    public async Task<IActionResult> GetTonKho()
    {
        var today = DateOnly.FromDateTime(DateTime.Now); // Chuyển DateTime thành DateOnly

        var tonKho = await _context.TblSanPhams
            .Select(sp => new
            {
                sp.PkSMaSp,
                sp.STenSp,
                sp.ISl,
                sp.SHanDung,
                TrangThai = sp.SHanDung.HasValue && sp.SHanDung.Value < today ? "Hết hạn" :
                            sp.ISl <= 10 ? "Sắp hết" : "Bình thường"
            })
            .OrderBy(sp => sp.STenSp)
            .ToListAsync();

        return Json(tonKho);
    }

    // 4. Thống kê khách hàng mua nhiều nhất
    [HttpGet]
    public async Task<IActionResult> GetKhachHangMuaNhieu()
    {
        var khachHang = await _context.TblKhachHangs
            .GroupJoin(_context.TblPhieuThus,
                kh => kh.PkSMaKh,
                pt => pt.FkSMaKh,
                (kh, pts) => new
                {
                    MaKH = kh.PkSMaKh,
                    TenKH = kh.STenKh,
                    SoDienThoai = kh.SSdt,
                    HoaDons = pts
                        .Select(pt => new
                        {
                            MaPT = pt.PkSMaPt,
                            NgayLap = pt.DTgLap,
                            HinhThucTT = pt.SHinhThucTt,
                            TongTien = pt.TblCtphieuThus
                                .Join(_context.TblSanPhams,
                                    ct => ct.PkFkSMaSp,
                                    sp => sp.PkSMaSp,
                                    (ct, sp) => ct.ISl * sp.FDonGiaBan)
                                .Sum(),
                            ChiTietHoaDon = pt.TblCtphieuThus
                                .Select(ct => new
                                {
                                    MaSP = ct.PkFkSMaSp,
                                    TenSP = ct.PkFkSMaSpNavigation.STenSp,
                                    SoLuong = ct.ISl,
                                    DonGia = ct.PkFkSMaSpNavigation.FDonGiaBan,
                                    ThanhTien = ct.ISl * ct.PkFkSMaSpNavigation.FDonGiaBan,
                                    // Xác định kê đơn dựa trên FkSMaLoai
                                    KeDon = ct.PkFkSMaSpNavigation.FkSMaLoai == "LSP002" // LSP002 là kê đơn, LSP001 là không kê đơn
                                }).ToList()
                        }).ToList()
                })
            .OrderBy(x => x.TenKH)
            .ToListAsync();

        return Json(khachHang);
    }
    // 5. Thống kê hiệu suất nhân viên
    [HttpGet]
    public async Task<IActionResult> GetHieuSuatNhanVien(DateTime? tuNgay, DateTime? denNgay)
    {
        tuNgay = tuNgay.HasValue ? tuNgay.Value : DateTime.Now.AddDays(-30);
        denNgay = denNgay.HasValue ? denNgay.Value : DateTime.Now;

        // Fetch all necessary data into memory first
        var nhanViens = await _context.TblNhanViens.ToListAsync();
        var phieuThus = await _context.TblPhieuThus
            .Where(pt => pt.DTgLap >= tuNgay && pt.DTgLap <= denNgay)
            .ToListAsync();
        var phieuXuatKhos = await _context.TblPhieuXuatKhos
            .Where(px => px.DTgLap >= tuNgay && px.DTgLap <= denNgay)
            .ToListAsync();
        var ctPhieuThus = await _context.TblCtphieuThus.ToListAsync();
        var sanPhams = await _context.TblSanPhams.ToListAsync();

        // Perform the GroupJoin and calculations client-side
        var nhanVien = nhanViens
            .GroupJoin(phieuThus,
                nv => nv.PkSMaNv,
                pt => pt.FkSMaNv,
                (nv, pts) => new { nv, pts })
            .GroupJoin(phieuXuatKhos,
                n => n.nv.PkSMaNv,
                px => px.FkSMaNv,
                (n, pxs) => new
                {
                    MaNV = n.nv.PkSMaNv,
                    TenNV = n.nv.SHoTen,
                    SoPhieuThu = n.pts.Count(),
                    SoPhieuXuat = pxs.Count(),
                    TongDoanhThu = n.pts
                        .Join(ctPhieuThus,
                            pt => pt.PkSMaPt,
                            ct => ct.PkFkSMaPt,
                            (pt, ct) => ct)
                        .Join(sanPhams,
                            ct => ct.PkFkSMaSp,
                            sp => sp.PkSMaSp,
                            (ct, sp) => ct.ISl * sp.FDonGiaBan)
                        .Sum()
                })
            .OrderByDescending(x => x.TongDoanhThu)
            .ToList();

        return Json(nhanVien);
    }
}
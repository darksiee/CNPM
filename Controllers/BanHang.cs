using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Authorize] // Yêu cầu đăng nhập
public class BanHangController : Controller
{
    private readonly PharmacyDbContext _context;

    public BanHangController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> SearchKhachHang(string query)
    {
        var khachHangs = await _context.TblKhachHangs
            .Where(kh => kh.PkSMaKh.Contains(query) || kh.STenKh.Contains(query))
            .Select(kh => new { kh.PkSMaKh, kh.STenKh, kh.SSdt })
            .Take(10) // Giới hạn 10 kết quả
            .ToListAsync();
        return Json(khachHangs);
    }

    [HttpGet]
    public async Task<IActionResult> SearchSanPham(string query)
    {
        var sanPhams = await _context.TblSanPhams
            .Where(sp => sp.PkSMaSp.Contains(query) || sp.STenSp.Contains(query))
            .Select(sp => new { sp.PkSMaSp, sp.STenSp, sp.ISl, sp.FDonGiaBan })
            .Take(10) // Giới hạn 10 kết quả
            .ToListAsync();
        return Json(sanPhams);
    }

    [HttpPost]
    public async Task<IActionResult> XuatKhoVaLapHoaDon(string maKH, Dictionary<string, int> sanPhamsXuat)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                string maPX = "PX" + DateTime.Now.ToString("yyyyMMddHHmmss"); // 16 ký tự
                string maPT = "PT" + DateTime.Now.ToString("yyyyMMddHHmmss"); // 16 ký tự

                if (!await _context.TblNhanViens.AnyAsync(nv => nv.PkSMaNv == "NV001"))
                {
                    return Json(new { success = false, message = "Nhân viên NV001 không tồn tại!" });
                }
                if (!await _context.TblKhachHangs.AnyAsync(kh => kh.PkSMaKh == maKH))
                {
                    return Json(new { success = false, message = "Khách hàng không tồn tại!" });
                }

                var phieuXuatKho = new TblPhieuXuatKho
                {
                    PkSMaPx = maPX,
                    DTgLap = DateTime.Now,
                    FkSMaNv = "NV001"
                };
                _context.TblPhieuXuatKhos.Add(phieuXuatKho);

                foreach (var item in sanPhamsXuat)
                {
                    string maSP = item.Key;
                    int soLuongXuat = item.Value;

                    var sanPham = await _context.TblSanPhams.FirstOrDefaultAsync(sp => sp.PkSMaSp == maSP);
                    if (sanPham == null || sanPham.ISl < soLuongXuat)
                    {
                        return Json(new { success = false, message = $"Sản phẩm {sanPham?.STenSp} không đủ tồn kho!" });
                    }

                    _context.TblCtphieuXuatKhos.Add(new TblCtphieuXuatKho
                    {
                        PkFkSMaPx = maPX,
                        PkFkSMaSp = maSP,
                        ISlyc = soLuongXuat,
                        ISlx = soLuongXuat,
                        SGhiChu = "Xuất bán"
                    });

                    sanPham.ISl -= soLuongXuat;
                }

                var phieuThu = new TblPhieuThu
                {
                    PkSMaPt = maPT,
                    DTgLap = DateTime.Now,
                    FkSMaNv = "NV001",
                    FkSMaKh = maKH,
                    SHinhThucTt = "Tiền mặt"
                };
                _context.TblPhieuThus.Add(phieuThu);

                foreach (var item in sanPhamsXuat)
                {
                    _context.TblCtphieuThus.Add(new TblCtphieuThu
                    {
                        PkFkSMaPt = maPT,
                        PkFkSMaSp = item.Key,
                        ISl = item.Value
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Json(new { success = true, maPT = maPT });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = ex.Message + " | Inner Exception: " + (ex.InnerException?.Message ?? "No inner exception") });
            }
        }
    }

    public async Task<IActionResult> HoaDon(string maPT)
    {
        if (string.IsNullOrEmpty(maPT))
        {
            return NotFound("Mã phiếu thu không hợp lệ!");
        }

        var phieuThu = await _context.TblPhieuThus
            .Include(pt => pt.TblCtphieuThus)
            .ThenInclude(ct => ct.PkFkSMaSpNavigation)
            .Include(pt => pt.FkSMaKhNavigation)
            .FirstOrDefaultAsync(pt => pt.PkSMaPt == maPT);

        if (phieuThu == null)
        {
            return NotFound("Không tìm thấy hóa đơn!");
        }

        return View(phieuThu);
    }
    [HttpGet]
    public async Task<IActionResult> ChiTietHoaDon(string maPT)
    {
        if (string.IsNullOrEmpty(maPT))
        {
            return NotFound("Mã phiếu thu không hợp lệ!");
        }

        var phieuThu = await _context.TblPhieuThus
            .Include(pt => pt.TblCtphieuThus)
            .ThenInclude(ct => ct.PkFkSMaSpNavigation) // Chi tiết sản phẩm
            .Include(pt => pt.FkSMaKhNavigation) // Thông tin khách hàng
            .Include(pt => pt.FkSMaNvNavigation) // Thông tin nhân viên lập phiếu
            .FirstOrDefaultAsync(pt => pt.PkSMaPt == maPT);

        if (phieuThu == null)
        {
            return NotFound("Không tìm thấy hóa đơn!");
        }

        return View(phieuThu); // Trả về view ChiTietHoaDon với dữ liệu phiếu thu
    }
}

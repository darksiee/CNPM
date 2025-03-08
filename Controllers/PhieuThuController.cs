using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class PhieuThuController : Controller
{
    private readonly PharmacyDbContext _context;

    public PhieuThuController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetPhieuThuList()
    {
        var phieuThus = await _context.TblPhieuThus
            .Include(pt => pt.FkSMaNvNavigation)
            .Include(pt => pt.FkSMaKhNavigation)
            .Select(pt => new
            {
                pt.PkSMaPt,
                pt.DTgLap,
                NhanVien = pt.FkSMaNvNavigation.SHoTen,
                KhachHang = pt.FkSMaKhNavigation.STenKh,
                pt.SHinhThucTt
            })
            .ToListAsync();
        return Json(phieuThus);
    }

    [HttpGet]
    public async Task<IActionResult> GetChiTietPhieuThu(string maPT)
    {
        var chiTiet = await _context.TblCtphieuThus
            .Where(ct => ct.PkFkSMaPt == maPT)
            .Include(ct => ct.PkFkSMaSpNavigation)
            .Select(ct => new
            {
                ct.PkFkSMaSp,
                SanPham = ct.PkFkSMaSpNavigation.STenSp,
                ct.ISl,
                DonGiaBan = ct.PkFkSMaSpNavigation.FDonGiaBan,
                ThanhTien = ct.ISl * ct.PkFkSMaSpNavigation.FDonGiaBan
            })
            .ToListAsync();

        var phieuThu = await _context.TblPhieuThus
            .Include(pt => pt.FkSMaKhNavigation)
            .FirstOrDefaultAsync(pt => pt.PkSMaPt == maPT);

        var result = new
        {
            MaPT = phieuThu.PkSMaPt,
            KhachHang = phieuThu.FkSMaKhNavigation.STenKh,
            NgayLap = phieuThu.DTgLap,
            HinhThucTT = phieuThu.SHinhThucTt,
            ChiTiet = chiTiet
        };

        return Json(result);
    }
}
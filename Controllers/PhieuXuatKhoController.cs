using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class PhieuXuatKhoController : Controller
{
    private readonly PharmacyDbContext _context;

    public PhieuXuatKhoController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetPhieuXuatKhoList()
    {
        var phieuXuatKhos = await _context.TblPhieuXuatKhos
            .Include(px => px.FkSMaNvNavigation)
            .Select(px => new
            {
                px.PkSMaPx,
                px.DTgLap,
                NhanVien = px.FkSMaNvNavigation.SHoTen
            })
            .ToListAsync();
        return Json(phieuXuatKhos);
    }

    [HttpGet]
    public async Task<IActionResult> GetChiTietPhieuXuatKho(string maPX)
    {
        var chiTiet = await _context.TblCtphieuXuatKhos
            .Where(ct => ct.PkFkSMaPx == maPX)
            .Include(ct => ct.PkFkSMaSpNavigation)
            .Select(ct => new
            {
                ct.PkFkSMaSp,
                SanPham = ct.PkFkSMaSpNavigation.STenSp,
                ct.ISlyc,
                ct.ISlx,
                ct.SGhiChu
            })
            .ToListAsync();

        var phieuXuatKho = await _context.TblPhieuXuatKhos
            .Include(px => px.FkSMaNvNavigation)
            .FirstOrDefaultAsync(px => px.PkSMaPx == maPX);

        var result = new
        {
            MaPX = phieuXuatKho.PkSMaPx,
            NhanVien = phieuXuatKho.FkSMaNvNavigation.SHoTen,
            NgayLap = phieuXuatKho.DTgLap,
            ChiTiet = chiTiet
        };

        return Json(result);
    }
}
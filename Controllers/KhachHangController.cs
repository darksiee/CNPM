using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class KhachHangController : Controller
{
    private readonly PharmacyDbContext _context;

    public KhachHangController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetKhachHangList()
    {
        var khachHangs = await _context.TblKhachHangs
            .Select(kh => new
            {
                kh.PkSMaKh,
                kh.STenKh,
                kh.SSdt
            })
            .ToListAsync();
        return Json(khachHangs);
    }

    [HttpPost]
    public async Task<IActionResult> AddKhachHang([FromBody] TblKhachHang khachHang)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        if (await _context.TblKhachHangs.AnyAsync(kh => kh.PkSMaKh == khachHang.PkSMaKh))
        {
            return Json(new { success = false, message = "Mã khách hàng đã tồn tại!" });
        }

        _context.TblKhachHangs.Add(khachHang);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Thêm khách hàng thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> EditKhachHang([FromBody] TblKhachHang khachHang)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        var existingKhachHang = await _context.TblKhachHangs
            .FirstOrDefaultAsync(kh => kh.PkSMaKh == khachHang.PkSMaKh);

        if (existingKhachHang == null)
        {
            return Json(new { success = false, message = "Không tìm thấy khách hàng!" });
        }

        existingKhachHang.STenKh = khachHang.STenKh;
        existingKhachHang.SSdt = khachHang.SSdt;

        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Sửa khách hàng thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteKhachHang(string maKH)
    {
        var khachHang = await _context.TblKhachHangs
            .FirstOrDefaultAsync(kh => kh.PkSMaKh == maKH);

        if (khachHang == null)
        {
            return Json(new { success = false, message = "Không tìm thấy khách hàng!" });
        }

        _context.TblKhachHangs.Remove(khachHang);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Xóa khách hàng thành công!" });
    }
}
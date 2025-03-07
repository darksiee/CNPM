using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class ChucVuController : Controller
{
    private readonly PharmacyDbContext _context;

    public ChucVuController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetChucVuList()
    {
        var chucVus = await _context.TblChucVus
            .Select(cv => new { cv.PkSMaCv, cv.STenCv })
            .ToListAsync();
        return Json(chucVus);
    }

    [HttpPost]
    public async Task<IActionResult> AddChucVu([FromBody] TblChucVu chucVu)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        if (await _context.TblChucVus.AnyAsync(cv => cv.PkSMaCv == chucVu.PkSMaCv))
        {
            return Json(new { success = false, message = "Mã chức vụ đã tồn tại!" });
        }

        _context.TblChucVus.Add(chucVu);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Thêm chức vụ thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> EditChucVu([FromBody] TblChucVu chucVu)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        var existingChucVu = await _context.TblChucVus
            .FirstOrDefaultAsync(cv => cv.PkSMaCv == chucVu.PkSMaCv);

        if (existingChucVu == null)
        {
            return Json(new { success = false, message = "Không tìm thấy chức vụ!" });
        }

        existingChucVu.STenCv = chucVu.STenCv;
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Sửa chức vụ thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteChucVu(string maCV)
    {
        var chucVu = await _context.TblChucVus
            .FirstOrDefaultAsync(cv => cv.PkSMaCv == maCV);

        if (chucVu == null)
        {
            return Json(new { success = false, message = "Không tìm thấy chức vụ!" });
        }

        _context.TblChucVus.Remove(chucVu);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Xóa chức vụ thành công!" });
    }
}
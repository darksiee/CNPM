using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class TaiKhoanController : Controller
{
    private readonly PharmacyDbContext _context;

    public TaiKhoanController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetTaiKhoanList()
    {
        var taiKhoans = await _context.TblTaiKhoans
            .Include(tk => tk.FkSMaQuyenNavigation)
            .Select(tk => new
            {
                tk.PkSMaTk,
                tk.STenTk,
                tk.SMk,
                Quyen = tk.FkSMaQuyenNavigation.STenQuyen
            })
            .ToListAsync();
        return Json(taiKhoans);
    }

    [HttpPost]
    public async Task<IActionResult> AddTaiKhoan([FromBody] TblTaiKhoan taiKhoan)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        if (await _context.TblTaiKhoans.AnyAsync(tk => tk.PkSMaTk == taiKhoan.PkSMaTk))
        {
            return Json(new { success = false, message = "Mã tài khoản đã tồn tại!" });
        }

        _context.TblTaiKhoans.Add(taiKhoan);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Thêm tài khoản thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> EditTaiKhoan([FromBody] TblTaiKhoan taiKhoan)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        var existingTaiKhoan = await _context.TblTaiKhoans
            .FirstOrDefaultAsync(tk => tk.PkSMaTk == taiKhoan.PkSMaTk);

        if (existingTaiKhoan == null)
        {
            return Json(new { success = false, message = "Không tìm thấy tài khoản!" });
        }

        existingTaiKhoan.STenTk = taiKhoan.STenTk;
        existingTaiKhoan.SMk = taiKhoan.SMk;
        existingTaiKhoan.FkSMaQuyen = taiKhoan.FkSMaQuyen;
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Sửa tài khoản thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTaiKhoan(string maTK)
    {
        var taiKhoan = await _context.TblTaiKhoans
            .FirstOrDefaultAsync(tk => tk.PkSMaTk == maTK);

        if (taiKhoan == null)
        {
            return Json(new { success = false, message = "Không tìm thấy tài khoản!" });
        }

        _context.TblTaiKhoans.Remove(taiKhoan);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Xóa tài khoản thành công!" });
    }

    [HttpGet]
    public async Task<IActionResult> GetQuyenDropdown()
    {
        var quyens = await _context.TblQuyens
            .Select(q => new { q.PkSMaQuyen, q.STenQuyen })
            .ToListAsync();
        return Json(quyens);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class QuyenController : Controller
{
    private readonly PharmacyDbContext _context;

    public QuyenController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetQuyenList()
    {
        var quyens = await _context.TblQuyens
            .Select(q => new { q.PkSMaQuyen, q.STenQuyen })
            .ToListAsync();
        return Json(quyens);
    }

    [HttpPost]
    public async Task<IActionResult> AddQuyen([FromBody] TblQuyen quyen)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        if (await _context.TblQuyens.AnyAsync(q => q.PkSMaQuyen == quyen.PkSMaQuyen))
        {
            return Json(new { success = false, message = "Mã quyền đã tồn tại!" });
        }

        _context.TblQuyens.Add(quyen);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Thêm quyền thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> EditQuyen([FromBody] TblQuyen quyen)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        var existingQuyen = await _context.TblQuyens
            .FirstOrDefaultAsync(q => q.PkSMaQuyen == quyen.PkSMaQuyen);

        if (existingQuyen == null)
        {
            return Json(new { success = false, message = "Không tìm thấy quyền!" });
        }

        existingQuyen.STenQuyen = quyen.STenQuyen;
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Sửa quyền thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteQuyen(string maQuyen)
    {
        var quyen = await _context.TblQuyens
            .FirstOrDefaultAsync(q => q.PkSMaQuyen == maQuyen);

        if (quyen == null)
        {
            return Json(new { success = false, message = "Không tìm thấy quyền!" });
        }

        _context.TblQuyens.Remove(quyen);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Xóa quyền thành công!" });
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize] 
public class NhaCungCapController : Controller
{
    private readonly PharmacyDbContext _context;

    public NhaCungCapController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetNhaCungCapList()
    {
        var nhaCungCaps = await _context.TblNhaCungCaps
            .Select(ncc => new
            {
                ncc.PkSMaNcc,
                ncc.STenNcc,
                ncc.SDiaChi,
                ncc.SSdt,
                ncc.SSoTk
            })
            .ToListAsync();

        return Json(nhaCungCaps);
    }

    [HttpPost]
    public async Task<IActionResult> AddNhaCungCap([FromBody] TblNhaCungCap nhaCungCap)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        if (await _context.TblNhaCungCaps.AnyAsync(ncc => ncc.PkSMaNcc == nhaCungCap.PkSMaNcc))
        {
            return Json(new { success = false, message = "Mã nhà cung cấp đã tồn tại!" });
        }

        _context.TblNhaCungCaps.Add(nhaCungCap);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Thêm nhà cung cấp thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> EditNhaCungCap([FromBody] TblNhaCungCap nhaCungCap)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        var existingNhaCungCap = await _context.TblNhaCungCaps
            .FirstOrDefaultAsync(ncc => ncc.PkSMaNcc == nhaCungCap.PkSMaNcc);

        if (existingNhaCungCap == null)
        {
            return Json(new { success = false, message = "Không tìm thấy nhà cung cấp!" });
        }

        existingNhaCungCap.STenNcc = nhaCungCap.STenNcc;
        existingNhaCungCap.SDiaChi = nhaCungCap.SDiaChi;
        existingNhaCungCap.SSdt = nhaCungCap.SSdt;
        existingNhaCungCap.SSoTk = nhaCungCap.SSoTk;

        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Sửa nhà cung cấp thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteNhaCungCap(string maNCC)
    {
        var nhaCungCap = await _context.TblNhaCungCaps
            .FirstOrDefaultAsync(ncc => ncc.PkSMaNcc == maNCC);

        if (nhaCungCap == null)
        {
            return Json(new { success = false, message = "Không tìm thấy nhà cung cấp!" });
        }

        _context.TblNhaCungCaps.Remove(nhaCungCap);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Xóa nhà cung cấp thành công!" });
    }
}
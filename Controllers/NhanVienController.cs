using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize] 
public class NhanVienController : Controller
{
    private readonly PharmacyDbContext _context;

    public NhanVienController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetNhanVienList()
    {
        var nhanViens = await _context.TblNhanViens
            .Include(nv => nv.FkSMaTkNavigation)
            .Include(nv => nv.FkSMaCvNavigation)
            .Select(nv => new
            {
                nv.PkSMaNv,
                nv.SHoTen,
                nv.DNgaySinh,
                nv.SCccd,
                nv.SSdt,
                nv.DNgayVaoLam,
                TaiKhoan = nv.FkSMaTkNavigation.STenTk,
                ChucVu = nv.FkSMaCvNavigation.STenCv,
                nv.BTrangThai
            })
            .ToListAsync();

        return Json(nhanViens);
    }

    [HttpPost]
    public async Task<IActionResult> AddNhanVien([FromBody] TblNhanVien nhanVien)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        if (await _context.TblNhanViens.AnyAsync(nv => nv.PkSMaNv == nhanVien.PkSMaNv))
        {
            return Json(new { success = false, message = "Mã nhân viên đã tồn tại!" });
        }

        _context.TblNhanViens.Add(nhanVien);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Thêm nhân viên thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> EditNhanVien([FromBody] TblNhanVien nhanVien)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        var existingNhanVien = await _context.TblNhanViens
            .FirstOrDefaultAsync(nv => nv.PkSMaNv == nhanVien.PkSMaNv);

        if (existingNhanVien == null)
        {
            return Json(new { success = false, message = "Không tìm thấy nhân viên!" });
        }

        existingNhanVien.SHoTen = nhanVien.SHoTen;
        existingNhanVien.DNgaySinh = nhanVien.DNgaySinh;
        existingNhanVien.SCccd = nhanVien.SCccd;
        existingNhanVien.SSdt = nhanVien.SSdt;
        existingNhanVien.DNgayVaoLam = nhanVien.DNgayVaoLam;
        existingNhanVien.FkSMaTk = nhanVien.FkSMaTk;
        existingNhanVien.FkSMaCv = nhanVien.FkSMaCv;
        existingNhanVien.BTrangThai = nhanVien.BTrangThai;

        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Sửa nhân viên thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteNhanVien(string maNV)
    {
        var nhanVien = await _context.TblNhanViens
            .FirstOrDefaultAsync(nv => nv.PkSMaNv == maNV);

        if (nhanVien == null)
        {
            return Json(new { success = false, message = "Không tìm thấy nhân viên!" });
        }

        _context.TblNhanViens.Remove(nhanVien);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Xóa nhân viên thành công!" });
    }

    [HttpGet]
    public async Task<IActionResult> GetDropdownData()
    {
        var taiKhoans = await _context.TblTaiKhoans.Select(tk => new { tk.PkSMaTk, tk.STenTk }).ToListAsync();
        var chucVus = await _context.TblChucVus.Select(cv => new { cv.PkSMaCv, cv.STenCv }).ToListAsync();
        return Json(new { taiKhoans, chucVus });
    }
}
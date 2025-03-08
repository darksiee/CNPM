using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
public class SanPhamController : Controller
{
    private readonly PharmacyDbContext _context;

    public SanPhamController(PharmacyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetSanPhamList()
    {
        var sanPhams = await _context.TblSanPhams
            .Include(sp => sp.FkSMaLoaiNavigation)
            .Include(sp => sp.FkSMaNccNavigation)
            .Select(sp => new
            {
                sp.PkSMaSp,
                sp.STenSp,
                sp.SDonViTinh,
                sp.SHanDung,
                sp.ISl,
                sp.FDonGiaBan,
                LoaiSanPham = sp.FkSMaLoaiNavigation.STenLoai,
                NhaCungCap = sp.FkSMaNccNavigation.STenNcc
            })
            .ToListAsync();
        return Json(sanPhams);
    }

    [HttpPost]
    public async Task<IActionResult> AddSanPham([FromBody] TblSanPham sanPham)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        if (await _context.TblSanPhams.AnyAsync(sp => sp.PkSMaSp == sanPham.PkSMaSp))
        {
            return Json(new { success = false, message = "Mã sản phẩm đã tồn tại!" });
        }

        _context.TblSanPhams.Add(sanPham);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Thêm sản phẩm thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> EditSanPham([FromBody] TblSanPham sanPham)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
        }

        var existingSanPham = await _context.TblSanPhams
            .FirstOrDefaultAsync(sp => sp.PkSMaSp == sanPham.PkSMaSp);

        if (existingSanPham == null)
        {
            return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });
        }

        existingSanPham.STenSp = sanPham.STenSp;
        existingSanPham.SDonViTinh = sanPham.SDonViTinh;
        existingSanPham.SHanDung = sanPham.SHanDung;
        existingSanPham.ISl = sanPham.ISl;
        existingSanPham.FDonGiaBan = sanPham.FDonGiaBan;
        existingSanPham.FkSMaLoai = sanPham.FkSMaLoai;
        existingSanPham.FkSMaNcc = sanPham.FkSMaNcc;

        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Sửa sản phẩm thành công!" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSanPham(string maSP)
    {
        var sanPham = await _context.TblSanPhams
            .FirstOrDefaultAsync(sp => sp.PkSMaSp == maSP);

        if (sanPham == null)
        {
            return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });
        }

        _context.TblSanPhams.Remove(sanPham);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Xóa sản phẩm thành công!" });
    }

    [HttpGet]
    public async Task<IActionResult> GetLoaiSanPhamDropdown()
    {
        var loaiSanPhams = await _context.TblLoaiSanPhams
            .Select(l => new { l.PkSMaLoai, l.STenLoai })
            .ToListAsync();
        return Json(loaiSanPhams);
    }

    [HttpGet]
    public async Task<IActionResult> GetNhaCungCapDropdown()
    {
        var nhaCungCaps = await _context.TblNhaCungCaps
            .Select(n => new { n.PkSMaNcc, n.STenNcc })
            .ToListAsync();
        return Json(nhaCungCaps);
    }
}
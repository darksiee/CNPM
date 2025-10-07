using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CNPM.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly PharmacyDbContext _context;

        public ProfileController(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var maTK = User.FindFirst("MaTK")?.Value;
            if (string.IsNullOrEmpty(maTK))
            {
                return RedirectToAction("Login", "Account");
            }

            var taiKhoan = await _context.TblTaiKhoans
                .Include(tk => tk.FkSMaQuyenNavigation)
                .Include(tk => tk.TblNhanViens)
                    .ThenInclude(nv => nv.FkSMaCvNavigation)
                .FirstOrDefaultAsync(tk => tk.PkSMaTk == maTK);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var maTK = User.FindFirst("MaTK")?.Value;
            if (string.IsNullOrEmpty(maTK))
            {
                return RedirectToAction("Login", "Account");
            }

            var taiKhoan = await _context.TblTaiKhoans
                .Include(tk => tk.TblNhanViens)
                .FirstOrDefaultAsync(tk => tk.PkSMaTk == maTK);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string currentPassword, string newPassword, string confirmPassword)
        {
            var maTK = User.FindFirst("MaTK")?.Value;
            if (string.IsNullOrEmpty(maTK))
            {
                return RedirectToAction("Login", "Account");
            }

            var taiKhoan = await _context.TblTaiKhoans
                .FirstOrDefaultAsync(tk => tk.PkSMaTk == maTK);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            // Validate current password
            if (taiKhoan.SMk != currentPassword)
            {
                ViewBag.ErrorMessage = "Mật khẩu hiện tại không đúng!";
                return View(taiKhoan);
            }

            // Validate new password
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 3)
            {
                ViewBag.ErrorMessage = "Mật khẩu mới phải có ít nhất 3 ký tự!";
                return View(taiKhoan);
            }

            // Validate password confirmation
            if (newPassword != confirmPassword)
            {
                ViewBag.ErrorMessage = "Mật khẩu mới và xác nhận không khớp!";
                return View(taiKhoan);
            }

            // Update password
            taiKhoan.SMk = newPassword;
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "Đổi mật khẩu thành công!";
            return View(taiKhoan);
        }
    }
}

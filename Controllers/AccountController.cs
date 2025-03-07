using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CNPM.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CNPM.Controllers
{
    public class AccountController : Controller
    {
        private readonly PharmacyDbContext _context;

        public AccountController(PharmacyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var taiKhoan = _context.TblTaiKhoans
                .Include(tk => tk.FkSMaQuyenNavigation) // Load quyền
                .FirstOrDefault(tk => tk.STenTk == username && tk.SMk == password);

            if (taiKhoan == null)
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }

            // Lấy mã quyền thay vì tên quyền
            var maQuyen = taiKhoan.FkSMaQuyenNavigation?.PkSMaQuyen ?? "User";

            Console.WriteLine($"User Role (MaQuyen): {maQuyen}"); // Debug kiểm tra mã quyền

            // Tạo claims cho người dùng
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, taiKhoan.STenTk),
        new Claim("MaTK", taiKhoan.PkSMaTk),
        new Claim(ClaimTypes.Role, maQuyen) // Lưu MÃ quyền thay vì tên quyền
    };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            // Đăng nhập người dùng
            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToAction("Index", "BanHang"); // Chuyển hướng đến BanHang sau khi đăng nhập
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth"); 
            return RedirectToAction("Login", "Account"); 
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
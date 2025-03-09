using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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
            var taiKhoan = await _context.TblTaiKhoans
                .Include(tk => tk.FkSMaQuyenNavigation)
                .FirstOrDefaultAsync(tk => tk.STenTk == username && tk.SMk == password);

            if (taiKhoan == null)
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }

            // Thêm các Claim, gán Role là mã quyền (FK_sMaQuyen)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, taiKhoan.STenTk),
                new Claim("MaTK", taiKhoan.PkSMaTk),
                new Claim(ClaimTypes.Role, taiKhoan.FkSMaQuyen ?? "Unknown"), // Role là mã quyền (Q002)
                new Claim("FkSMaQuyen", taiKhoan.FkSMaQuyen ?? "Unknown") // Claim riêng cho mã quyền nếu cần
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string tenTK, string matKhau)
        {
            if (string.IsNullOrEmpty(tenTK) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.ErrorMessage = "Vui lòng điền đầy đủ tên tài khoản và mật khẩu!";
                return View();
            }

            if (await _context.TblTaiKhoans.AnyAsync(tk => tk.STenTk == tenTK))
            {
                ViewBag.ErrorMessage = "Tên tài khoản đã được sử dụng!";
                return View();
            }

            // Tự sinh mã tài khoản (TK001, TK002, ...)
            string maTK;
            int count = 1;
            do
            {
                maTK = $"TK{count:000}";
                count++;
            } while (await _context.TblTaiKhoans.AnyAsync(tk => tk.PkSMaTk == maTK));

            var taiKhoan = new TblTaiKhoan
            {
                PkSMaTk = maTK,
                STenTk = tenTK,
                SMk = matKhau,
                FkSMaQuyen = null // Gán quyền mặc định Q002 cho tài khoản mới
            };

            _context.TblTaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đăng ký tài khoản thành công! Vui lòng đăng nhập.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
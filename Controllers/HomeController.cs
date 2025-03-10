using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CNPM.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace CNPM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PharmacyDbContext _context;

        public HomeController(ILogger<HomeController> logger, PharmacyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string searchString, int? page, string filterType = null)
        {
            int pageSize = 12; // 3 sản phẩm x 4 hàng = 12 sản phẩm mỗi trang
            int pageNumber = page ?? 1;

            var sanPhams = _context.TblSanPhams.AsNoTracking();

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(sp => sp.STenSp.Contains(searchString));
            }

            // Lọc theo loại thuốc
            if (!string.IsNullOrEmpty(filterType))
            {
                if (filterType == "NoPrescription") // Thuốc không kê đơn
                {
                    sanPhams = sanPhams.Where(sp => sp.FkSMaLoai == "LSP001");
                }
                else if (filterType == "Prescription") // Thuốc kê đơn
                {
                    sanPhams = sanPhams.Where(sp => sp.FkSMaLoai == "LSP002");
                }
            }

            var pagedSanPhams = sanPhams
                .OrderBy(sp => sp.PkSMaSp)
                .ToPagedList(pageNumber, pageSize);

            ViewBag.SearchString = searchString;
            ViewBag.FilterType = filterType; // Lưu filterType để giữ trạng thái giao diện
            return View(pagedSanPhams);
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = _context.TblSanPhams
                .Include(sp => sp.FkSMaNccNavigation) // Lấy thông tin nhà cung cấp
                .FirstOrDefault(sp => sp.PkSMaSp == id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
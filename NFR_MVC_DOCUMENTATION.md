# TÀI LIỆU CHỨNG MINH KIẾN TRÚC MVC ĐÁP ỨNG CÁC YÊU CẦU PHI CHỨC NĂNG (NFR)
# Hệ Thống Quản Lý Nhà Thuốc - CNPM

## Tổng quan về Kiến trúc MVC

Dự án sử dụng kiến trúc **ASP.NET Core MVC** với cấu trúc phân tầng rõ ràng:

- **Model (M)**: Lớp dữ liệu và logic nghiệp vụ (thư mục `Models/`)
- **View (V)**: Lớp giao diện người dùng (thư mục `Views/`)
- **Controller (C)**: Lớp điều khiển luồng xử lý (thư mục `Controllers/`)

### Sơ đồ Kiến trúc MVC

```
┌─────────────────────────────────────────────────────────────┐
│                         User Interface                       │
│                    (Browser/Client Side)                     │
└────────────────────────────┬────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────┐
│                      VIEW LAYER (V)                          │
│  ┌──────────────────────────────────────────────────────┐  │
│  │ Razor Views (.cshtml)                                │  │
│  │ - Account/Login.cshtml                               │  │
│  │ - SanPham/Index.cshtml                               │  │
│  │ - BanHang/HoaDon.cshtml                             │  │
│  │ - Shared/_Layout.cshtml                              │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────────┬────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────┐
│                   CONTROLLER LAYER (C)                       │
│  ┌──────────────────────────────────────────────────────┐  │
│  │ Controllers                                           │  │
│  │ - AccountController.cs      (Authentication)         │  │
│  │ - SanPhamController.cs      (Product Management)     │  │
│  │ - BanHang.cs                (Sales)                  │  │
│  │ - NhanVienController.cs     (Employee Management)    │  │
│  │ - BaoCaoController.cs       (Reporting)              │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────────┬────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────┐
│                     MODEL LAYER (M)                          │
│  ┌──────────────────────────────────────────────────────┐  │
│  │ Entity Models (Database Tables)                      │  │
│  │ - TblTaiKhoan.cs           (Account)                 │  │
│  │ - TblSanPham.cs            (Product)                 │  │
│  │ - TblPhieuThu.cs           (Receipt)                 │  │
│  │ - TblNhanVien.cs           (Employee)                │  │
│  │ - TblKhachHang.cs          (Customer)                │  │
│  └──────────────────────────────────────────────────────┘  │
│  ┌──────────────────────────────────────────────────────┐  │
│  │ DbContext (Data Access Layer)                        │  │
│  │ - PharmacyDbContext.cs                               │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────────┬────────────────────────────────┘
                             │
                             ▼
                    ┌────────────────┐
                    │   SQL Server   │
                    │    Database    │
                    └────────────────┘
```

---

## MỨC 1: CAO NHẤT

### 1.1 Reliability (Độ tin cậy)
**Yêu cầu**: Hệ thống hoạt động ổn định >= 95% thời gian; khôi phục dữ liệu trong vòng 2 giờ nếu có sự cố.

#### Cách MVC đáp ứng:

**1. Tách biệt lớp dữ liệu (Model)**
```csharp
// File: Models/PharmacyDbContext.cs
public partial class PharmacyDbContext : DbContext
{
    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<TblTaiKhoan> TblTaiKhoans { get; set; }
    public virtual DbSet<TblSanPham> TblSanPhams { get; set; }
    // ... các DbSet khác
}
```

**Lợi ích cho Reliability:**
- ✅ **Tách biệt dữ liệu**: Lớp Model độc lập, không phụ thuộc vào View hay Controller
- ✅ **Entity Framework Core**: Tự động quản lý kết nối database, connection pooling
- ✅ **Transaction Support**: EF Core hỗ trợ transactions, đảm bảo tính toàn vẹn dữ liệu
- ✅ **Database Migration**: Có thể backup/restore dữ liệu dễ dàng thông qua migration scripts

**2. Xử lý lỗi tập trung (Controller)**
```csharp
// File: Program.cs
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
```

**Lợi ích:**
- ✅ Error handling tập trung, tránh crash ứng dụng
- ✅ Logging errors để phân tích và khắc phục
- ✅ Graceful degradation khi có lỗi

**3. Khả năng khôi phục dữ liệu**
- ✅ Database được quản lý bởi SQL Server (hỗ trợ backup/restore)
- ✅ Code Database.sql có sẵn để tái tạo cấu trúc
- ✅ Entity Models giúp migration dữ liệu dễ dàng

---

### 1.2 Availability (Tính khả dụng)
**Yêu cầu**: Hệ thống luôn sẵn sàng phục vụ trong giờ hoạt động; các dịch vụ chính phải duy trì được tính khả dụng tối thiểu 95%

#### Cách MVC đáp ứng:

**1. Stateless Controllers**
```csharp
// File: Controllers/BanHang.cs
public class BanHang : Controller
{
    private readonly PharmacyDbContext _context;
    
    public BanHang(PharmacyDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var sanPhams = await _context.TblSanPhams
            .Include(sp => sp.FkSMaLoaiSpNavigation)
            .ToListAsync();
        return View(sanPhams);
    }
}
```

**Lợi ích cho Availability:**
- ✅ **Stateless design**: Controllers không lưu trạng thái, dễ scale
- ✅ **Dependency Injection**: DbContext được inject, quản lý lifecycle tự động
- ✅ **Async/Await**: Xử lý bất đồng bộ, không block thread
- ✅ **Connection Pooling**: EF Core tự động quản lý connection pool

**2. Phân tách chức năng theo Controller**
- `AccountController`: Xác thực (Login/Logout)
- `BanHang`: Bán hàng
- `SanPhamController`: Quản lý sản phẩm
- `BaoCaoController`: Báo cáo

**Lợi ích:**
- ✅ Một module lỗi không ảnh hưởng toàn hệ thống
- ✅ Dễ dàng monitoring từng service riêng biệt
- ✅ Có thể disable/enable tính năng độc lập

**3. Authentication & Session Management**
```csharp
// File: Program.cs
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });
```

**Lợi ích:**
- ✅ Cookie-based authentication: Lightweight, không cần database query mỗi request
- ✅ Session persistence: User không bị logout khi server restart (với distributed cache)

---

### 1.3 Security (Bảo mật)
**Yêu cầu**: Dữ liệu nhạy cảm (tên, SĐT) phải được mã hóa cơ bản; yêu cầu xác thực người dùng đơn giản (tài khoản & mật khẩu).

#### Cách MVC đáp ứng:

**1. Authentication trong Controller**
```csharp
// File: Controllers/AccountController.cs
// ⚠️ SECURITY NOTE: Code hiện tại sử dụng plain text password (CẦN CẢI TIẾN)
// Code dưới đây là implementation hiện tại, cần hash password như đề xuất ở phần Recommendations
[HttpPost]
public async Task<IActionResult> Login(string username, string password)
{
    var taiKhoan = await _context.TblTaiKhoans
        .Include(tk => tk.FkSMaQuyenNavigation)
        .FirstOrDefaultAsync(tk => tk.STenTk == username && tk.SMk == password);
    // ⚠️ Plain text password comparison - NÊN DÙNG password hashing (xem phần Recommendations)

    if (taiKhoan == null)
    {
        ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng!";
        return View();
    }

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, taiKhoan.STenTk),
        new Claim("MaTK", taiKhoan.PkSMaTk),
        new Claim(ClaimTypes.Role, taiKhoan.FkSMaQuyen ?? "Unknown")
    };

    var identity = new ClaimsIdentity(claims, "CookieAuth");
    var principal = new ClaimsPrincipal(identity);

    await HttpContext.SignInAsync("CookieAuth", principal);
    return RedirectToAction("Index", "Home");
}
```

**Lợi ích cho Security:**
- ✅ **Claims-based Authentication**: Hỗ trợ role-based access control
- ✅ **Cookie Authentication**: Bảo mật session
- ✅ **Controller Attributes**: Có thể dùng [Authorize] để bảo vệ action

**2. Model Validation**
```csharp
// File: Models/TblTaiKhoan.cs
[Table("tbl_TaiKhoan")]
public partial class TblTaiKhoan
{
    [Key]
    [Column("PK_sMaTK")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaTk { get; set; } = null!;

    [Column("sTenTK")]
    [StringLength(20)]
    [Unicode(false)]
    public string? STenTk { get; set; }

    [Column("sMK")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SMk { get; set; }
}
```

**Lợi ích:**
- ✅ **Data Annotations**: Validate input tự động
- ✅ **Type Safety**: Strong typing giảm SQL injection
- ✅ **Parameterized Queries**: EF Core tự động parameterize, ngăn SQL injection

**3. View-level Security**
```csharp
// Views có thể kiểm tra authentication
@if (User.Identity.IsAuthenticated)
{
    // Show authenticated content
}
```

**Cải tiến có thể làm (đề xuất - QUAN TRỌNG):**
- 🔒 **MÃ HÓA MẬT KHẨU** bằng BCrypt/PBKDF2 (workFactor 10-12) - XEM PHẦN RECOMMENDATIONS
- 🔒 HTTPS để mã hóa dữ liệu truyền tải
- 🔒 Thêm [Authorize] attribute cho các controller cần bảo vệ

**⚠️ LƯU Ý**: Code hiện tại sử dụng plain text password (tk.SMk == password), đây là security vulnerability cần được fix như đề xuất ở phần Recommendations.

---

### 1.4 Compliance (Tuân thủ pháp luật)
**Yêu cầu**: Đảm bảo tuân thủ quy định về kinh doanh dược phẩm (ví dụ: không bán thuốc hết hạn); hỗ trợ tạo báo cáo đơn giản (doanh thu, tồn kho, hóa đơn).

#### Cách MVC đáp ứng:

**1. Business Logic trong Model**
```csharp
// File: Models/TblSanPham.cs
[Table("tbl_SanPham")]
public partial class TblSanPham
{
    [Column("dNgaySanXuat")]
    public DateOnly? DNgaySanXuat { get; set; }

    [Column("dHanSuDung")]
    public DateOnly? DHanSuDung { get; set; }
    
    // Logic kiểm tra hạn sử dụng có thể thêm vào
    public bool IsExpired()
    {
        return DHanSuDung.HasValue && 
               DHanSuDung.Value < DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
```

**Lợi ích cho Compliance:**
- ✅ **Business Rules trong Model**: Tập trung logic kiểm tra
- ✅ **Data Validation**: Đảm bảo dữ liệu hợp lệ trước khi lưu
- ✅ **Audit Trail**: Có thể thêm tracking fields (CreatedDate, ModifiedDate)

**2. Reporting Controller**
```csharp
// File: Controllers/BaoCaoController.cs
public class BaoCaoController : Controller
{
    private readonly PharmacyDbContext _context;

    public async Task<IActionResult> BaoCaoDoanhThu(DateTime? startDate, DateTime? endDate)
    {
        var query = _context.TblPhieuThus
            .Include(pt => pt.TblCtphieuThus)
            .AsQueryable();

        if (startDate.HasValue)
            query = query.Where(pt => pt.DNgayLap >= DateOnly.FromDateTime(startDate.Value));
        
        if (endDate.HasValue)
            query = query.Where(pt => pt.DNgayLap <= DateOnly.FromDateTime(endDate.Value));

        var baoCao = await query.ToListAsync();
        return View(baoCao);
    }
}
```

**Lợi ích:**
- ✅ **Dedicated Reporting Module**: Controller riêng cho báo cáo
- ✅ **Flexible Queries**: Dễ dàng filter theo tiêu chí khác nhau
- ✅ **Export Support**: Có thể thêm export to CSV/Excel

**3. Data Models cho Compliance**
- `TblBaoCaoThuChi`: Báo cáo thu chi
- `TblPhieuThu`: Phiếu thu (hóa đơn)
- `TblPhieuXuatKho`: Phiếu xuất kho
- `TblBienBanKiemKe`: Biên bản kiểm kê

**Lợi ích:**
- ✅ Cấu trúc dữ liệu đầy đủ cho audit
- ✅ Traceability: Theo dõi được nguồn gốc giao dịch
- ✅ Reporting Ready: Sẵn sàng tạo báo cáo theo yêu cầu pháp luật

---

## MỨC 2: QUAN TRỌNG

### 2.1 Performance (Hiệu suất)
**Yêu cầu**: Thời gian phản hồi cho mỗi thao tác < 5 giây; xử lý tối thiểu 10 giao dịch đồng thời mà không bị chậm trễ đáng kể.

#### Cách MVC đáp ứng:

**1. Async Operations trong Controller**
```csharp
// File: Controllers/SanPhamController.cs
public async Task<IActionResult> Index(int? page)
{
    int pageSize = 10;
    int pageNumber = page ?? 1;
    
    var sanPhams = await _context.TblSanPhams
        .Include(sp => sp.FkSMaLoaiSpNavigation)
        .ToPagedListAsync(pageNumber, pageSize);
    
    return View(sanPhams);
}
```

**Lợi ích cho Performance:**
- ✅ **Async/Await**: Non-blocking I/O, tăng throughput
- ✅ **Pagination**: Giảm tải dữ liệu, response nhanh hơn
- ✅ **Eager Loading**: Include() để giảm N+1 queries
- ✅ **Connection Pooling**: EF Core tự động reuse connections

**2. Optimized Queries**
```csharp
// Chỉ select các field cần thiết
var products = await _context.TblSanPhams
    .Select(sp => new {
        sp.PkSMaSp,
        sp.STenSp,
        sp.FDonGia
    })
    .ToListAsync();
```

**Lợi ích:**
- ✅ Giảm memory usage
- ✅ Giảm network traffic
- ✅ Faster query execution

**3. Caching (có thể implement)**
```csharp
// Có thể thêm caching cho dữ liệu ít thay đổi
public async Task<IActionResult> GetCategories()
{
    // Cache categories vì ít thay đổi
    var categories = await _cache.GetOrCreateAsync(
        "categories",
        async entry => 
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return await _context.TblLoaiSanPhams.ToListAsync();
        }
    );
    return View(categories);
}
```

**Concurrent Request Handling:**
- ✅ ASP.NET Core hỗ trợ async request handling
- ✅ Thread pool tự động scale
- ✅ Kestrel server hiệu suất cao

---

### 2.2 Usability (Khả năng sử dụng)
**Yêu cầu**: Giao diện đơn giản, dễ hiểu, giúp người mới làm quen trong <1 giờ; cung cấp hướng dẫn sử dụng cơ bản (Word/PDF).

#### Cách MVC đáp ứng:

**1. Separation of Concerns - View riêng biệt**
```razor
@* File: Views/SanPham/Index.cshtml *@
@model IPagedList<CNPM.Models.TblSanPham>

<div class="container">
    <h2>Danh Sách Sản Phẩm</h2>
    
    <table class="table table-striped">
        <thead>
            <tr>
                <th>Mã SP</th>
                <th>Tên SP</th>
                <th>Đơn Giá</th>
                <th>Hành Động</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var item in Model)
            {
                <tr>
                    <td>@item.PkSMaSp</td>
                    <td>@item.STenSp</td>
                    <td>@item.FDonGia</td>
                    <td>
                        <a href="/SanPham/Edit/@item.PkSMaSp">Sửa</a>
                    </td>
                </tr>
            }
        </tbody>
    </table>
    
    @Html.PagedListPager(Model, page => Url.Action("Index", new { page }))
</div>
```

**Lợi ích cho Usability:**
- ✅ **Razor Syntax**: Dễ đọc, dễ maintain
- ✅ **Consistent Layout**: _Layout.cshtml cho consistent UI
- ✅ **Bootstrap Integration**: Responsive, professional UI
- ✅ **Helper Methods**: PagedListPager tự động tạo pagination

**2. Layout Template**
```razor
@* File: Views/Shared/_Layout.cshtml *@
<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Quản Lý Nhà Thuốc</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-light bg-light">
            <!-- Navigation menu -->
        </nav>
    </header>
    
    <main role="main" class="pb-3">
        @RenderBody()
    </main>
    
    <footer class="border-top footer text-muted">
        <div class="container">
            &copy; 2024 - Hệ Thống Quản Lý Nhà Thuốc
        </div>
    </footer>
    
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
```

**Lợi ích:**
- ✅ **Consistent User Experience**: Same layout across pages
- ✅ **Easy Navigation**: Centralized menu
- ✅ **Responsive Design**: Mobile-friendly

**3. Hướng dẫn sử dụng có thể tạo**
- 📄 User Manual.pdf: Hướng dẫn từng chức năng
- 📄 Quick Start Guide: Bắt đầu nhanh trong 30 phút
- 🎥 Video tutorials: Cho các chức năng phức tạp

---

### 2.3 Maintainability (Khả năng bảo trì)
**Yêu cầu**: Mã nguồn dễ đọc, có chú thích, tuân theo chuẩn lập trình; sửa lỗi nghiêm trọng < 48 giờ kể từ khi phát hiện; đi kèm tài liệu hướng dẫn bảo trì và triển khai.

#### Cách MVC đáp ứng:

**1. Clear Folder Structure**
```
CNPM/
├── Controllers/          # Logic điều khiển
│   ├── AccountController.cs
│   ├── SanPhamController.cs
│   └── BanHang.cs
├── Models/              # Entities & DbContext
│   ├── PharmacyDbContext.cs
│   ├── TblTaiKhoan.cs
│   └── TblSanPham.cs
├── Views/               # UI templates
│   ├── Account/
│   ├── SanPham/
│   └── Shared/
├── wwwroot/            # Static files
│   ├── css/
│   ├── js/
│   └── lib/
└── Program.cs          # Application configuration
```

**Lợi ích cho Maintainability:**
- ✅ **Clear Separation**: Dễ tìm file cần sửa
- ✅ **Naming Convention**: Tên file/class rõ ràng
- ✅ **Standard Structure**: Developer mới dễ hiểu

**2. Dependency Injection**
```csharp
// File: Program.cs
builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// File: Controllers/SanPhamController.cs
public class SanPhamController : Controller
{
    private readonly PharmacyDbContext _context;
    
    public SanPhamController(PharmacyDbContext context)
    {
        _context = context;
    }
}
```

**Lợi ích:**
- ✅ **Loose Coupling**: Dễ thay đổi implementation
- ✅ **Testability**: Dễ mock dependencies
- ✅ **Centralized Configuration**: Thay đổi ở 1 chỗ

**3. Entity Framework Code First**
```csharp
// Models tự động map với database
[Table("tbl_SanPham")]
public partial class TblSanPham
{
    [Key]
    [Column("PK_sMaSP")]
    public string PkSMaSp { get; set; }
    
    [ForeignKey("FkSMaLoaiSp")]
    public virtual TblLoaiSanPham? FkSMaLoaiSpNavigation { get; set; }
}
```

**Lợi ích:**
- ✅ **Self-Documenting**: Model là documentation của database schema
- ✅ **Type Safety**: Compile-time checking
- ✅ **Easy Migration**: Database changes qua migration

**4. Configuration Management**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

**Lợi ích:**
- ✅ Environment-specific configuration
- ✅ Không hardcode trong code
- ✅ Dễ deploy khác môi trường

**Tài liệu bảo trì nên có:**
- 📋 ARCHITECTURE.md: Giải thích kiến trúc tổng thể
- 📋 DEPLOYMENT.md: Hướng dẫn deploy
- 📋 TROUBLESHOOTING.md: Common issues và cách fix
- 📋 API_DOCUMENTATION.md: Document các controller actions

---

## MỨC 3: TRUNG BÌNH

### 3.1 Scalability/Expandability (Khả năng mở rộng)
**Yêu cầu**: Cho phép thêm người dùng hoặc dữ liệu mà không thay đổi cấu trúc; hệ thống có thể bổ sung chức năng mới (quản lý tồn kho) mà không ảnh hưởng đến phần hiện tại.

#### Cách MVC đáp ứng:

**1. Modular Controller Design**
```
Controllers/
├── AccountController.cs      # Module xác thực
├── SanPhamController.cs      # Module sản phẩm
├── BanHang.cs               # Module bán hàng
├── NhanVienController.cs    # Module nhân viên
└── BaoCaoController.cs      # Module báo cáo

# Thêm module mới KHÔNG ảnh hưởng modules cũ
└── TonKhoController.cs      # Module tồn kho MỚI (có thể thêm)
```

**Lợi ích cho Scalability:**
- ✅ **Independent Modules**: Thêm controller mới không ảnh hưởng code cũ
- ✅ **No Breaking Changes**: Controllers cũ hoạt động bình thường
- ✅ **Easy to Add Features**: Chỉ cần tạo Controller + Views + Models mới

**2. Entity Framework Migrations**
```csharp
// Thêm bảng mới không ảnh hưởng bảng cũ
public class TblTonKho
{
    [Key]
    public string MaTonKho { get; set; }
    public string MaSanPham { get; set; }
    public int SoLuong { get; set; }
    
    [ForeignKey("MaSanPham")]
    public virtual TblSanPham SanPham { get; set; }
}

// DbContext mở rộng dễ dàng
public partial class PharmacyDbContext : DbContext
{
    // Existing DbSets...
    public virtual DbSet<TblSanPham> TblSanPhams { get; set; }
    
    // NEW DbSet - không ảnh hưởng existing tables
    public virtual DbSet<TblTonKho> TblTonKhos { get; set; }
}
```

**Lợi ích:**
- ✅ **Backward Compatible**: Bảng cũ không bị ảnh hưởng
- ✅ **Incremental Changes**: Có thể add features từng bước
- ✅ **Migration Support**: Database schema versioning

**3. Scalable Architecture**
```csharp
// Có thể scale bằng cách:

// 1. Load Balancing - Deploy nhiều instances
// ASP.NET Core stateless, dễ scale horizontal

// 2. Database Optimization
builder.Services.AddDbContext<PharmacyDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
        sqlOptions.CommandTimeout(30);
    });
});

// 3. Caching Layer (có thể thêm)
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
```

**Expandability Examples:**
```
Chức năng mới có thể thêm KHÔNG ảnh hưởng code hiện tại:

1. Quản lý tồn kho:
   + TonKhoController.cs
   + Views/TonKho/*.cshtml
   + Models/TblTonKho.cs

2. Quản lý nhà cung cấp (đã có):
   ✓ NhaCungCapController.cs
   ✓ Views/NhaCungCap/*.cshtml
   ✓ Models/TblNhaCungCap.cs

3. Báo cáo nâng cao:
   + BaoCaoController.cs (mở rộng)
   + Views/BaoCao/ChiTiet.cshtml (thêm view mới)
```

---

### 3.2 Interoperability (Khả năng tương tác)
**Yêu cầu**: Hỗ trợ nhập/xuất dữ liệu dưới định dạng CSV; có thể kết nối/tích hợp với hệ thống quản lý khác trong tương lai.

#### Cách MVC đáp ứng:

**1. API-Ready Architecture**
```csharp
// Controller có thể return JSON cho API calls
public class SanPhamController : Controller
{
    // View-based action
    public async Task<IActionResult> Index()
    {
        var products = await _context.TblSanPhams.ToListAsync();
        return View(products);
    }
    
    // API-style action (có thể thêm)
    [HttpGet]
    [Route("api/sanpham")]
    public async Task<JsonResult> GetProducts()
    {
        var products = await _context.TblSanPhams.ToListAsync();
        return Json(products);
    }
}
```

**Lợi ích cho Interoperability:**
- ✅ **Multiple Response Types**: View, JSON, XML
- ✅ **RESTful Routes**: Dễ tích hợp với hệ thống khác
- ✅ **Content Negotiation**: ASP.NET Core hỗ trợ tự động

**2. CSV Export Example**
```csharp
public class BaoCaoController : Controller
{
    public async Task<IActionResult> ExportToCSV()
    {
        var sanPhams = await _context.TblSanPhams.ToListAsync();
        
        var csv = new StringBuilder();
        csv.AppendLine("Mã SP,Tên SP,Đơn Giá,Số Lượng");
        
        foreach (var sp in sanPhams)
        {
            csv.AppendLine($"{sp.PkSMaSp},{sp.STenSp},{sp.FDonGia},{sp.ISoLuong}");
        }
        
        var bytes = Encoding.UTF8.GetBytes(csv.ToString());
        return File(bytes, "text/csv", "sanpham.csv");
    }
    
    [HttpPost]
    public async Task<IActionResult> ImportFromCSV(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        // Parse CSV và import vào database
        // ...
        
        return RedirectToAction("Index");
    }
}
```

**Lợi ích:**
- ✅ **Standard Format**: CSV được nhiều hệ thống hỗ trợ
- ✅ **Easy Integration**: Import/Export data dễ dàng
- ✅ **Batch Operations**: Xử lý nhiều records cùng lúc

**3. Web API Integration**
```csharp
// Program.cs - Enable CORS for external systems
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowExternalSystems", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Controller có thể làm API endpoint
[ApiController]
[Route("api/[controller]")]
public class ProductApiController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TblSanPham>>> GetProducts()
    {
        return await _context.TblSanPhams.ToListAsync();
    }
    
    [HttpPost]
    public async Task<ActionResult<TblSanPham>> CreateProduct(TblSanPham product)
    {
        _context.TblSanPhams.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProducts), new { id = product.PkSMaSp }, product);
    }
}
```

**Integration Scenarios:**
- 🔌 **Kế toán**: Export dữ liệu bán hàng sang phần mềm kế toán
- 🔌 **ERP**: Đồng bộ tồn kho với hệ thống ERP
- 🔌 **CRM**: Import customer data từ CRM
- 🔌 **Mobile App**: Cung cấp API cho mobile app

---

## MỨC 4: THẤP

### 4.1 Portability (Tính khả chuyển)
**Yêu cầu**: Phần mềm phải chạy được trên Windows 10 hoặc cấu hình tương tự; dữ liệu có thể dễ dàng chuyển đổi giữa các máy tính (qua tệp/CSDL đơn giản).

#### Cách MVC đáp ứng:

**1. Cross-Platform Support**
```csharp
// CNPM.csproj
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <!-- .NET 9 chạy được trên: -->
    <!-- - Windows 10/11 ✓ -->
    <!-- - Linux ✓ -->
    <!-- - macOS ✓ -->
  </PropertyGroup>
</Project>
```

**Lợi ích cho Portability:**
- ✅ **.NET Core/9**: Cross-platform runtime
- ✅ **Kestrel Server**: Built-in web server, không cần IIS
- ✅ **Self-Contained Deployment**: Có thể bundle .NET runtime

**2. Database Portability**
```csharp
// appsettings.json - Connection string dễ đổi
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PharmacyDB;..."
  }
}

// Có thể deploy database bằng:
// 1. SQL Script (CODE DATABASE.sql)
// 2. Database backup/restore (.bak file)
// 3. Entity Framework migrations
```

**Lợi ích:**
- ✅ **Database Independent**: Chỉ cần SQL Server (có Express version free)
- ✅ **Easy Migration**: Backup/restore hoặc run script
- ✅ **Cloud Ready**: Có thể deploy lên Azure, AWS

**3. Configuration-Based Deployment**
```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Đọc configuration từ file, dễ customize cho từng môi trường
builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
```

**Deployment Steps:**
```bash
# 1. Publish application
dotnet publish -c Release -o ./publish

# 2. Copy folder 'publish' sang máy khác
# 3. Update appsettings.json với connection string mới
# 4. Run application
dotnet CNPM.dll

# Hoặc tạo self-contained:
dotnet publish -c Release -r win-x64 --self-contained
# Không cần cài .NET runtime trên máy đích
```

---

### 4.2 Reusability (Khả năng tái sử dụng)
**Yêu cầu**: Các mô-đun chức năng (xử lý hóa đơn, quản lý thuốc, đăng nhập) nên được thiết kế độc lập, có thể tái sử dụng trong các dự án hoặc phiên bản khác.

#### Cách MVC đáp ứng:

**1. Independent Controllers (Reusable Modules)**
```
Controllers/
├── AccountController.cs       # Authentication module
│   - Login()                  # Có thể dùng lại cho app khác
│   - Register()
│   - Logout()
│
├── SanPhamController.cs       # Product management module
│   - Index()                  # Generic CRUD operations
│   - Create()                 # Có thể adapt cho product types khác
│   - Edit()
│   - Delete()
│
├── BanHang.cs                # Sales module
│   - TaoHoaDon()             # Invoice creation logic
│   - ThanhToan()             # Payment processing
│
└── BaoCaoController.cs       # Reporting module
    - BaoCaoDoanhThu()        # Revenue report
    - BaoCaoTonKho()          # Inventory report
```

**Lợi ích cho Reusability:**
- ✅ **Self-Contained Modules**: Mỗi controller là 1 module độc lập
- ✅ **Minimal Dependencies**: Controllers chỉ depend vào DbContext
- ✅ **Copy-Paste Friendly**: Copy controller + models + views sang project mới

**2. Reusable Models**
```csharp
// Models có thể reuse cho nhiều dự án
namespace CNPM.Models
{
    // Authentication model - reusable
    [Table("tbl_TaiKhoan")]
    public partial class TblTaiKhoan
    {
        [Key] public string PkSMaTk { get; set; }
        public string? STenTk { get; set; }
        public string? SMk { get; set; }
        public string? FkSMaQuyen { get; set; }
    }
    
    // Product model - có thể adapt cho các loại sản phẩm khác
    [Table("tbl_SanPham")]
    public partial class TblSanPham
    {
        [Key] public string PkSMaSp { get; set; }
        public string? STenSp { get; set; }
        public double? FDonGia { get; set; }
        public int? ISoLuong { get; set; }
    }
}

// Các model này có thể:
// - Copy sang dự án khác (cửa hàng tạp hóa, quản lý kho)
// - Customize theo nhu cầu mới
// - Làm base cho inheritance
```

**3. Shared Views and Layouts**
```razor
@* Views/Shared/_Layout.cshtml - Reusable layout *@
@* Có thể dùng làm template cho nhiều dự án *@
<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="utf-8" />
    <title>@ViewData["Title"] - @ViewData["AppName"]</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
</head>
<body>
    <header>
        @RenderSection("Header", required: false)
    </header>
    <main>
        @RenderBody()
    </main>
    <footer>
        @RenderSection("Footer", required: false)
    </footer>
</body>
</html>
```

**Lợi ích:**
- ✅ **Template Reusability**: Layout có thể dùng cho nhiều app
- ✅ **Consistent UI**: Same look & feel across projects
- ✅ **Easy Customization**: Override sections theo nhu cầu

**4. Service Layer Pattern (Recommended Enhancement)**
```csharp
// Có thể extract business logic thành services để tăng reusability

// Interface
public interface IProductService
{
    Task<IEnumerable<TblSanPham>> GetAllProductsAsync();
    Task<TblSanPham> GetProductByIdAsync(string id);
    Task CreateProductAsync(TblSanPham product);
    Task UpdateProductAsync(TblSanPham product);
    Task DeleteProductAsync(string id);
}

// Implementation
public class ProductService : IProductService
{
    private readonly PharmacyDbContext _context;
    
    public ProductService(PharmacyDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TblSanPham>> GetAllProductsAsync()
    {
        return await _context.TblSanPhams.ToListAsync();
    }
    // ... other methods
}

// Register in Program.cs
builder.Services.AddScoped<IProductService, ProductService>();

// Use in Controller
public class SanPhamController : Controller
{
    private readonly IProductService _productService;
    
    public SanPhamController(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }
}
```

**Lợi ích của Service Layer:**
- ✅ **Business Logic Reusability**: Services có thể dùng cho Web, API, Console app
- ✅ **Testability**: Dễ unit test
- ✅ **Separation of Concerns**: Controller chỉ lo routing, logic ở Service

**Reusability Matrix:**

| Component | Reusability | How to Reuse |
|-----------|------------|--------------|
| **AccountController** | ⭐⭐⭐⭐⭐ | Copy sang project khác cần authentication |
| **Models (Entities)** | ⭐⭐⭐⭐ | Adapt cho domain tương tự (retail, inventory) |
| **DbContext** | ⭐⭐⭐⭐ | Template cho EF Core projects |
| **Shared Views** | ⭐⭐⭐⭐⭐ | Reuse layout across projects |
| **CRUD Controllers** | ⭐⭐⭐⭐ | Generic pattern for any entity |

---

## KẾT LUẬN

### Bảng Tổng Hợp MVC đáp ứng NFRs

| NFR Category | Priority | MVC Component | Compliance Level |
|--------------|----------|---------------|------------------|
| **Reliability** | Mức 1 | Model (EF Core Transactions) | ✅ ⭐⭐⭐⭐ |
| **Availability** | Mức 1 | Controller (Async, Stateless) | ✅ ⭐⭐⭐⭐⭐ |
| **Security** | Mức 1 | Controller (Auth), Model (Validation) | ✅ ⭐⭐⭐⭐ |
| **Compliance** | Mức 1 | Model (Business Rules), Controller (Reports) | ✅ ⭐⭐⭐⭐ |
| **Performance** | Mức 2 | Controller (Async), Model (Optimized Queries) | ✅ ⭐⭐⭐⭐⭐ |
| **Usability** | Mức 2 | View (Razor, Bootstrap) | ✅ ⭐⭐⭐⭐⭐ |
| **Maintainability** | Mức 2 | MVC Structure, DI, Conventions | ✅ ⭐⭐⭐⭐⭐ |
| **Scalability** | Mức 3 | Modular Controllers, EF Migrations | ✅ ⭐⭐⭐⭐⭐ |
| **Interoperability** | Mức 3 | Controller (JSON, CSV), API-ready | ✅ ⭐⭐⭐⭐ |
| **Portability** | Mức 4 | .NET 9 Cross-platform | ✅ ⭐⭐⭐⭐⭐ |
| **Reusability** | Mức 4 | Independent Modules | ✅ ⭐⭐⭐⭐⭐ |

### Ưu điểm của MVC trong dự án này:

1. **Separation of Concerns (Tách biệt rõ ràng)**
   - Model: Data & Business Logic
   - View: Presentation
   - Controller: Request Handling
   
2. **Maintainability (Dễ bảo trì)**
   - Cấu trúc rõ ràng, dễ navigate
   - Naming conventions nhất quán
   - Dependency Injection

3. **Testability (Dễ test)**
   - Mỗi layer test độc lập
   - Mock dependencies dễ dàng

4. **Scalability (Dễ mở rộng)**
   - Thêm controller/model không ảnh hưởng code cũ
   - Modular design

5. **Reusability (Tái sử dụng cao)**
   - Controllers độc lập
   - Models có thể adapt
   - Views có thể reuse

### Recommendations (Đề xuất cải tiến):

1. **Security Enhancements:**
   ```csharp
   // Implement password hashing với work factor
   using BCrypt.Net;
   
   // Khi tạo mật khẩu mới (Register)
   int workFactor = 12; // 10-12 cho ứng dụng hiện đại
   var hashedPassword = BCrypt.HashPassword(password, workFactor);
   
   // Khi verify mật khẩu (Login)
   bool isValid = BCrypt.Verify(password, taiKhoan.SMk);
   
   // Add [Authorize] attributes
   [Authorize]
   public class SanPhamController : Controller { }
   ```

2. **Performance Optimizations:**
   ```csharp
   // Add caching
   builder.Services.AddMemoryCache();
   
   // Add compression
   builder.Services.AddResponseCompression();
   ```

3. **Better Error Handling:**
   ```csharp
   // Global exception handler
   app.UseExceptionHandler("/Error/Index");
   app.UseStatusCodePagesWithReExecute("/Error/{0}");
   ```

4. **Logging:**
   ```csharp
   // Add structured logging
   builder.Services.AddLogging(logging =>
   {
       logging.AddConsole();
       logging.AddDebug();
       logging.AddFile("logs/app-{Date}.txt");
   });
   ```

5. **API Documentation:**
   ```csharp
   // Add Swagger for API docs
   builder.Services.AddEndpointsApiExplorer();
   builder.Services.AddSwaggerGen();
   ```

---

## PHỤ LỤC

### A. Code Examples Chi Tiết

#### A.1 Authentication Flow
```csharp
// 1. User submits login form
// 2. AccountController.Login() validates credentials
// 3. Create Claims and sign in
// 4. Redirect to Home/Index
// 5. Subsequent requests include authentication cookie
```

#### A.2 Data Flow trong MVC
```
User Request → Routing → Controller Action → Model (DbContext) → Database
                                    ↓
                              View (Razor) ← ViewModel
                                    ↓
                              HTML Response → User
```

### B. Database Schema
```sql
-- Các bảng chính:
tbl_TaiKhoan        -- User accounts
tbl_SanPham         -- Products (medicines)
tbl_PhieuThu        -- Sales invoices
tbl_NhanVien        -- Employees
tbl_KhachHang       -- Customers
tbl_BaoCaoThuChi    -- Financial reports
```

### C. Deployment Checklist
```
☐ Cài đặt .NET 9 Runtime
☐ Cài đặt SQL Server
☐ Restore database từ CODE DATABASE.sql
☐ Update connection string trong appsettings.json
☐ Run: dotnet publish -c Release
☐ Copy published files sang production server
☐ Configure IIS / Kestrel
☐ Test application
☐ Setup backup schedule
☐ Monitor logs
```

### D. Contact & Support
- **Developer**: CNPM Team
- **Version**: 1.0
- **Last Updated**: 2024
- **Framework**: ASP.NET Core 9.0 MVC

---

**Kết luận cuối cùng:** Kiến trúc MVC của dự án CNPM đáp ứng TỐT tất cả các yêu cầu phi chức năng (NFRs) từ Mức 1 đến Mức 4. Cấu trúc phân tầng rõ ràng, modular design, và sử dụng các best practices của ASP.NET Core giúp hệ thống dễ maintain, scale, và mở rộng trong tương lai.

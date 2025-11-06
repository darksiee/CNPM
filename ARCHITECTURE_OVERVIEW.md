# Architecture Overview - Pharmacy Management System

## MVC Architecture Demonstration

This document provides visual diagrams and code examples demonstrating how the MVC architecture pattern is implemented in the CNPM Pharmacy Management System.

## 1. High-Level Architecture

```
┌────────────────────────────────────────────────────────────────┐
│                    Client (Web Browser)                         │
│                  User Interface Layer                          │
└───────────────────────────┬────────────────────────────────────┘
                            │ HTTP Request/Response
                            │
┌───────────────────────────▼────────────────────────────────────┐
│                    ASP.NET Core MVC                            │
│  ┌──────────────────────────────────────────────────────────┐ │
│  │  Middleware Pipeline                                      │ │
│  │  - Static Files                                           │ │
│  │  - Authentication                                         │ │
│  │  - Authorization                                          │ │
│  │  - Routing                                                │ │
│  │  - Exception Handling                                     │ │
│  └──────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐        │
│  │              │  │              │  │              │        │
│  │  Controller  │◄─┤    Model     │  │     View     │        │
│  │              │  │              │  │              │        │
│  │  • Routing   │  │  • Entities  │  │  • Razor     │        │
│  │  • Actions   │──►  • DbContext │  │  • HTML      │        │
│  │  • Logic     │  │  • Validation│  │  • CSS/JS    │        │
│  │              │  │              │  │              │        │
│  └──────────────┘  └──────┬───────┘  └──────▲───────┘        │
│                           │                  │                 │
└───────────────────────────┼──────────────────┼─────────────────┘
                            │                  │
                  ┌─────────▼─────────┐       │
                  │ Entity Framework  │       │
                  │      Core         │       │
                  └─────────┬─────────┘       │
                            │                  │
                  ┌─────────▼─────────┐       │
                  │   SQL Server DB   │       │
                  │  (PharmacyDB)     │       │
                  └───────────────────┘       │
                                              │
                  Data flows back up ─────────┘
```

## 2. MVC Pattern in Detail

### 2.1 Model Layer

**Purpose**: Data structures and business logic

**Components**:
- Entity classes (`TblTaiKhoan`, `TblSanPham`, etc.)
- `PharmacyDbContext` (Data access)
- Data annotations and validations

**Example**:
```csharp
// Models/TblSanPham.cs
[Table("tbl_SanPham")]
public partial class TblSanPham
{
    [Key]
    [Column("PK_sMaSP")]
    [StringLength(20)]
    public string PkSMaSp { get; set; } = null!;

    [Column("sTenSP")]
    [StringLength(100)]
    public string? STenSp { get; set; }

    [Column("fDonGia")]
    public double? FDonGia { get; set; }

    [Column("iSoLuong")]
    public int? ISoLuong { get; set; }

    [ForeignKey("FkSMaLoaiSp")]
    public virtual TblLoaiSanPham? FkSMaLoaiSpNavigation { get; set; }
}
```

### 2.2 View Layer

**Purpose**: User interface presentation

**Components**:
- Razor views (`.cshtml` files)
- Layouts (`_Layout.cshtml`)
- Partial views
- Static assets (CSS, JavaScript)

**Example**:
```razor
@* Views/SanPham/Index.cshtml *@
@model IPagedList<CNPM.Models.TblSanPham>

<div class="container">
    <h2>Product List</h2>
    
    <table class="table">
        <thead>
            <tr>
                <th>Product Code</th>
                <th>Product Name</th>
                <th>Price</th>
                <th>Quantity</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var item in Model)
            {
                <tr>
                    <td>@item.PkSMaSp</td>
                    <td>@item.STenSp</td>
                    <td>@item.FDonGia?.ToString("C")</td>
                    <td>@item.ISoLuong</td>
                    <td>
                        <a asp-action="Edit" asp-route-id="@item.PkSMaSp">Edit</a> |
                        <a asp-action="Delete" asp-route-id="@item.PkSMaSp">Delete</a>
                    </td>
                </tr>
            }
        </tbody>
    </table>
    
    @Html.PagedListPager(Model, page => Url.Action("Index", new { page }))
</div>
```

### 2.3 Controller Layer

**Purpose**: Request handling and flow control

**Components**:
- Controller classes
- Action methods
- Request routing
- Response generation

**Example**:
```csharp
// Controllers/SanPhamController.cs
public class SanPhamController : Controller
{
    private readonly PharmacyDbContext _context;

    public SanPhamController(PharmacyDbContext context)
    {
        _context = context;
    }

    // GET: SanPham
    public async Task<IActionResult> Index(int? page)
    {
        int pageSize = 10;
        int pageNumber = page ?? 1;
        
        var sanPhams = await _context.TblSanPhams
            .Include(sp => sp.FkSMaLoaiSpNavigation)
            .ToPagedListAsync(pageNumber, pageSize);
        
        return View(sanPhams);
    }

    // GET: SanPham/Create
    public IActionResult Create()
    {
        ViewBag.Categories = _context.TblLoaiSanPhams.ToList();
        return View();
    }

    // POST: SanPham/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TblSanPham sanPham)
    {
        if (ModelState.IsValid)
        {
            _context.Add(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(sanPham);
    }
}
```

## 3. Request Flow Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│  1. User clicks "View Products" link                            │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  2. HTTP GET /SanPham/Index                                     │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  3. Routing: Matches to SanPhamController.Index()               │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  4. Controller Action Execution                                 │
│     - Inject PharmacyDbContext                                  │
│     - Call _context.TblSanPhams.ToPagedListAsync()             │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  5. Entity Framework queries database                           │
│     SELECT * FROM tbl_SanPham                                   │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  6. Database returns data (List<TblSanPham>)                    │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  7. Controller passes model to View                             │
│     return View(sanPhams);                                      │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  8. Razor View Engine processes Index.cshtml                    │
│     - Binds model data                                          │
│     - Generates HTML                                            │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  9. HTTP Response with HTML sent to browser                     │
└────────────────────────────┬────────────────────────────────────┘
                             │
                             ▼
┌─────────────────────────────────────────────────────────────────┐
│  10. Browser renders the product list page                      │
└─────────────────────────────────────────────────────────────────┘
```

## 4. Module Independence

One of the key benefits of MVC is module independence. Each controller represents an independent module:

```
┌──────────────────┐     ┌──────────────────┐     ┌──────────────────┐
│ AccountController│     │ SanPhamController│     │  BanHang         │
│                  │     │                  │     │  Controller      │
│ - Login()        │     │ - Index()        │     │                  │
│ - Logout()       │     │ - Create()       │     │ - Index()        │
│ - Register()     │     │ - Edit()         │     │ - TaoHoaDon()    │
│                  │     │ - Delete()       │     │ - ThanhToan()    │
└────────┬─────────┘     └────────┬─────────┘     └────────┬─────────┘
         │                        │                        │
         │                        │                        │
         └────────────┬───────────┴────────────┬───────────┘
                      │                        │
                      ▼                        ▼
         ┌────────────────────────────────────────────┐
         │      PharmacyDbContext (Shared)            │
         │                                            │
         │  - TblTaiKhoans                           │
         │  - TblSanPhams                            │
         │  - TblPhieuThus                           │
         └────────────────────────────────────────────┘
```

**Benefits**:
- ✅ Can modify one controller without affecting others
- ✅ Easy to add new modules
- ✅ Can test modules independently
- ✅ Clear responsibility boundaries

## 5. Dependency Injection Pattern

```csharp
// Program.cs - Service Registration
builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseSqlServer(connectionString));

// Controller - Constructor Injection
public class SanPhamController : Controller
{
    private readonly PharmacyDbContext _context;
    
    public SanPhamController(PharmacyDbContext context)
    {
        _context = context;  // Injected by framework
    }
    
    public async Task<IActionResult> Index()
    {
        var products = await _context.TblSanPhams.ToListAsync();
        return View(products);
    }
}
```

**Benefits**:
- ✅ Loose coupling between components
- ✅ Easy to test (can inject mock DbContext)
- ✅ Centralized configuration
- ✅ Automatic lifecycle management

## 6. Folder Structure Map

```
CNPM/
│
├── Controllers/                    # C - Controllers
│   ├── AccountController.cs        # Authentication
│   ├── SanPhamController.cs        # Product management
│   ├── BanHang.cs                  # Sales
│   ├── NhanVienController.cs       # Employee management
│   ├── KhachHangController.cs      # Customer management
│   ├── BaoCaoController.cs         # Reports
│   └── HomeController.cs           # Home page
│
├── Models/                         # M - Models
│   ├── PharmacyDbContext.cs        # EF DbContext
│   ├── TblTaiKhoan.cs             # Account entity
│   ├── TblSanPham.cs              # Product entity
│   ├── TblPhieuThu.cs             # Receipt entity
│   ├── TblNhanVien.cs             # Employee entity
│   ├── TblKhachHang.cs            # Customer entity
│   └── [... other entities ...]
│
├── Views/                          # V - Views
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   ├── SanPham/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   └── Edit.cshtml
│   ├── BanHang/
│   │   ├── Index.cshtml
│   │   └── HoaDon.cshtml
│   ├── Shared/
│   │   ├── _Layout.cshtml          # Master layout
│   │   └── Error.cshtml
│   └── [... other view folders ...]
│
├── wwwroot/                        # Static files
│   ├── css/
│   ├── js/
│   └── lib/
│       ├── bootstrap/
│       ├── jquery/
│       └── jquery-validation/
│
├── Program.cs                      # App configuration
├── appsettings.json               # Configuration
└── CNPM.csproj                    # Project file
```

## 7. Authentication Flow

```
┌──────────────────────────────────────────────────────────────┐
│  1. User navigates to /Account/Login                         │
└────────────────────┬─────────────────────────────────────────┘
                     │
                     ▼
┌──────────────────────────────────────────────────────────────┐
│  2. AccountController.Login() [GET]                          │
│     Returns Login view                                       │
└────────────────────┬─────────────────────────────────────────┘
                     │
                     ▼
┌──────────────────────────────────────────────────────────────┐
│  3. User enters credentials and submits form                 │
└────────────────────┬─────────────────────────────────────────┘
                     │
                     ▼
┌──────────────────────────────────────────────────────────────┐
│  4. AccountController.Login() [POST]                         │
│     - Validate credentials against database                  │
│     - Create claims (Name, Role, etc.)                       │
│     - Sign in user with cookie authentication                │
└────────────────────┬─────────────────────────────────────────┘
                     │
                     ├─── Invalid ───► Return View with error
                     │
                     └─── Valid ───┐
                                   │
                                   ▼
┌──────────────────────────────────────────────────────────────┐
│  5. Redirect to Home/Index                                   │
│     - Cookie set in browser                                  │
│     - User.Identity.IsAuthenticated = true                   │
└──────────────────────────────────────────────────────────────┘
```

## 8. NFR Mapping to MVC Components

| Non-Functional Requirement | MVC Component | Implementation |
|---------------------------|---------------|----------------|
| **Reliability** | Model (EF Core) | Transactions, connection pooling, retry logic |
| **Availability** | Controller (Async) | Non-blocking operations, stateless design |
| **Security** | Controller + Model | Authentication, authorization, validation |
| **Performance** | All layers | Async/await, pagination, caching, optimized queries |
| **Usability** | View | Razor syntax, Bootstrap, consistent layout |
| **Maintainability** | MVC structure | Clear separation, DI, conventions |
| **Scalability** | All layers | Modular design, stateless controllers |
| **Interoperability** | Controller | JSON API support, CSV export |
| **Portability** | .NET Core | Cross-platform runtime |
| **Reusability** | All layers | Independent modules, DI |

## 9. Adding New Features (Example: Inventory Management)

To demonstrate scalability, here's how to add a new feature:

```
Step 1: Create Model
┌──────────────────────────────────────────┐
│ Models/TblTonKho.cs                      │
│                                          │
│ [Table("tbl_TonKho")]                    │
│ public class TblTonKho                   │
│ {                                        │
│     [Key] public string MaTonKho;        │
│     public string MaSanPham;             │
│     public int SoLuongTon;               │
│     public DateTime NgayCapNhat;         │
│ }                                        │
└──────────────────────────────────────────┘

Step 2: Update DbContext
┌──────────────────────────────────────────┐
│ Models/PharmacyDbContext.cs              │
│                                          │
│ public DbSet<TblTonKho> TblTonKhos       │
│     { get; set; }                        │
└──────────────────────────────────────────┘

Step 3: Create Controller
┌──────────────────────────────────────────┐
│ Controllers/TonKhoController.cs          │
│                                          │
│ public class TonKhoController            │
│ {                                        │
│     public async Task<IActionResult>     │
│         Index() { ... }                  │
│     public async Task<IActionResult>     │
│         Update() { ... }                 │
│ }                                        │
└──────────────────────────────────────────┘

Step 4: Create Views
┌──────────────────────────────────────────┐
│ Views/TonKho/Index.cshtml               │
│ Views/TonKho/Update.cshtml              │
└──────────────────────────────────────────┘

✅ No changes to existing code!
✅ Existing features continue working!
```

## 10. Summary

The MVC architecture in this project provides:

1. **Clear Separation**: Model, View, Controller each have distinct responsibilities
2. **Maintainability**: Easy to locate and modify code
3. **Testability**: Components can be tested independently
4. **Scalability**: Easy to add new features without breaking existing code
5. **Reusability**: Controllers and models can be reused in other projects
6. **Security**: Built-in support for authentication and validation
7. **Performance**: Async operations, connection pooling, caching support

This architecture meets all Non-Functional Requirements from Level 1 (Highest) to Level 4 (Lowest) as detailed in the main NFR documentation.

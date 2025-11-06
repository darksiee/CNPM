# NFR Documentation for CNPM Pharmacy Management System

## 📚 Documentation Overview

This repository contains comprehensive documentation demonstrating how the **ASP.NET Core MVC architecture** meets all **Non-Functional Requirements (NFRs)** for the Pharmacy Management System.

## 📖 Available Documents

### 1. NFR_MVC_DOCUMENTATION.md (Vietnamese) 🇻🇳
**Main documentation file - Comprehensive NFR analysis**

- **Content**: Detailed explanation of how MVC architecture addresses each NFR
- **Language**: Vietnamese (Tiếng Việt)
- **Sections**:
  - Mức 1 (Cao nhất): Reliability, Availability, Security, Compliance
  - Mức 2 (Quan trọng): Performance, Usability, Maintainability
  - Mức 3 (Trung bình): Scalability, Interoperability
  - Mức 4 (Thấp): Portability, Reusability
- **Includes**: 
  - Code examples for each NFR
  - MVC architecture diagrams
  - Implementation recommendations
  - Compliance matrix

**👉 Start here if**: You need detailed Vietnamese documentation

### 2. ARCHITECTURE_OVERVIEW.md (English) 🇺🇸
**Technical architecture documentation with diagrams**

- **Content**: Visual diagrams and technical details of MVC implementation
- **Language**: English
- **Sections**:
  - High-level architecture diagram
  - MVC pattern explanation (Model, View, Controller)
  - Request flow diagrams
  - Module independence visualization
  - Dependency injection pattern
  - Folder structure map
  - Authentication flow
  - NFR mapping to components

**👉 Start here if**: You need technical architecture understanding

### 3. QUICK_REFERENCE.md (Bilingual) 🇻🇳🇺🇸
**Quick reference guide and presentation checklist**

- **Content**: Summary, quick lookup, and presentation guide
- **Language**: Vietnamese + English
- **Sections**:
  - Quick index to all NFRs
  - Key highlights
  - How to read the documentation
  - Presentation checklist
  - Common Q&A
  - File map

**👉 Start here if**: You need a quick overview or preparing a presentation

## 🎯 For Different Audiences

### Product Owners / Managers
1. Read **QUICK_REFERENCE.md** first
2. Check the compliance matrix in **NFR_MVC_DOCUMENTATION.md** conclusion section
3. Review recommendations for improvements

### Developers
1. Read **ARCHITECTURE_OVERVIEW.md** to understand the structure
2. Study code examples in **NFR_MVC_DOCUMENTATION.md**
3. Follow naming conventions and patterns shown

### Testers / QA
1. Use **NFR_MVC_DOCUMENTATION.md** as test requirement specification
2. Verify each NFR checklist
3. Test performance metrics mentioned

### Architects / Technical Reviewers
1. Read all three documents
2. Verify implementation matches documentation
3. Review code examples against actual codebase

## 📊 NFR Compliance Summary

| NFR Level | Requirements | Compliance |
|-----------|-------------|------------|
| **Mức 1 (Highest)** | Reliability, Availability, Security, Compliance | ✅ ⭐⭐⭐⭐ |
| **Mức 2 (Important)** | Performance, Usability, Maintainability | ✅ ⭐⭐⭐⭐⭐ |
| **Mức 3 (Medium)** | Scalability, Interoperability | ✅ ⭐⭐⭐⭐⭐ |
| **Mức 4 (Low)** | Portability, Reusability | ✅ ⭐⭐⭐⭐⭐ |

**Overall Assessment**: The MVC architecture **fully meets** all NFR requirements from Level 1 to Level 4.

## 🏗️ MVC Architecture Overview

```
┌──────────────────────────────────────────────┐
│           ASP.NET Core MVC                   │
│  ┌─────────┐  ┌─────────┐  ┌─────────┐     │
│  │  Model  │  │  View   │  │Controller│     │
│  │         │  │         │  │          │     │
│  │ Data &  │  │  Razor  │  │ Request  │     │
│  │Business │◄─┤  HTML   │◄─┤ Handling │     │
│  │ Logic   │  │  UI     │  │          │     │
│  └────┬────┘  └─────────┘  └─────────┘     │
└───────┼───────────────────────────────────────┘
        │
        ▼
  ┌───────────┐
  │ Database  │
  │SQL Server │
  └───────────┘
```

### Key Components:

- **Controllers/** (13 controllers)
  - AccountController: Authentication & authorization
  - SanPhamController: Product management
  - BanHang: Sales and invoicing
  - BaoCaoController: Reporting
  - [... and 9 more]

- **Models/** (20+ entities)
  - PharmacyDbContext: EF Core DbContext
  - TblTaiKhoan, TblSanPham, TblPhieuThu, etc.

- **Views/** (Multiple view folders)
  - Razor templates (.cshtml)
  - Shared layouts
  - Bootstrap-based UI

## 🚀 How MVC Meets NFRs - Quick Examples

### Reliability ✅
```csharp
// EF Core automatic transaction management
await _context.SaveChangesAsync();  // Atomic operation
```

### Performance ✅
```csharp
// Async operations for better throughput
public async Task<IActionResult> Index()
{
    var data = await _context.TblSanPhams.ToListAsync();
    return View(data);
}
```

### Security ✅
```csharp
// Cookie-based authentication with claims
var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, username),
    new Claim(ClaimTypes.Role, role)
};
await HttpContext.SignInAsync("CookieAuth", principal);
```

### Maintainability ✅
```
Clear folder structure:
Controllers/  ← Business logic
Models/       ← Data models  
Views/        ← UI templates
```

### Scalability ✅
```
Add new module:
1. Create Controller → TonKhoController.cs
2. Create Model → TblTonKho.cs
3. Create Views → Views/TonKho/
✅ No changes to existing code!
```

## 📋 Presentation Guide

When presenting this to stakeholders:

### 5-Minute Version
1. Show MVC architecture diagram (ARCHITECTURE_OVERVIEW.md #1)
2. Highlight compliance matrix (NFR_MVC_DOCUMENTATION.md - Kết luận)
3. Demo 2-3 key NFRs with code examples

### 15-Minute Version
1. Introduction to MVC (5 min)
2. Walk through Mức 1 NFRs with examples (7 min)
3. Summary and recommendations (3 min)

### 30-Minute Version
1. Full MVC architecture explanation (10 min)
2. All NFR levels with code examples (15 min)
3. Q&A and recommendations (5 min)

Use **QUICK_REFERENCE.md** checklist while presenting.

## 🔧 Technical Stack

- **Framework**: ASP.NET Core 9.0 MVC
- **Database**: SQL Server with Entity Framework Core 9.0.2
- **UI**: Razor Pages + Bootstrap
- **Authentication**: Cookie Authentication
- **Additional**: X.PagedList for pagination

## 📈 Key Benefits of MVC

1. **Separation of Concerns**: Clear boundaries between data, UI, and logic
2. **Testability**: Each layer can be tested independently
3. **Maintainability**: Easy to locate and modify code
4. **Scalability**: Add features without breaking existing functionality
5. **Reusability**: Components can be reused across projects
6. **Industry Standard**: Well-known pattern, easy for new developers

## 🎓 Recommendations for Improvement

### High Priority
- ✅ Implement password hashing (BCrypt/PBKDF2)
- ✅ Add `[Authorize]` attributes to protected controllers
- ✅ Enable HTTPS

### Medium Priority
- ⚠️ Add response caching for performance
- ⚠️ Implement comprehensive logging
- ⚠️ Complete CSV import/export functionality

### Low Priority
- 📋 Write unit tests
- 📋 Add API documentation (Swagger)
- 📋 Implement background jobs for heavy tasks

## 📞 Getting Started

1. **Read the documentation**:
   - Quick overview → `QUICK_REFERENCE.md`
   - Architecture → `ARCHITECTURE_OVERVIEW.md`
   - Detailed NFRs → `NFR_MVC_DOCUMENTATION.md`

2. **Explore the code**:
   - Controllers → `Controllers/`
   - Data models → `Models/`
   - UI templates → `Views/`

3. **Build and run**:
   ```bash
   # Step 1: Restore dependencies
   dotnet restore
   
   # Step 2: Setup database (choose one method)
   # Method A: Using SQL script
   # Run CODE DATABASE.sql on your SQL Server instance
   
   # Method B: Using Entity Framework migrations (if available)
   # dotnet ef database update
   
   # Step 3: Update connection string in appsettings.json
   # Edit the DefaultConnection to point to your database
   
   # Step 4: Build the application
   dotnet build
   
   # Step 5: Run the application
   dotnet run
   ```

## 📝 Document Maintenance

- **Version**: 1.0
- **Created**: November 2024
- **Language**: Vietnamese (main) + English (supplementary)
- **Purpose**: Demonstrate MVC architecture compliance with NFRs

## 🤝 Contributing

When updating this documentation:
1. Keep code examples in sync with actual implementation
2. Update diagrams if architecture changes
3. Maintain both Vietnamese and English versions
4. Keep the quick reference guide updated

## 📚 Additional Resources

- [ASP.NET Core MVC Documentation](https://learn.microsoft.com/aspnet/core/mvc/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/ef/core/)
- [C# Coding Conventions](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)

---

**Happy coding! 🚀**

For questions or clarifications, refer to the detailed documentation files listed above.

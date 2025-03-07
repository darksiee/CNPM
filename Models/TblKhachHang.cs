using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_KhachHang")]
public partial class TblKhachHang
{
    [Key]
    [Column("PK_sMaKH")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaKh { get; set; } = null!;

    [Column("sTenKH")]
    [StringLength(50)]
    public string? STenKh { get; set; }

    [Column("sSDT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SSdt { get; set; }

    [InverseProperty("FkSMaKhNavigation")]
    public virtual ICollection<TblPhieuThu> TblPhieuThus { get; set; } = new List<TblPhieuThu>();
}

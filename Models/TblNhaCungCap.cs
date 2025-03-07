using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_NhaCungCap")]
public partial class TblNhaCungCap
{
    [Key]
    [Column("PK_sMaNCC")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaNcc { get; set; } = null!;

    [Column("sTenNCC")]
    [StringLength(100)]
    public string? STenNcc { get; set; }

    [Column("sDiaChi")]
    [StringLength(100)]
    public string? SDiaChi { get; set; }

    [Column("sSDT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SSdt { get; set; }

    [Column("sSoTK")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SSoTk { get; set; }

    [InverseProperty("FkSMaNccNavigation")]
    public virtual ICollection<TblHopDongCungCap> TblHopDongCungCaps { get; set; } = new List<TblHopDongCungCap>();

    [InverseProperty("FkSMaNccNavigation")]
    public virtual ICollection<TblSanPham> TblSanPhams { get; set; } = new List<TblSanPham>();
}

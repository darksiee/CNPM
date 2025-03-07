using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_LoaiSanPham")]
public partial class TblLoaiSanPham
{
    [Key]
    [Column("PK_sMaLoai")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaLoai { get; set; } = null!;

    [Column("sTenLoai")]
    [StringLength(100)]
    public string? STenLoai { get; set; }

    [InverseProperty("FkSMaLoaiNavigation")]
    public virtual ICollection<TblSanPham> TblSanPhams { get; set; } = new List<TblSanPham>();
}

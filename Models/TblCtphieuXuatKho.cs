using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaPx", "PkFkSMaSp")]
[Table("tbl_CTPhieuXuatKho")]
public partial class TblCtphieuXuatKho
{
    [Key]
    [Column("PK_FK_sMaPX")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaPx { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("iSLYC")]
    public int? ISlyc { get; set; }

    [Column("iSLX")]
    public int? ISlx { get; set; }

    [Column("sGhiChu")]
    [StringLength(100)]
    public string? SGhiChu { get; set; }

    [ForeignKey("PkFkSMaPx")]
    [InverseProperty("TblCtphieuXuatKhos")]
    public virtual TblPhieuXuatKho PkFkSMaPxNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCtphieuXuatKhos")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

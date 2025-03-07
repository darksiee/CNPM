using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaPn", "PkFkSMaSp")]
[Table("tbl_CTPhieuNhapKho")]
public partial class TblCtphieuNhapKho
{
    [Key]
    [Column("PK_FK_sMaPN")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaPn { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("iSL")]
    public int? ISl { get; set; }

    [Column("sGhiChu")]
    [StringLength(100)]
    public string? SGhiChu { get; set; }

    [ForeignKey("PkFkSMaPn")]
    [InverseProperty("TblCtphieuNhapKhos")]
    public virtual TblPhieuNhapKho PkFkSMaPnNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCtphieuNhapKhos")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

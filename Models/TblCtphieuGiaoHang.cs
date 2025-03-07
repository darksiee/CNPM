using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaPgh", "PkFkSMaSp")]
[Table("tbl_CTPhieuGiaoHang")]
public partial class TblCtphieuGiaoHang
{
    [Key]
    [Column("PK_FK_sMaPGH")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaPgh { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("iSL")]
    public int? ISl { get; set; }

    [ForeignKey("PkFkSMaPgh")]
    [InverseProperty("TblCtphieuGiaoHangs")]
    public virtual TblPhieuGiaoHang PkFkSMaPghNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCtphieuGiaoHangs")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaPyc", "PkFkSMaSp")]
[Table("tbl_CTPhieuYeuCau")]
public partial class TblCtphieuYeuCau
{
    [Key]
    [Column("PK_FK_sMaPYC")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaPyc { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("iSLCan")]
    public int? ISlcan { get; set; }

    [Column("iSLDuyet")]
    public int? ISlduyet { get; set; }

    [ForeignKey("PkFkSMaPyc")]
    [InverseProperty("TblCtphieuYeuCaus")]
    public virtual TblPhieuYeuCau PkFkSMaPycNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCtphieuYeuCaus")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

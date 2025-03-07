using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaPt", "PkFkSMaSp")]
[Table("tbl_CTPhieuThu")]
public partial class TblCtphieuThu
{
    [Key]
    [Column("PK_FK_sMaPT")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaPt { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("iSL")]
    public int? ISl { get; set; }

    [ForeignKey("PkFkSMaPt")]
    [InverseProperty("TblCtphieuThus")]
    public virtual TblPhieuThu PkFkSMaPtNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCtphieuThus")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

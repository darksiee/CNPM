using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaPdh", "PkFkSMaSp")]
[Table("tbl_CTPhieuDatHang")]
public partial class TblCtphieuDatHang
{
    [Key]
    [Column("PK_FK_sMaPDH")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaPdh { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("iSL")]
    public int? ISl { get; set; }

    [ForeignKey("PkFkSMaPdh")]
    [InverseProperty("TblCtphieuDatHangs")]
    public virtual TblPhieuDatHang PkFkSMaPdhNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCtphieuDatHangs")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

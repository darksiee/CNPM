using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaBbh", "PkFkSMaSp")]
[Table("tbl_CTBienBanHuy")]
public partial class TblCtbienBanHuy
{
    [Key]
    [Column("PK_FK_sMaBBH")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaBbh { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("iSL")]
    public int? ISl { get; set; }

    [Column("sLyDo")]
    [StringLength(20)]
    public string? SLyDo { get; set; }

    [ForeignKey("PkFkSMaBbh")]
    [InverseProperty("TblCtbienBanHuys")]
    public virtual TblBienBanHuy PkFkSMaBbhNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCtbienBanHuys")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

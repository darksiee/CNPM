using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaBbk", "PkFkSMaSp")]
[Table("tbl_CTBienBanKiemKe")]
public partial class TblCtbienBanKiemKe
{
    [Key]
    [Column("PK_FK_sMaBBK")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaBbk { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("iSL")]
    public int? ISl { get; set; }

    [ForeignKey("PkFkSMaBbk")]
    [InverseProperty("TblCtbienBanKiemKes")]
    public virtual TblBienBanKiemKe PkFkSMaBbkNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCtbienBanKiemKes")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

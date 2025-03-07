using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[PrimaryKey("PkFkSMaHd", "PkFkSMaSp")]
[Table("tbl_CTHopDongCungCap")]
public partial class TblCthopDongCungCap
{
    [Key]
    [Column("PK_FK_sMaHD")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaHd { get; set; } = null!;

    [Key]
    [Column("PK_FK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkFkSMaSp { get; set; } = null!;

    [Column("fDonGia")]
    public double? FDonGia { get; set; }

    [ForeignKey("PkFkSMaHd")]
    [InverseProperty("TblCthopDongCungCaps")]
    public virtual TblHopDongCungCap PkFkSMaHdNavigation { get; set; } = null!;

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("TblCthopDongCungCaps")]
    public virtual TblSanPham PkFkSMaSpNavigation { get; set; } = null!;
}

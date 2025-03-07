using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_HopDongCungCap")]
public partial class TblHopDongCungCap
{
    [Key]
    [Column("PK_sMaHD")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaHd { get; set; } = null!;

    [Column("FK_sMaNCC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNcc { get; set; }

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [Column("dNgayBatDau", TypeName = "datetime")]
    public DateTime? DNgayBatDau { get; set; }

    [Column("dNgayKetThuc", TypeName = "datetime")]
    public DateTime? DNgayKetThuc { get; set; }

    [ForeignKey("FkSMaNcc")]
    [InverseProperty("TblHopDongCungCaps")]
    public virtual TblNhaCungCap? FkSMaNccNavigation { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblHopDongCungCaps")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [InverseProperty("PkFkSMaHdNavigation")]
    public virtual ICollection<TblCthopDongCungCap> TblCthopDongCungCaps { get; set; } = new List<TblCthopDongCungCap>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_PhieuDatHang")]
public partial class TblPhieuDatHang
{
    [Key]
    [Column("PK_sMaPDH")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaPdh { get; set; } = null!;

    [Column("FK_sMaPYC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaPyc { get; set; }

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [Column("bTrangThai")]
    public bool? BTrangThai { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblPhieuDatHangs")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [ForeignKey("FkSMaPyc")]
    [InverseProperty("TblPhieuDatHangs")]
    public virtual TblPhieuYeuCau? FkSMaPycNavigation { get; set; }

    [InverseProperty("PkFkSMaPdhNavigation")]
    public virtual ICollection<TblCtphieuDatHang> TblCtphieuDatHangs { get; set; } = new List<TblCtphieuDatHang>();

    [InverseProperty("FkSMaPdhNavigation")]
    public virtual ICollection<TblPhieuGiaoHang> TblPhieuGiaoHangs { get; set; } = new List<TblPhieuGiaoHang>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_PhieuYeuCau")]
public partial class TblPhieuYeuCau
{
    [Key]
    [Column("PK_sMaPYC")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaPyc { get; set; } = null!;

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [Column("bTrangThai")]
    public bool? BTrangThai { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblPhieuYeuCaus")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [InverseProperty("PkFkSMaPycNavigation")]
    public virtual ICollection<TblCtphieuYeuCau> TblCtphieuYeuCaus { get; set; } = new List<TblCtphieuYeuCau>();

    [InverseProperty("FkSMaPycNavigation")]
    public virtual ICollection<TblPhieuDatHang> TblPhieuDatHangs { get; set; } = new List<TblPhieuDatHang>();
}

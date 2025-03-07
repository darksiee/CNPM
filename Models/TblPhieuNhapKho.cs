using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_PhieuNhapKho")]
public partial class TblPhieuNhapKho
{
    [Key]
    [Column("PK_sMaPN")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaPn { get; set; } = null!;

    [Column("FK_sMaPGH")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaPgh { get; set; }

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblPhieuNhapKhos")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [ForeignKey("FkSMaPgh")]
    [InverseProperty("TblPhieuNhapKhos")]
    public virtual TblPhieuGiaoHang? FkSMaPghNavigation { get; set; }

    [InverseProperty("PkFkSMaPnNavigation")]
    public virtual ICollection<TblCtphieuNhapKho> TblCtphieuNhapKhos { get; set; } = new List<TblCtphieuNhapKho>();

    [InverseProperty("FkSMaPnNavigation")]
    public virtual ICollection<TblPhieuChi> TblPhieuChis { get; set; } = new List<TblPhieuChi>();
}

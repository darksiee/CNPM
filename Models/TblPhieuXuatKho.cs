using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_PhieuXuatKho")]
public partial class TblPhieuXuatKho
{
    [Key]
    [Column("PK_sMaPX")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaPx { get; set; } = null!;

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblPhieuXuatKhos")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [InverseProperty("PkFkSMaPxNavigation")]
    public virtual ICollection<TblCtphieuXuatKho> TblCtphieuXuatKhos { get; set; } = new List<TblCtphieuXuatKho>();
}

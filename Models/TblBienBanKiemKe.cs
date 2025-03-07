using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_BienBanKiemKe")]
public partial class TblBienBanKiemKe
{
    [Key]
    [Column("PK_sMaBBK")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaBbk { get; set; } = null!;

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [Column("sDiaDiemKiem")]
    [StringLength(100)]
    public string? SDiaDiemKiem { get; set; }

    [Column("dTgBatDau", TypeName = "datetime")]
    public DateTime? DTgBatDau { get; set; }

    [Column("dTgKetThuc", TypeName = "datetime")]
    public DateTime? DTgKetThuc { get; set; }

    [Column("bTrangThai")]
    public bool? BTrangThai { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblBienBanKiemKes")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [InverseProperty("PkFkSMaBbkNavigation")]
    public virtual ICollection<TblCtbienBanKiemKe> TblCtbienBanKiemKes { get; set; } = new List<TblCtbienBanKiemKe>();

    [ForeignKey("PkFkSMaBbk")]
    [InverseProperty("PkFkSMaBbks")]
    public virtual ICollection<TblNhanVien> PkFkSMaNguoiKiems { get; set; } = new List<TblNhanVien>();
}

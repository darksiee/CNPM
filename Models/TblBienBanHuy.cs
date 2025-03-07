using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_BienBanHuy")]
public partial class TblBienBanHuy
{
    [Key]
    [Column("PK_sMaBBH")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaBbh { get; set; } = null!;

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [Column("sDiaDiemHuy")]
    [StringLength(100)]
    public string? SDiaDiemHuy { get; set; }

    [Column("dTgBatDau", TypeName = "datetime")]
    public DateTime? DTgBatDau { get; set; }

    [Column("dTgKetThuc", TypeName = "datetime")]
    public DateTime? DTgKetThuc { get; set; }

    [Column("sPhuongThucHuy")]
    [StringLength(20)]
    public string? SPhuongThucHuy { get; set; }

    [Column("bTrangThai")]
    public bool? BTrangThai { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblBienBanHuys")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [InverseProperty("PkFkSMaBbhNavigation")]
    public virtual ICollection<TblCtbienBanHuy> TblCtbienBanHuys { get; set; } = new List<TblCtbienBanHuy>();

    [ForeignKey("PkFkMaBbh")]
    [InverseProperty("PkFkMaBbhs")]
    public virtual ICollection<TblNhanVien> PkFkSMaNguoiHuys { get; set; } = new List<TblNhanVien>();
}

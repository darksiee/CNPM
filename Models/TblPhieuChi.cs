using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_PhieuChi")]
public partial class TblPhieuChi
{
    [Key]
    [Column("PK_sMaPChi")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaPchi { get; set; } = null!;

    [Column("FK_sMaPN")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaPn { get; set; }

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [Column("sHinhThucTT")]
    [StringLength(20)]
    public string? SHinhThucTt { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblPhieuChis")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [ForeignKey("FkSMaPn")]
    [InverseProperty("TblPhieuChis")]
    public virtual TblPhieuNhapKho? FkSMaPnNavigation { get; set; }

    [ForeignKey("PkFkSMaPchi")]
    [InverseProperty("PkFkSMaPchis")]
    public virtual ICollection<TblSanPham> PkFkSMaSps { get; set; } = new List<TblSanPham>();

}

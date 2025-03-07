using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_BaoCaoThuChi")]
public partial class TblBaoCaoThuChi
{
    [Key]
    [Column("PK_sMaBC")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaBc { get; set; } = null!;

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [Column("dTgBatDau", TypeName = "datetime")]
    public DateTime? DTgBatDau { get; set; }

    [Column("dTgKetThuc", TypeName = "datetime")]
    public DateTime? DTgKetThuc { get; set; }

    [Column("bTrangThai")]
    public bool? BTrangThai { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblBaoCaoThuChis")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [ForeignKey("PkFkSMaBc")]
    [InverseProperty("PkFkSMaBcs")]
    public virtual ICollection<TblSanPham> PkFkSMaSps { get; set; } = new List<TblSanPham>();
}

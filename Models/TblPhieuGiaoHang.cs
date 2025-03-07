using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_PhieuGiaoHang")]
public partial class TblPhieuGiaoHang
{
    [Key]
    [Column("PK_sMaPGH")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaPgh { get; set; } = null!;

    [Column("FK_sMaPDH")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaPdh { get; set; }

    [Column("dTgLap", TypeName = "datetime")]
    public DateTime? DTgLap { get; set; }

    [Column("FK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNv { get; set; }

    [Column("sHoTenGH")]
    [StringLength(50)]
    public string? SHoTenGh { get; set; }

    [Column("sSDTGH")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SSdtgh { get; set; }

    [ForeignKey("FkSMaNv")]
    [InverseProperty("TblPhieuGiaoHangs")]
    public virtual TblNhanVien? FkSMaNvNavigation { get; set; }

    [ForeignKey("FkSMaPdh")]
    [InverseProperty("TblPhieuGiaoHangs")]
    public virtual TblPhieuDatHang? FkSMaPdhNavigation { get; set; }

    [InverseProperty("PkFkSMaPghNavigation")]
    public virtual ICollection<TblCtphieuGiaoHang> TblCtphieuGiaoHangs { get; set; } = new List<TblCtphieuGiaoHang>();

    [InverseProperty("FkSMaPghNavigation")]
    public virtual ICollection<TblPhieuNhapKho> TblPhieuNhapKhos { get; set; } = new List<TblPhieuNhapKho>();
}

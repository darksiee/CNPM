using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_NhanVien")]
public partial class TblNhanVien
{
    [Key]
    [Column("PK_sMaNV")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaNv { get; set; } = null!;

    [Column("FK_sMaTK")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaTk { get; set; }

    [Column("sHoTen")]
    [StringLength(50)]
    public string? SHoTen { get; set; }

    [Column("dNgaySinh", TypeName = "datetime")]
    public DateTime? DNgaySinh { get; set; }

    [Column("sCCCD")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SCccd { get; set; }

    [Column("sSDT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SSdt { get; set; }

    [Column("dNgayVaoLam", TypeName = "datetime")]
    public DateTime? DNgayVaoLam { get; set; }

    [Column("FK_sMaCV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaCv { get; set; }

    [Column("bTrangThai")]
    public bool? BTrangThai { get; set; }

    [ForeignKey("FkSMaCv")]
    [InverseProperty("TblNhanViens")]
    public virtual TblChucVu? FkSMaCvNavigation { get; set; }

    [ForeignKey("FkSMaTk")]
    [InverseProperty("TblNhanViens")]
    public virtual TblTaiKhoan? FkSMaTkNavigation { get; set; }

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblBaoCaoThuChi> TblBaoCaoThuChis { get; set; } = new List<TblBaoCaoThuChi>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblBienBanHuy> TblBienBanHuys { get; set; } = new List<TblBienBanHuy>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblBienBanKiemKe> TblBienBanKiemKes { get; set; } = new List<TblBienBanKiemKe>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblHopDongCungCap> TblHopDongCungCaps { get; set; } = new List<TblHopDongCungCap>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblPhieuChi> TblPhieuChis { get; set; } = new List<TblPhieuChi>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblPhieuDatHang> TblPhieuDatHangs { get; set; } = new List<TblPhieuDatHang>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblPhieuGiaoHang> TblPhieuGiaoHangs { get; set; } = new List<TblPhieuGiaoHang>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblPhieuNhapKho> TblPhieuNhapKhos { get; set; } = new List<TblPhieuNhapKho>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblPhieuThu> TblPhieuThus { get; set; } = new List<TblPhieuThu>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblPhieuXuatKho> TblPhieuXuatKhos { get; set; } = new List<TblPhieuXuatKho>();

    [InverseProperty("FkSMaNvNavigation")]
    public virtual ICollection<TblPhieuYeuCau> TblPhieuYeuCaus { get; set; } = new List<TblPhieuYeuCau>();

    [ForeignKey("PkFkSMaNguoiHuy")]
    [InverseProperty("PkFkSMaNguoiHuys")]
    public virtual ICollection<TblBienBanHuy> PkFkMaBbhs { get; set; } = new List<TblBienBanHuy>();

    [ForeignKey("PkFkSMaNguoiKiem")]
    [InverseProperty("PkFkSMaNguoiKiems")]
    public virtual ICollection<TblBienBanKiemKe> PkFkSMaBbks { get; set; } = new List<TblBienBanKiemKe>();
}

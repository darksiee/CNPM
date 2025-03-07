using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_SanPham")]
public partial class TblSanPham
{
    [Key]
    [Column("PK_sMaSP")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaSp { get; set; } = null!;

    [Column("sTenSP")]
    [StringLength(100)]
    public string? STenSp { get; set; }

    [Column("sDonViTinh")]
    [StringLength(20)]
    public string? SDonViTinh { get; set; }

    [Column("sHanDung")]
    public DateOnly? SHanDung { get; set; }

    [Column("iSL")]
    public int? ISl { get; set; }

    [Column("fDonGiaBan")]
    public double? FDonGiaBan { get; set; }

    [Column("FK_sMaLoai")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaLoai { get; set; }

    [Column("FK_sMaNCC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FkSMaNcc { get; set; }

    [ForeignKey("FkSMaLoai")]
    [InverseProperty("TblSanPhams")]
    public virtual TblLoaiSanPham? FkSMaLoaiNavigation { get; set; }

    [ForeignKey("FkSMaNcc")]
    [InverseProperty("TblSanPhams")]
    public virtual TblNhaCungCap? FkSMaNccNavigation { get; set; }

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCtbienBanHuy> TblCtbienBanHuys { get; set; } = new List<TblCtbienBanHuy>();

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCtbienBanKiemKe> TblCtbienBanKiemKes { get; set; } = new List<TblCtbienBanKiemKe>();

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCthopDongCungCap> TblCthopDongCungCaps { get; set; } = new List<TblCthopDongCungCap>();

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCtphieuDatHang> TblCtphieuDatHangs { get; set; } = new List<TblCtphieuDatHang>();

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCtphieuGiaoHang> TblCtphieuGiaoHangs { get; set; } = new List<TblCtphieuGiaoHang>();

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCtphieuNhapKho> TblCtphieuNhapKhos { get; set; } = new List<TblCtphieuNhapKho>();

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCtphieuThu> TblCtphieuThus { get; set; } = new List<TblCtphieuThu>();

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCtphieuXuatKho> TblCtphieuXuatKhos { get; set; } = new List<TblCtphieuXuatKho>();

    [InverseProperty("PkFkSMaSpNavigation")]
    public virtual ICollection<TblCtphieuYeuCau> TblCtphieuYeuCaus { get; set; } = new List<TblCtphieuYeuCau>();

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("PkFkSMaSps")]
    public virtual ICollection<TblBaoCaoThuChi> PkFkSMaBcs { get; set; } = new List<TblBaoCaoThuChi>();

    [ForeignKey("PkFkSMaSp")]
    [InverseProperty("PkFkSMaSps")]
    public virtual ICollection<TblPhieuChi> PkFkSMaPchis { get; set; } = new List<TblPhieuChi>();
}

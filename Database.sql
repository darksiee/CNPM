USE NMCNPM
CREATE TABLE tbl_LoaiSanPham (
    PK_sMaLoai VARCHAR(20) PRIMARY KEY,
    sTenLoai NVARCHAR(100)
);

CREATE TABLE tbl_NhaCungCap (
    PK_sMaNCC VARCHAR(20) PRIMARY KEY,
    sTenNCC NVARCHAR(100),
    sDiaChi NVARCHAR(100),
    sSDT VARCHAR(20),
    sSoTK VARCHAR(20) -- Assumed length since VARCHAR() is invalid
);

CREATE TABLE tbl_KhachHang (
    PK_sMaKH VARCHAR(20) PRIMARY KEY,
    sTenKH NVARCHAR(50),
    sSDT VARCHAR(20)
);

CREATE TABLE tbl_ChucVu (
    PK_sMaCV VARCHAR(20) PRIMARY KEY,
    sTenCV NVARCHAR(50)
);

CREATE TABLE tbl_Quyen (
    PK_sMaQuyen VARCHAR(20) PRIMARY KEY,
    sTenQuyen NVARCHAR(20)
);

CREATE TABLE tbl_TaiKhoan (
    PK_sMaTK VARCHAR(20) PRIMARY KEY,
    sTenTK VARCHAR(20),
    sMK VARCHAR(20),
    FK_sMaQuyen VARCHAR(20),
    FOREIGN KEY (FK_sMaQuyen) REFERENCES tbl_Quyen(PK_sMaQuyen)
);

CREATE TABLE tbl_NhanVien (
    PK_sMaNV VARCHAR(20) PRIMARY KEY,
    FK_sMaTK VARCHAR(20),
    sHoTen NVARCHAR(50),
    dNgaySinh DATETIME,
    sCCCD VARCHAR(20),
    sSDT VARCHAR(20),
    dNgayVaoLam DATETIME,
    FK_sMaCV VARCHAR(20), 
    bTrangThai BIT,
    FOREIGN KEY (FK_sMaTK) REFERENCES tbl_TaiKhoan(PK_sMaTK),
    FOREIGN KEY (FK_sMaCV) REFERENCES tbl_ChucVu(PK_sMaCV)
);

CREATE TABLE tbl_SanPham (
    PK_sMaSP VARCHAR(20) PRIMARY KEY,
    sTenSP NVARCHAR(100),
    sDonViTinh NVARCHAR(20),
    sHanDung DATE,
    iSL INT,
    fDonGiaBan FLOAT,
    FK_sMaLoai VARCHAR(20),
    FK_sMaNCC VARCHAR(20),
    FOREIGN KEY (FK_sMaLoai) REFERENCES tbl_LoaiSanPham(PK_sMaLoai),
    FOREIGN KEY (FK_sMaNCC) REFERENCES tbl_NhaCungCap(PK_sMaNCC)
);

CREATE TABLE tbl_PhieuYeuCau (
    PK_sMaPYC VARCHAR(20) PRIMARY KEY,
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    bTrangThai BIT,
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTPhieuYeuCau (
    PK_FK_sMaPYC VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    iSLCan INT,
    iSLDuyet INT,
    PRIMARY KEY (PK_FK_sMaPYC, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaPYC) REFERENCES tbl_PhieuYeuCau(PK_sMaPYC),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_PhieuDatHang (
    PK_sMaPDH VARCHAR(20) PRIMARY KEY,
    FK_sMaPYC VARCHAR(20),
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    bTrangThai BIT,
    FOREIGN KEY (FK_sMaPYC) REFERENCES tbl_PhieuYeuCau(PK_sMaPYC),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTPhieuDatHang (
    PK_FK_sMaPDH VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    iSL INT,
    PRIMARY KEY (PK_FK_sMaPDH, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaPDH) REFERENCES tbl_PhieuDatHang(PK_sMaPDH),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_PhieuGiaoHang (
    PK_sMaPGH VARCHAR(20) PRIMARY KEY,
    FK_sMaPDH VARCHAR(20),
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    sHoTenGH NVARCHAR(50),
    sSDTGH VARCHAR(20),
    FOREIGN KEY (FK_sMaPDH) REFERENCES tbl_PhieuDatHang(PK_sMaPDH),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTPhieuGiaoHang (
    PK_FK_sMaPGH VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    iSL INT,
    PRIMARY KEY (PK_FK_sMaPGH, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaPGH) REFERENCES tbl_PhieuGiaoHang(PK_sMaPGH),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_PhieuNhapKho (
    PK_sMaPN VARCHAR(20) PRIMARY KEY,
    FK_sMaPGH VARCHAR(20),
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    FOREIGN KEY (FK_sMaPGH) REFERENCES tbl_PhieuGiaoHang(PK_sMaPGH),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTPhieuNhapKho (
    PK_FK_sMaPN VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    iSL INT,
    sGhiChu NVARCHAR(100),
    PRIMARY KEY (PK_FK_sMaPN, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaPN) REFERENCES tbl_PhieuNhapKho(PK_sMaPN),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_HopDongCungCap (
    PK_sMaHD VARCHAR(20) PRIMARY KEY,
    FK_sMaNCC VARCHAR(20),
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    dNgayBatDau DATETIME,
    dNgayKetThuc DATETIME,
    FOREIGN KEY (FK_sMaNCC) REFERENCES tbl_NhaCungCap(PK_sMaNCC),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTHopDongCungCap (
    PK_FK_sMaHD VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    fDonGia FLOAT,
    PRIMARY KEY (PK_FK_sMaHD, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaHD) REFERENCES tbl_HopDongCungCap(PK_sMaHD),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_PhieuChi (
    PK_sMaPChi VARCHAR(20) PRIMARY KEY,
    FK_sMaPN VARCHAR(20),
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    sHinhThucTT NVARCHAR(20),
    FOREIGN KEY (FK_sMaPN) REFERENCES tbl_PhieuNhapKho(PK_sMaPN),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTPhieuChi (
    PK_FK_sMaPChi VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    PRIMARY KEY (PK_FK_sMaPChi, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaPChi) REFERENCES tbl_PhieuChi(PK_sMaPChi),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_PhieuXuatKho (
    PK_sMaPX VARCHAR(20) PRIMARY KEY,
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTPhieuXuatKho (
    PK_FK_sMaPX VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    iSLYC INT,
    iSLX INT,
    sGhiChu NVARCHAR(100),
    PRIMARY KEY (PK_FK_sMaPX, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaPX) REFERENCES tbl_PhieuXuatKho(PK_sMaPX),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_PhieuThu (
    PK_sMaPT VARCHAR(20) PRIMARY KEY,
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    FK_sMaKH VARCHAR(20),
    sHinhThucTT NVARCHAR(20),
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV),
    FOREIGN KEY (FK_sMaKH) REFERENCES tbl_KhachHang(PK_sMaKH)
);

CREATE TABLE tbl_CTPhieuThu (
    PK_FK_sMaPT VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    iSL INT,
    PRIMARY KEY (PK_FK_sMaPT, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaPT) REFERENCES tbl_PhieuThu(PK_sMaPT),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_BienBanHuy (
    PK_sMaBBH VARCHAR(20) PRIMARY KEY,
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    sDiaDiemHuy NVARCHAR(100),
    dTgBatDau DATETIME,
    dTgKetThuc DATETIME,
    sPhuongThucHuy NVARCHAR(20),
    bTrangThai BIT,
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTBienBanHuy (
    PK_FK_sMaBBH VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    iSL INT,
    sLyDo NVARCHAR(20),
    PRIMARY KEY (PK_FK_sMaBBH, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaBBH) REFERENCES tbl_BienBanHuy(PK_sMaBBH),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_ThanhVienHuy (
    PK_FK_MaBBH VARCHAR(20),
    PK_FK_sMaNguoiHuy VARCHAR(20),
    PRIMARY KEY (PK_FK_MaBBH, PK_FK_sMaNguoiHuy),
    FOREIGN KEY (PK_FK_MaBBH) REFERENCES tbl_BienBanHuy(PK_sMaBBH),
    FOREIGN KEY (PK_FK_sMaNguoiHuy) REFERENCES tbl_NhanVien(PK_sMaNV) -- Assumed reference
);

CREATE TABLE tbl_BienBanKiemKe (
    PK_sMaBBK VARCHAR(20) PRIMARY KEY,
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    sDiaDiemKiem NVARCHAR(100),
    dTgBatDau DATETIME,
    dTgKetThuc DATETIME,
    bTrangThai BIT,
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTBienBanKiemKe (
    PK_FK_sMaBBK VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    iSL INT,
    PRIMARY KEY (PK_FK_sMaBBK, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaBBK) REFERENCES tbl_BienBanKiemKe(PK_sMaBBK),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);

CREATE TABLE tbl_ThanhVienKiemKe (
    PK_FK_sMaBBK VARCHAR(20),
    PK_FK_sMaNguoiKiem VARCHAR(20),
    PRIMARY KEY (PK_FK_sMaBBK, PK_FK_sMaNguoiKiem),
    FOREIGN KEY (PK_FK_sMaBBK) REFERENCES tbl_BienBanKiemKe(PK_sMaBBK),
    FOREIGN KEY (PK_FK_sMaNguoiKiem) REFERENCES tbl_NhanVien(PK_sMaNV) -- Assumed reference
);

CREATE TABLE tbl_BaoCaoThuChi (
    PK_sMaBC VARCHAR(20) PRIMARY KEY,
    dTgLap DATETIME,
    FK_sMaNV VARCHAR(20),
    dTgBatDau DATETIME,
    dTgKetThuc DATETIME,
    bTrangThai BIT,
    FOREIGN KEY (FK_sMaNV) REFERENCES tbl_NhanVien(PK_sMaNV)
);

CREATE TABLE tbl_CTBaoCaoThuChi (
    PK_FK_sMaBC VARCHAR(20),
    PK_FK_sMaSP VARCHAR(20),
    PRIMARY KEY (PK_FK_sMaBC, PK_FK_sMaSP),
    FOREIGN KEY (PK_FK_sMaBC) REFERENCES tbl_BaoCaoThuChi(PK_sMaBC),
    FOREIGN KEY (PK_FK_sMaSP) REFERENCES tbl_SanPham(PK_sMaSP)
);


INSERT INTO tbl_SanPham (PK_sMaSP, sTenSP, iSL, fDonGiaBan) VALUES ('SP001', 'Paracetamol', 100, 5000);
INSERT INTO tbl_SanPham (PK_sMaSP, sTenSP, iSL, fDonGiaBan) VALUES ('SP002', 'Panadol', 10, 500);
INSERT INTO tbl_KhachHang (PK_sMaKH, sTenKH, sSDT) VALUES ('KH001', 'Nguyen Van A', '0123456789');
INSERT INTO tbl_Quyen(PK_sMaQuyen, sTenQuyen) VALUES ('Q001','Nhan vien')
INSERT INTO tbl_Quyen(PK_sMaQuyen, sTenQuyen) VALUES ('Q002','Quan ly')
INSERT INTO tbl_ChucVu(PK_sMaCV, sTenCV) VALUES ('CV01','Thuc tap')
INSERT INTO tbl_ChucVu(PK_sMaCV, sTenCV) VALUES ('CV02','Nhan vien')
INSERT INTO tbl_ChucVu(PK_sMaCV, sTenCV) VALUES ('CV03','Quan ly')



INSERT INTO tbl_TaiKhoan (PK_sMaTK, sTenTK, sMK, FK_sMaQuyen) 
VALUES ('TK001', 'nhanvien1', 'password123', 'Q001');
INSERT INTO tbl_NhanVien (PK_sMaNV, FK_sMaTK, sHoTen) VALUES ('NV001', 'TK001', 'Nhan Vien A');

EXEC sp_help 'tbl_PhieuXuatKho'; -- Kiểm tra cột PK_sMaPX
EXEC sp_help 'tbl_PhieuThu';     -- Kiểm tra cột PK_sMaPT
EXEC sp_help 'tbl_CTPhieuXuatKho'; -- Kiểm tra cột PK_FK_sMaPX
EXEC sp_help 'tbl_CTPhieuThu';     -- Kiểm tra cột PK_FK_sMaPT



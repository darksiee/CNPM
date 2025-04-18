USE [master]
GO
/****** Object:  Database [NMCNPM]    Script Date: 10/03/2025 13:32:56 ******/
CREATE DATABASE [NMCNPM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NMCNPM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.PHONG\MSSQL\DATA\NMCNPM.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NMCNPM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.PHONG\MSSQL\DATA\NMCNPM_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NMCNPM] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NMCNPM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NMCNPM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NMCNPM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NMCNPM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NMCNPM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NMCNPM] SET ARITHABORT OFF 
GO
ALTER DATABASE [NMCNPM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NMCNPM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NMCNPM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NMCNPM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NMCNPM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NMCNPM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NMCNPM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NMCNPM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NMCNPM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NMCNPM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NMCNPM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NMCNPM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NMCNPM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NMCNPM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NMCNPM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NMCNPM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NMCNPM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NMCNPM] SET RECOVERY FULL 
GO
ALTER DATABASE [NMCNPM] SET  MULTI_USER 
GO
ALTER DATABASE [NMCNPM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NMCNPM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NMCNPM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NMCNPM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NMCNPM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NMCNPM] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NMCNPM', N'ON'
GO
ALTER DATABASE [NMCNPM] SET QUERY_STORE = ON
GO
ALTER DATABASE [NMCNPM] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NMCNPM]
GO
/****** Object:  Table [dbo].[tbl_BaoCaoThuChi]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BaoCaoThuChi](
	[PK_sMaBC] [varchar](20) NOT NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[dTgBatDau] [datetime] NULL,
	[dTgKetThuc] [datetime] NULL,
	[bTrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaBC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BienBanHuy]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BienBanHuy](
	[PK_sMaBBH] [varchar](20) NOT NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[sDiaDiemHuy] [nvarchar](100) NULL,
	[dTgBatDau] [datetime] NULL,
	[dTgKetThuc] [datetime] NULL,
	[sPhuongThucHuy] [nvarchar](20) NULL,
	[bTrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaBBH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BienBanKiemKe]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BienBanKiemKe](
	[PK_sMaBBK] [varchar](20) NOT NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[sDiaDiemKiem] [nvarchar](100) NULL,
	[dTgBatDau] [datetime] NULL,
	[dTgKetThuc] [datetime] NULL,
	[bTrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaBBK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_ChucVu]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ChucVu](
	[PK_sMaCV] [varchar](20) NOT NULL,
	[sTenCV] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaCV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTBaoCaoThuChi]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTBaoCaoThuChi](
	[PK_FK_sMaBC] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaBC] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTBienBanHuy]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTBienBanHuy](
	[PK_FK_sMaBBH] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[iSL] [int] NULL,
	[sLyDo] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaBBH] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTBienBanKiemKe]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTBienBanKiemKe](
	[PK_FK_sMaBBK] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[iSL] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaBBK] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTHopDongCungCap]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTHopDongCungCap](
	[PK_FK_sMaHD] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[fDonGia] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaHD] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTPhieuChi]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTPhieuChi](
	[PK_FK_sMaPChi] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaPChi] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTPhieuDatHang]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTPhieuDatHang](
	[PK_FK_sMaPDH] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[iSL] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaPDH] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTPhieuGiaoHang]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTPhieuGiaoHang](
	[PK_FK_sMaPGH] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[iSL] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaPGH] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTPhieuNhapKho]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTPhieuNhapKho](
	[PK_FK_sMaPN] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[iSL] [int] NULL,
	[sGhiChu] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaPN] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTPhieuThu]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTPhieuThu](
	[PK_FK_sMaPT] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[iSL] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaPT] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTPhieuXuatKho]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTPhieuXuatKho](
	[PK_FK_sMaPX] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[iSLYC] [int] NULL,
	[iSLX] [int] NULL,
	[sGhiChu] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaPX] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_CTPhieuYeuCau]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CTPhieuYeuCau](
	[PK_FK_sMaPYC] [varchar](20) NOT NULL,
	[PK_FK_sMaSP] [varchar](20) NOT NULL,
	[iSLCan] [int] NULL,
	[iSLDuyet] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaPYC] ASC,
	[PK_FK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_HopDongCungCap]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_HopDongCungCap](
	[PK_sMaHD] [varchar](20) NOT NULL,
	[FK_sMaNCC] [varchar](20) NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[dNgayBatDau] [datetime] NULL,
	[dNgayKetThuc] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_KhachHang]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_KhachHang](
	[PK_sMaKH] [varchar](20) NOT NULL,
	[sTenKH] [nvarchar](50) NULL,
	[sSDT] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_LoaiSanPham]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_LoaiSanPham](
	[PK_sMaLoai] [varchar](20) NOT NULL,
	[sTenLoai] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_NhaCungCap]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_NhaCungCap](
	[PK_sMaNCC] [varchar](20) NOT NULL,
	[sTenNCC] [nvarchar](100) NULL,
	[sDiaChi] [nvarchar](100) NULL,
	[sSDT] [varchar](20) NULL,
	[sSoTK] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_NhanVien]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_NhanVien](
	[PK_sMaNV] [varchar](20) NOT NULL,
	[FK_sMaTK] [varchar](20) NULL,
	[sHoTen] [nvarchar](50) NULL,
	[dNgaySinh] [datetime] NULL,
	[sCCCD] [varchar](20) NULL,
	[sSDT] [varchar](20) NULL,
	[dNgayVaoLam] [datetime] NULL,
	[FK_sMaCV] [varchar](20) NULL,
	[bTrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PhieuChi]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PhieuChi](
	[PK_sMaPChi] [varchar](20) NOT NULL,
	[FK_sMaPN] [varchar](20) NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[sHinhThucTT] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaPChi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PhieuDatHang]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PhieuDatHang](
	[PK_sMaPDH] [varchar](20) NOT NULL,
	[FK_sMaPYC] [varchar](20) NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[bTrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaPDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PhieuGiaoHang]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PhieuGiaoHang](
	[PK_sMaPGH] [varchar](20) NOT NULL,
	[FK_sMaPDH] [varchar](20) NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[sHoTenGH] [nvarchar](50) NULL,
	[sSDTGH] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaPGH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PhieuNhapKho]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PhieuNhapKho](
	[PK_sMaPN] [varchar](20) NOT NULL,
	[FK_sMaPGH] [varchar](20) NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaPN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PhieuThu]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PhieuThu](
	[PK_sMaPT] [varchar](20) NOT NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[FK_sMaKH] [varchar](20) NULL,
	[sHinhThucTT] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaPT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PhieuXuatKho]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PhieuXuatKho](
	[PK_sMaPX] [varchar](20) NOT NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaPX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PhieuYeuCau]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PhieuYeuCau](
	[PK_sMaPYC] [varchar](20) NOT NULL,
	[dTgLap] [datetime] NULL,
	[FK_sMaNV] [varchar](20) NULL,
	[bTrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaPYC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Quyen]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Quyen](
	[PK_sMaQuyen] [varchar](20) NOT NULL,
	[sTenQuyen] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_SanPham]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SanPham](
	[PK_sMaSP] [varchar](20) NOT NULL,
	[sTenSP] [nvarchar](100) NULL,
	[sDonViTinh] [nvarchar](20) NULL,
	[sHanDung] [date] NULL,
	[iSL] [int] NULL,
	[fDonGiaBan] [float] NULL,
	[FK_sMaLoai] [varchar](20) NULL,
	[FK_sMaNCC] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_TaiKhoan]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_TaiKhoan](
	[PK_sMaTK] [varchar](20) NOT NULL,
	[sTenTK] [varchar](20) NULL,
	[sMK] [varchar](20) NULL,
	[FK_sMaQuyen] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_sMaTK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_ThanhVienHuy]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ThanhVienHuy](
	[PK_FK_MaBBH] [varchar](20) NOT NULL,
	[PK_FK_sMaNguoiHuy] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_MaBBH] ASC,
	[PK_FK_sMaNguoiHuy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_ThanhVienKiemKe]    Script Date: 10/03/2025 13:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ThanhVienKiemKe](
	[PK_FK_sMaBBK] [varchar](20) NOT NULL,
	[PK_FK_sMaNguoiKiem] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PK_FK_sMaBBK] ASC,
	[PK_FK_sMaNguoiKiem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_ChucVu] ([PK_sMaCV], [sTenCV]) VALUES (N'CV01', N'Thuc tap')
INSERT [dbo].[tbl_ChucVu] ([PK_sMaCV], [sTenCV]) VALUES (N'CV02', N'Nhan vien')
INSERT [dbo].[tbl_ChucVu] ([PK_sMaCV], [sTenCV]) VALUES (N'CV03', N'Quan ly')
GO
INSERT [dbo].[tbl_CTPhieuThu] ([PK_FK_sMaPT], [PK_FK_sMaSP], [iSL]) VALUES (N'PT20250307234102', N'SP002', 2)
INSERT [dbo].[tbl_CTPhieuThu] ([PK_FK_sMaPT], [PK_FK_sMaSP], [iSL]) VALUES (N'PT20250310095034', N'SP001', 5)
INSERT [dbo].[tbl_CTPhieuThu] ([PK_FK_sMaPT], [PK_FK_sMaSP], [iSL]) VALUES (N'PT20250310101050', N'SP002', 2)
GO
INSERT [dbo].[tbl_CTPhieuXuatKho] ([PK_FK_sMaPX], [PK_FK_sMaSP], [iSLYC], [iSLX], [sGhiChu]) VALUES (N'PX20250307234102', N'SP002', 2, 2, N'Xuất bán')
INSERT [dbo].[tbl_CTPhieuXuatKho] ([PK_FK_sMaPX], [PK_FK_sMaSP], [iSLYC], [iSLX], [sGhiChu]) VALUES (N'PX20250310095034', N'SP001', 5, 5, N'Xuất bán')
INSERT [dbo].[tbl_CTPhieuXuatKho] ([PK_FK_sMaPX], [PK_FK_sMaSP], [iSLYC], [iSLX], [sGhiChu]) VALUES (N'PX20250310101050', N'SP002', 2, 2, N'Xuất tiêu huỷ')
GO
INSERT [dbo].[tbl_KhachHang] ([PK_sMaKH], [sTenKH], [sSDT]) VALUES (N'KH001', N'Nguyen Van A', N'0123456789')
INSERT [dbo].[tbl_KhachHang] ([PK_sMaKH], [sTenKH], [sSDT]) VALUES (N'KH002', N'Trần Hồng Phong', N'0398039936')
GO
INSERT [dbo].[tbl_LoaiSanPham] ([PK_sMaLoai], [sTenLoai]) VALUES (N'LSP001', N'Thuốc không kê đơn')
INSERT [dbo].[tbl_LoaiSanPham] ([PK_sMaLoai], [sTenLoai]) VALUES (N'LSP002', N'Thuốc kê đơn')
GO
INSERT [dbo].[tbl_NhaCungCap] ([PK_sMaNCC], [sTenNCC], [sDiaChi], [sSDT], [sSoTK]) VALUES (N'NCC001', N'Công ty dược Đức Minh', N'Hà Nội', N'0381232322', N'022221232132')
INSERT [dbo].[tbl_NhaCungCap] ([PK_sMaNCC], [sTenNCC], [sDiaChi], [sSDT], [sSoTK]) VALUES (N'NCC002', N'KENU', N'Hà Nội', N'0312323222', N'0123213322')
INSERT [dbo].[tbl_NhaCungCap] ([PK_sMaNCC], [sTenNCC], [sDiaChi], [sSDT], [sSoTK]) VALUES (N'NCC003', N'Organika', N'Cần Thơ', N'01232111212', N'0122323233')
INSERT [dbo].[tbl_NhaCungCap] ([PK_sMaNCC], [sTenNCC], [sDiaChi], [sSDT], [sSoTK]) VALUES (N'NCC004', N'AstraZeneca', N'Hoa Kỳ', N'012312321', N'024242424')
GO
INSERT [dbo].[tbl_NhanVien] ([PK_sMaNV], [FK_sMaTK], [sHoTen], [dNgaySinh], [sCCCD], [sSDT], [dNgayVaoLam], [FK_sMaCV], [bTrangThai]) VALUES (N'NV001', N'TK001', N'Nhan Vien A', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_NhanVien] ([PK_sMaNV], [FK_sMaTK], [sHoTen], [dNgaySinh], [sCCCD], [sSDT], [dNgayVaoLam], [FK_sMaCV], [bTrangThai]) VALUES (N'NV002', NULL, N'Tran Hong Phong', CAST(N'2004-08-28T00:00:00.000' AS DateTime), N'', N'0398039923', CAST(N'2025-03-06T00:00:00.000' AS DateTime), NULL, 1)
GO
INSERT [dbo].[tbl_PhieuThu] ([PK_sMaPT], [dTgLap], [FK_sMaNV], [FK_sMaKH], [sHinhThucTT]) VALUES (N'PT20250307234102', CAST(N'2025-03-07T23:41:02.443' AS DateTime), N'NV001', N'KH001', N'Tiền mặt')
INSERT [dbo].[tbl_PhieuThu] ([PK_sMaPT], [dTgLap], [FK_sMaNV], [FK_sMaKH], [sHinhThucTT]) VALUES (N'PT20250310095034', CAST(N'2025-03-10T09:50:34.767' AS DateTime), N'NV001', N'KH002', N'Tiền mặt')
INSERT [dbo].[tbl_PhieuThu] ([PK_sMaPT], [dTgLap], [FK_sMaNV], [FK_sMaKH], [sHinhThucTT]) VALUES (N'PT20250310101050', CAST(N'2025-03-10T10:10:50.693' AS DateTime), N'NV001', N'KH002', N'Chuyển khoản')
GO
INSERT [dbo].[tbl_PhieuXuatKho] ([PK_sMaPX], [dTgLap], [FK_sMaNV]) VALUES (N'PX20250307234102', CAST(N'2025-03-07T23:41:02.210' AS DateTime), N'NV001')
INSERT [dbo].[tbl_PhieuXuatKho] ([PK_sMaPX], [dTgLap], [FK_sMaNV]) VALUES (N'PX20250310095034', CAST(N'2025-03-10T09:50:34.683' AS DateTime), N'NV001')
INSERT [dbo].[tbl_PhieuXuatKho] ([PK_sMaPX], [dTgLap], [FK_sMaNV]) VALUES (N'PX20250310101050', CAST(N'2025-03-10T10:10:50.527' AS DateTime), N'NV001')
GO
INSERT [dbo].[tbl_Quyen] ([PK_sMaQuyen], [sTenQuyen]) VALUES (N'Q001', N'Nhan vien')
INSERT [dbo].[tbl_Quyen] ([PK_sMaQuyen], [sTenQuyen]) VALUES (N'Q002', N'Quan ly')
GO
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP001', N'Paracetamol', N'Viên', CAST(N'2025-12-31' AS Date), 95, 5000, N'LSP001', N'NCC001')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP002', N'Panadol', N'Vỉ', CAST(N'1970-01-01' AS Date), 6, 500, N'LSP002', N'NCC001')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP003', N'Kenu Herbrain', N'Hộp', CAST(N'2027-03-10' AS Date), 30, 180000, N'LSP001', N'NCC002')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP004', N'Organika Bedtime', N'Hộp', CAST(N'2028-03-12' AS Date), 10, 600000, N'LSP001', N'NCC003')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP005', N'Organika Spirulina', N'Hộp', CAST(N'2028-02-12' AS Date), 30, 650000, N'LSP001', N'NCC003')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP006', N'Organika Premium Liga', N'Hộp', CAST(N'2029-02-12' AS Date), 25, 600000, N'LSP001', N'NCC003')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP007', N'Organika Chelated', N'Hộp', CAST(N'2027-02-12' AS Date), 25, 700000, N'LSP001', N'NCC003')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP008', N'Organika Cholesterol', N'Hộp', CAST(N'2027-03-12' AS Date), 25, 400000, N'LSP001', N'NCC003')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP009', N'Organika Spirulina', N'Hộp', CAST(N'2029-03-12' AS Date), 30, 700000, N'LSP001', N'NCC003')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP010', N'Glucofast', N'Hộp', CAST(N'2026-03-12' AS Date), 10, 200000, N'LSP002', N'NCC004')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP011', N'Arimidex', N'Hộp', CAST(N'2023-02-12' AS Date), 5, 800000, N'LSP002', N'NCC004')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP012', N'Nolvadex-D', N'Hộp', CAST(N'2027-02-12' AS Date), 25, 800000, N'LSP002', N'NCC004')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP013', N'Hytinon 500mg', N'Hộp', CAST(N'2028-05-14' AS Date), 30, 900000, N'LSP002', N'NCC004')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP014', N'Casodex', N'Hộp', CAST(N'2028-04-12' AS Date), 20, 900000, N'LSP002', N'NCC004')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP015', N'Vastarel', N'Hộp', CAST(N'2027-02-12' AS Date), 20, 800000, N'LSP002', N'NCC004')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP016', N'Stadovas 5mg', N'Hộp', CAST(N'2029-02-16' AS Date), 15, 1200000, N'LSP002', N'NCC004')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP017', N'Concor Tab 5mg', N'Hộp', CAST(N'2026-02-12' AS Date), 10, 1500000, N'LSP002', N'NCC004')
INSERT [dbo].[tbl_SanPham] ([PK_sMaSP], [sTenSP], [sDonViTinh], [sHanDung], [iSL], [fDonGiaBan], [FK_sMaLoai], [FK_sMaNCC]) VALUES (N'SP018', N'Lipitor 10mg', N'Hộp', CAST(N'2024-02-12' AS Date), 12, 700000, N'LSP002', N'NCC004')
GO
INSERT [dbo].[tbl_TaiKhoan] ([PK_sMaTK], [sTenTK], [sMK], [FK_sMaQuyen]) VALUES (N'TK001', N'nhanvien1', N'password123', N'Q001')
INSERT [dbo].[tbl_TaiKhoan] ([PK_sMaTK], [sTenTK], [sMK], [FK_sMaQuyen]) VALUES (N'TK002', N'phong', N'123456', N'Q002')
INSERT [dbo].[tbl_TaiKhoan] ([PK_sMaTK], [sTenTK], [sMK], [FK_sMaQuyen]) VALUES (N'TK003', N'khach', N'123456', NULL)
GO
ALTER TABLE [dbo].[tbl_BaoCaoThuChi]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_BienBanHuy]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_BienBanKiemKe]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_CTBaoCaoThuChi]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaBC])
REFERENCES [dbo].[tbl_BaoCaoThuChi] ([PK_sMaBC])
GO
ALTER TABLE [dbo].[tbl_CTBaoCaoThuChi]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTBienBanHuy]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaBBH])
REFERENCES [dbo].[tbl_BienBanHuy] ([PK_sMaBBH])
GO
ALTER TABLE [dbo].[tbl_CTBienBanHuy]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTBienBanKiemKe]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaBBK])
REFERENCES [dbo].[tbl_BienBanKiemKe] ([PK_sMaBBK])
GO
ALTER TABLE [dbo].[tbl_CTBienBanKiemKe]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTHopDongCungCap]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaHD])
REFERENCES [dbo].[tbl_HopDongCungCap] ([PK_sMaHD])
GO
ALTER TABLE [dbo].[tbl_CTHopDongCungCap]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTPhieuChi]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaPChi])
REFERENCES [dbo].[tbl_PhieuChi] ([PK_sMaPChi])
GO
ALTER TABLE [dbo].[tbl_CTPhieuChi]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTPhieuDatHang]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaPDH])
REFERENCES [dbo].[tbl_PhieuDatHang] ([PK_sMaPDH])
GO
ALTER TABLE [dbo].[tbl_CTPhieuDatHang]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTPhieuGiaoHang]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaPGH])
REFERENCES [dbo].[tbl_PhieuGiaoHang] ([PK_sMaPGH])
GO
ALTER TABLE [dbo].[tbl_CTPhieuGiaoHang]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTPhieuNhapKho]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaPN])
REFERENCES [dbo].[tbl_PhieuNhapKho] ([PK_sMaPN])
GO
ALTER TABLE [dbo].[tbl_CTPhieuNhapKho]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTPhieuThu]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaPT])
REFERENCES [dbo].[tbl_PhieuThu] ([PK_sMaPT])
GO
ALTER TABLE [dbo].[tbl_CTPhieuThu]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTPhieuXuatKho]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaPX])
REFERENCES [dbo].[tbl_PhieuXuatKho] ([PK_sMaPX])
GO
ALTER TABLE [dbo].[tbl_CTPhieuXuatKho]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_CTPhieuYeuCau]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaPYC])
REFERENCES [dbo].[tbl_PhieuYeuCau] ([PK_sMaPYC])
GO
ALTER TABLE [dbo].[tbl_CTPhieuYeuCau]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaSP])
REFERENCES [dbo].[tbl_SanPham] ([PK_sMaSP])
GO
ALTER TABLE [dbo].[tbl_HopDongCungCap]  WITH CHECK ADD FOREIGN KEY([FK_sMaNCC])
REFERENCES [dbo].[tbl_NhaCungCap] ([PK_sMaNCC])
GO
ALTER TABLE [dbo].[tbl_HopDongCungCap]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_NhanVien]  WITH CHECK ADD FOREIGN KEY([FK_sMaTK])
REFERENCES [dbo].[tbl_TaiKhoan] ([PK_sMaTK])
GO
ALTER TABLE [dbo].[tbl_NhanVien]  WITH CHECK ADD FOREIGN KEY([FK_sMaCV])
REFERENCES [dbo].[tbl_ChucVu] ([PK_sMaCV])
GO
ALTER TABLE [dbo].[tbl_PhieuChi]  WITH CHECK ADD FOREIGN KEY([FK_sMaPN])
REFERENCES [dbo].[tbl_PhieuNhapKho] ([PK_sMaPN])
GO
ALTER TABLE [dbo].[tbl_PhieuChi]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_PhieuDatHang]  WITH CHECK ADD FOREIGN KEY([FK_sMaPYC])
REFERENCES [dbo].[tbl_PhieuYeuCau] ([PK_sMaPYC])
GO
ALTER TABLE [dbo].[tbl_PhieuDatHang]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_PhieuGiaoHang]  WITH CHECK ADD FOREIGN KEY([FK_sMaPDH])
REFERENCES [dbo].[tbl_PhieuDatHang] ([PK_sMaPDH])
GO
ALTER TABLE [dbo].[tbl_PhieuGiaoHang]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_PhieuNhapKho]  WITH CHECK ADD FOREIGN KEY([FK_sMaPGH])
REFERENCES [dbo].[tbl_PhieuGiaoHang] ([PK_sMaPGH])
GO
ALTER TABLE [dbo].[tbl_PhieuNhapKho]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_PhieuThu]  WITH CHECK ADD FOREIGN KEY([FK_sMaKH])
REFERENCES [dbo].[tbl_KhachHang] ([PK_sMaKH])
GO
ALTER TABLE [dbo].[tbl_PhieuThu]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_PhieuXuatKho]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_PhieuYeuCau]  WITH CHECK ADD FOREIGN KEY([FK_sMaNV])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_SanPham]  WITH CHECK ADD FOREIGN KEY([FK_sMaLoai])
REFERENCES [dbo].[tbl_LoaiSanPham] ([PK_sMaLoai])
GO
ALTER TABLE [dbo].[tbl_SanPham]  WITH CHECK ADD FOREIGN KEY([FK_sMaNCC])
REFERENCES [dbo].[tbl_NhaCungCap] ([PK_sMaNCC])
GO
ALTER TABLE [dbo].[tbl_TaiKhoan]  WITH CHECK ADD FOREIGN KEY([FK_sMaQuyen])
REFERENCES [dbo].[tbl_Quyen] ([PK_sMaQuyen])
GO
ALTER TABLE [dbo].[tbl_ThanhVienHuy]  WITH CHECK ADD FOREIGN KEY([PK_FK_MaBBH])
REFERENCES [dbo].[tbl_BienBanHuy] ([PK_sMaBBH])
GO
ALTER TABLE [dbo].[tbl_ThanhVienHuy]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaNguoiHuy])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
ALTER TABLE [dbo].[tbl_ThanhVienKiemKe]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaBBK])
REFERENCES [dbo].[tbl_BienBanKiemKe] ([PK_sMaBBK])
GO
ALTER TABLE [dbo].[tbl_ThanhVienKiemKe]  WITH CHECK ADD FOREIGN KEY([PK_FK_sMaNguoiKiem])
REFERENCES [dbo].[tbl_NhanVien] ([PK_sMaNV])
GO
USE [master]
GO
ALTER DATABASE [NMCNPM] SET  READ_WRITE 
GO

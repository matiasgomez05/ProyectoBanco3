USE [master]
GO
/****** Object:  Database [G5-homebanking]    Script Date: 8/11/2022 20:21:13 ******/
CREATE DATABASE [G5-homebanking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'G5-homebanking', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\G5-homebanking.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'G5-homebanking_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\G5-homebanking_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [G5-homebanking] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [G5-homebanking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [G5-homebanking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [G5-homebanking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [G5-homebanking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [G5-homebanking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [G5-homebanking] SET ARITHABORT OFF 
GO
ALTER DATABASE [G5-homebanking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [G5-homebanking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [G5-homebanking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [G5-homebanking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [G5-homebanking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [G5-homebanking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [G5-homebanking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [G5-homebanking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [G5-homebanking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [G5-homebanking] SET  DISABLE_BROKER 
GO
ALTER DATABASE [G5-homebanking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [G5-homebanking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [G5-homebanking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [G5-homebanking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [G5-homebanking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [G5-homebanking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [G5-homebanking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [G5-homebanking] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [G5-homebanking] SET  MULTI_USER 
GO
ALTER DATABASE [G5-homebanking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [G5-homebanking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [G5-homebanking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [G5-homebanking] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [G5-homebanking] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [G5-homebanking] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [G5-homebanking] SET QUERY_STORE = OFF
GO
USE [G5-homebanking]
GO
/****** Object:  Table [dbo].[caja]    Script Date: 8/11/2022 20:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[caja](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cbu] [int] NOT NULL,
	[saldo] [float] NOT NULL,
 CONSTRAINT [PK_caja] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movimiento]    Script Date: 8/11/2022 20:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movimiento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCaja] [int] NOT NULL,
	[detalle] [varchar](50) NULL,
	[monto] [float] NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [PK_movimiento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pago]    Script Date: 8/11/2022 20:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
	[monto] [float] NULL,
	[pagado] [bit] NULL,
	[metodo] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[plazoFijo]    Script Date: 8/11/2022 20:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plazoFijo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[titular] [int] NOT NULL,
	[monto] [float] NULL,
	[fechaIni] [datetime] NULL,
	[fechaFin] [datetime] NULL,
	[tasa] [float] NULL,
	[pagado] [bit] NULL,
	[cbu] [int] NULL,
 CONSTRAINT [PK_plazoFijo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tarjeta]    Script Date: 8/11/2022 20:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tarjeta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[titular] [int] NOT NULL,
	[numero] [int] NOT NULL,
	[codigo] [int] NOT NULL,
	[limite] [float] NULL,
	[consumos] [float] NULL,
 CONSTRAINT [PK_tarjeta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 8/11/2022 20:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dni] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[mail] [varchar](50) NULL,
	[password] [varchar](50) NOT NULL,
	[intentosFallidos] [int] NULL,
	[bloqueado] [bit] NULL,
	[esAdmin] [bit] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarioCaja]    Script Date: 8/11/2022 20:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarioCaja](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idCaja] [int] NOT NULL,
 CONSTRAINT [PK_usuarioCaja] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[caja] ON 

INSERT [dbo].[caja] ([id], [cbu], [saldo]) VALUES (1, 7000010, 0)
INSERT [dbo].[caja] ([id], [cbu], [saldo]) VALUES (2, 7000020, 0)
SET IDENTITY_INSERT [dbo].[caja] OFF
GO
SET IDENTITY_INSERT [dbo].[movimiento] ON 

INSERT [dbo].[movimiento] ([id], [idCaja], [detalle], [monto], [fecha]) VALUES (1, 1, N'Abona luz', 2000, CAST(N'2022-08-11T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[movimiento] OFF
GO
SET IDENTITY_INSERT [dbo].[pago] ON 

INSERT [dbo].[pago] ([id], [idUsuario], [nombre], [monto], [pagado], [metodo]) VALUES (1, 1, N'Luz', 2000, 1, N'Virtual')
INSERT [dbo].[pago] ([id], [idUsuario], [nombre], [monto], [pagado], [metodo]) VALUES (2, 1, N'Gas', 3000, 1, N'Presencial')
INSERT [dbo].[pago] ([id], [idUsuario], [nombre], [monto], [pagado], [metodo]) VALUES (3, 1, N'Telefono', 1500, 1, N'Virtual')
SET IDENTITY_INSERT [dbo].[pago] OFF
GO
SET IDENTITY_INSERT [dbo].[plazoFijo] ON 

INSERT [dbo].[plazoFijo] ([id], [titular], [monto], [fechaIni], [fechaFin], [tasa], [pagado], [cbu]) VALUES (1, 1, 0.1, CAST(N'2022-11-03T00:00:00.000' AS DateTime), CAST(N'2022-12-03T00:00:00.000' AS DateTime), 70.4, 0, 124)
INSERT [dbo].[plazoFijo] ([id], [titular], [monto], [fechaIni], [fechaFin], [tasa], [pagado], [cbu]) VALUES (2, 1, 1000.7, CAST(N'2022-10-03T00:00:00.000' AS DateTime), CAST(N'2022-11-03T00:00:00.000' AS DateTime), 70.5, 0, 124)
INSERT [dbo].[plazoFijo] ([id], [titular], [monto], [fechaIni], [fechaFin], [tasa], [pagado], [cbu]) VALUES (24, 1, 2500, CAST(N'2022-11-07T11:38:52.530' AS DateTime), CAST(N'2022-12-07T11:38:52.530' AS DateTime), 70, 0, 124)
SET IDENTITY_INSERT [dbo].[plazoFijo] OFF
GO
SET IDENTITY_INSERT [dbo].[tarjeta] ON 

INSERT [dbo].[tarjeta] ([id], [titular], [numero], [codigo], [limite], [consumos]) VALUES (2, 1, 1, 1000001, 5000, 0)
INSERT [dbo].[tarjeta] ([id], [titular], [numero], [codigo], [limite], [consumos]) VALUES (3, 1, 2, 1000002, 5000, 0)
SET IDENTITY_INSERT [dbo].[tarjeta] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([id], [dni], [nombre], [apellido], [mail], [password], [intentosFallidos], [bloqueado], [esAdmin]) VALUES (1, 123, N'Admin', N'Admin', N'admin@admin', N'123', 0, 0, 1)
INSERT [dbo].[usuario] ([id], [dni], [nombre], [apellido], [mail], [password], [intentosFallidos], [bloqueado], [esAdmin]) VALUES (2, 36522030, N'Matias', N'Gomez', N'matias_gomez05@hotmail.com', N'123', 0, 0, 0)
INSERT [dbo].[usuario] ([id], [dni], [nombre], [apellido], [mail], [password], [intentosFallidos], [bloqueado], [esAdmin]) VALUES (3, 1111, N'test', N'test', N'test@test', N'1111', 0, 0, 0)
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarioCaja] ON 

INSERT [dbo].[usuarioCaja] ([id], [idUsuario], [idCaja]) VALUES (1, 1, 1)
INSERT [dbo].[usuarioCaja] ([id], [idUsuario], [idCaja]) VALUES (2, 1, 2)
SET IDENTITY_INSERT [dbo].[usuarioCaja] OFF
GO
/****** Object:  Index [IX_plazoFijo]    Script Date: 8/11/2022 20:21:13 ******/
ALTER TABLE [dbo].[plazoFijo] ADD  CONSTRAINT [IX_plazoFijo] UNIQUE NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[movimiento]  WITH CHECK ADD  CONSTRAINT [FK_movimiento_caja] FOREIGN KEY([idCaja])
REFERENCES [dbo].[caja] ([id])
GO
ALTER TABLE [dbo].[movimiento] CHECK CONSTRAINT [FK_movimiento_caja]
GO
ALTER TABLE [dbo].[pago]  WITH CHECK ADD  CONSTRAINT [FK_pago_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[pago] CHECK CONSTRAINT [FK_pago_usuario]
GO
ALTER TABLE [dbo].[plazoFijo]  WITH CHECK ADD  CONSTRAINT [FK_plazoFijo_usuario] FOREIGN KEY([titular])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[plazoFijo] CHECK CONSTRAINT [FK_plazoFijo_usuario]
GO
ALTER TABLE [dbo].[tarjeta]  WITH CHECK ADD  CONSTRAINT [FK_tarjeta_usuario] FOREIGN KEY([titular])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[tarjeta] CHECK CONSTRAINT [FK_tarjeta_usuario]
GO
ALTER TABLE [dbo].[usuarioCaja]  WITH CHECK ADD  CONSTRAINT [FK_usuarioCaja_caja] FOREIGN KEY([idCaja])
REFERENCES [dbo].[caja] ([id])
GO
ALTER TABLE [dbo].[usuarioCaja] CHECK CONSTRAINT [FK_usuarioCaja_caja]
GO
ALTER TABLE [dbo].[usuarioCaja]  WITH CHECK ADD  CONSTRAINT [FK_usuarioCaja_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[usuarioCaja] CHECK CONSTRAINT [FK_usuarioCaja_usuario]
GO
USE [master]
GO
ALTER DATABASE [G5-homebanking] SET  READ_WRITE 
GO

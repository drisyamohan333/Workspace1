USE [master]
GO
/****** Object:  Database [EmployeeDbContext]    Script Date: 02/12/2017 03:23:15 PM ******/
CREATE DATABASE [EmployeeDbContext]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmployeeDbContext', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\EmployeeDbContext.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EmployeeDbContext_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\EmployeeDbContext_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EmployeeDbContext] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeeDbContext].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeeDbContext] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeDbContext] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeDbContext] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmployeeDbContext] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EmployeeDbContext] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeeDbContext] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET RECOVERY FULL 
GO
ALTER DATABASE [EmployeeDbContext] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeeDbContext] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeeDbContext] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmployeeDbContext] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmployeeDbContext] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EmployeeDbContext', N'ON'
GO
USE [EmployeeDbContext]
GO
/****** Object:  Table [dbo].[EmployeeManagers]    Script Date: 02/12/2017 03:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeManagers](
	[EmployeeManagerID] [int] IDENTITY(1,1) NOT NULL,
	[strManager] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.EmployeeManagers] PRIMARY KEY CLUSTERED 
(
	[EmployeeManagerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employees]    Script Date: 02/12/2017 03:23:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee_Name] [nvarchar](max) NULL,
	[Salary] [float] NOT NULL,
	[EmployeeManagerID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_EmployeeManagerID]    Script Date: 02/12/2017 03:23:15 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeManagerID] ON [dbo].[Employees]
(
	[EmployeeManagerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Employees_dbo.EmployeeManagers_EmployeeManagerID] FOREIGN KEY([EmployeeManagerID])
REFERENCES [dbo].[EmployeeManagers] ([EmployeeManagerID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_dbo.Employees_dbo.EmployeeManagers_EmployeeManagerID]
GO
USE [master]
GO
ALTER DATABASE [EmployeeDbContext] SET  READ_WRITE 
GO

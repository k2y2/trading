USE [master]
GO
/****** Object:  Database [trading]    Script Date: 6/16/2020 6:23:33 AM ******/
CREATE DATABASE [trading]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'trading', FILENAME = N'F:\data\trading.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'trading_log', FILENAME = N'G:\log\trading_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [trading] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [trading].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [trading] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [trading] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [trading] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [trading] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [trading] SET ARITHABORT OFF 
GO
ALTER DATABASE [trading] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [trading] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [trading] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [trading] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [trading] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [trading] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [trading] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [trading] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [trading] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [trading] SET  DISABLE_BROKER 
GO
ALTER DATABASE [trading] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [trading] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [trading] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [trading] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [trading] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [trading] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [trading] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [trading] SET RECOVERY FULL 
GO
ALTER DATABASE [trading] SET  MULTI_USER 
GO
ALTER DATABASE [trading] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [trading] SET DB_CHAINING OFF 
GO
ALTER DATABASE [trading] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [trading] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [trading] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'trading', N'ON'
GO
ALTER DATABASE [trading] SET QUERY_STORE = OFF
GO
USE [trading]
GO
/****** Object:  Table [dbo].[Introducer]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Introducer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[IntroducerName] [varchar](100) NOT NULL,
	[IntroducerLegalName] [varchar](100) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Introducer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[IntroducerView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[IntroducerView]
AS
SELECT id, IntroducerName, IntroducerLegalName, DateTimeModified, DateTimeAdded, '' AS AssociatedClient
FROM     dbo.Introducer
GO
/****** Object:  Table [dbo].[AccountType]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountType](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[AccountTypeName] [varchar](10) NOT NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IntroducerBankAccount]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IntroducerBankAccount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[IntroducerID] [int] NOT NULL,
	[AccountName] [varchar](100) NOT NULL,
	[CurrencyID] [tinyint] NULL,
	[AccountTypeID] [tinyint] NULL,
	[AccountHolderName] [varchar](100) NULL,
	[AccountHolderAddress] [varchar](500) NULL,
	[AccountNo] [varchar](100) NULL,
	[IBAN] [varchar](100) NULL,
	[BSB] [varchar](100) NULL,
	[BankCode] [varchar](100) NULL,
	[BankName] [varchar](100) NOT NULL,
	[SWIFT] [varchar](100) NOT NULL,
	[CountryID] [tinyint] NOT NULL,
	[BranchAddress] [varchar](500) NULL,
	[Reference] [varchar](500) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_IntroducerBankAccount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[CurrencyName] [char](5) NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](50) NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[IntroducerBankAccountView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[IntroducerBankAccountView]
AS
SELECT ib.id, ib.IntroducerID, ib.AccountName, ib.CurrencyID, ib.AccountTypeID, ib.AccountHolderName, ib.AccountHolderAddress, ib.AccountNo, ib.IBAN, ib.BSB, ib.BankCode, ib.BankName, ib.SWIFT, ib.CountryID, ib.BranchAddress, 
                  ib.Reference, ib.DateTimeModified, ib.DateTimeAdded, i.IntroducerName, c.CurrencyName, a.AccountTypeName, co.CountryName
FROM     dbo.IntroducerBankAccount AS ib LEFT OUTER JOIN
                  dbo.Introducer AS i ON ib.IntroducerID = i.id LEFT OUTER JOIN
                  dbo.Currency AS c ON ib.CurrencyID = c.id LEFT OUTER JOIN
                  dbo.AccountType AS a ON ib.AccountTypeID = a.id LEFT OUTER JOIN
                  dbo.Country AS co ON ib.CountryID = co.id
GO
/****** Object:  Table [dbo].[Client]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClientName] [varchar](100) NOT NULL,
	[ClientLegalName] [varchar](100) NULL,
	[ClientTypeID] [tinyint] NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_client] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientTradingProfile]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientTradingProfile](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[ClientTradingProfileName] [varchar](100) NOT NULL,
	[CurrencyPairID] [smallint] NOT NULL,
	[Price] [decimal](10, 4) NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_ClientTradingProfile] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientType]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientType](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[ClientTypeName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_ClientType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ClientView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ClientView]
AS
SELECT c.id, c.ClientName, c.ClientLegalName, c.ClientTypeID, c.DateTimeModified, c.DateTimeAdded, ct.ClientTypeName, ctp.ClientTradingProfileInfo
FROM     dbo.Client AS c LEFT OUTER JOIN
                  dbo.ClientType AS ct ON c.ClientTypeID = ct.id LEFT OUTER JOIN
                      (SELECT ClientID, STRING_AGG(ClientTradingProfileName, ',') AS ClientTradingProfileInfo
                       FROM      dbo.ClientTradingProfile
                       GROUP BY ClientID) AS ctp ON ctp.ClientID = c.id
GO
/****** Object:  Table [dbo].[CurrencyPair]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrencyPair](
	[id] [smallint] IDENTITY(1,1) NOT NULL,
	[CurrencyPairName] [varchar](10) NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_currencyPair] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ClientTradingProfileView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ClientTradingProfileView]
AS
SELECT ctp.id, ctp.ClientID, ctp.ClientTradingProfileName, ctp.CurrencyPairID, ctp.Price, ctp.DateTimeModified, ctp.DateTimeAdded, c.ClientName, cp.CurrencyPairName, '' AS IntroducerInfo
FROM     dbo.ClientTradingProfile AS ctp LEFT OUTER JOIN
                  dbo.Client AS c ON ctp.ClientID = c.id LEFT OUTER JOIN
                  dbo.CurrencyPair AS cp ON ctp.CurrencyPairID = cp.id
GO
/****** Object:  Table [dbo].[ClientBankAccount]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientBankAccount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[AccountName] [varchar](100) NOT NULL,
	[CurrencyID] [tinyint] NOT NULL,
	[AccountTypeID] [tinyint] NULL,
	[AccountHolderName] [varchar](100) NOT NULL,
	[AccountHolderAddress] [varchar](500) NOT NULL,
	[AccountNo] [varchar](100) NULL,
	[IBAN] [varchar](100) NULL,
	[BSB] [varchar](100) NULL,
	[BankCode] [varchar](100) NULL,
	[BankName] [varchar](100) NOT NULL,
	[SWIFT] [varchar](100) NOT NULL,
	[CountryID] [tinyint] NOT NULL,
	[BranchAddress] [varchar](500) NULL,
	[Reference] [varchar](500) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_ClientBankAccount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ClientBankAccountView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ClientBankAccountView]
AS
SELECT cb.id, cb.ClientID, cb.AccountName, cb.CurrencyID, cb.AccountTypeID, cb.AccountHolderName, cb.AccountHolderAddress, cb.AccountNo, cb.IBAN, cb.BSB, cb.BankCode, cb.BankName, cb.SWIFT, cb.CountryID, cb.BranchAddress, 
                  cb.Reference, cb.DateTimeModified, cb.DateTimeAdded, c.ClientName, cu.CurrencyName, a.AccountTypeName, co.CountryName
FROM     dbo.ClientBankAccount AS cb LEFT OUTER JOIN
                  dbo.Client AS c ON cb.ClientID = c.id LEFT OUTER JOIN
                  dbo.Currency AS cu ON cb.CurrencyID = cu.id LEFT OUTER JOIN
                  dbo.AccountType AS a ON cb.AccountTypeID = a.id LEFT OUTER JOIN
                  dbo.Country AS co ON cb.CountryID = co.id
GO
/****** Object:  Table [dbo].[ClientTradingProfileIntroducerMap]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientTradingProfileIntroducerMap](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClientTradingProfileID] [int] NOT NULL,
	[IntroducerID] [int] NOT NULL,
	[IntroducerCommissionTypeID] [tinyint] NULL,
	[IntroducerCommissionRate] [decimal](7, 4) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_ClientTradingProfileIntroducerMap] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IntroducerCommissionType]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IntroducerCommissionType](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[IntroducerCommissionTypeName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_IntroducerCommissionType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ClientTradingProfileIntroducerMapView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ClientTradingProfileIntroducerMapView]
AS
SELECT map.id, map.ClientTradingProfileID, map.IntroducerID, map.IntroducerCommissionTypeID, map.IntroducerCommissionRate, map.DateTimeModified, map.DateTimeAdded, ctp.ClientTradingProfileName, i.IntroducerName, 
                  ict.IntroducerCommissionTypeName
FROM     dbo.ClientTradingProfileIntroducerMap AS map LEFT OUTER JOIN
                  dbo.ClientTradingProfile AS ctp ON map.ClientTradingProfileID = ctp.id LEFT OUTER JOIN
                  dbo.Introducer AS i ON map.IntroducerID = i.id LEFT OUTER JOIN
                  dbo.IntroducerCommissionType AS ict ON map.IntroducerCommissionTypeID = ict.id
GO
/****** Object:  Table [dbo].[Account]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [varchar](100) NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountBankAccount]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountBankAccount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [int] NOT NULL,
	[AccountName] [varchar](100) NOT NULL,
	[CurrencyID] [tinyint] NOT NULL,
	[AccountTypeID] [tinyint] NOT NULL,
	[AccountHolderName] [varchar](100) NOT NULL,
	[AccountHolderAddress] [varchar](500) NOT NULL,
	[AccountNo] [varchar](100) NULL,
	[IBAN] [varchar](100) NULL,
	[BSB] [varchar](100) NULL,
	[BankCode] [varchar](100) NULL,
	[BankName] [varchar](100) NOT NULL,
	[SWIFT] [varchar](100) NOT NULL,
	[CountryID] [tinyint] NOT NULL,
	[BranchAddress] [varchar](500) NOT NULL,
	[Reference] [varchar](500) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_AccountBankAccount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AccountBankAccountView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AccountBankAccountView]
AS
SELECT ab.id, ab.AccountID, ab.AccountName, ab.CurrencyID, ab.AccountTypeID, ab.AccountHolderName, ab.AccountHolderAddress, ab.AccountNo, ab.IBAN, ab.BSB, ab.BankCode, ab.BankName, ab.SWIFT, ab.CountryID, ab.BranchAddress, 
                  ab.Reference, ab.DateTimeModified, ab.DateTimeAdded, a.AccountName AS MasterAccountName, cu.CurrencyName, at.AccountTypeName, co.CountryName
FROM     dbo.AccountBankAccount AS ab LEFT OUTER JOIN
                  dbo.Account AS a ON ab.AccountID = a.id LEFT OUTER JOIN
                  dbo.Currency AS cu ON ab.CurrencyID = cu.id LEFT OUTER JOIN
                  dbo.AccountType AS at ON ab.AccountTypeID = at.id LEFT OUTER JOIN
                  dbo.Country AS co ON ab.CountryID = co.id
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provider](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProviderName] [varchar](50) NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_provider] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProviderBankAccount]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderBankAccount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProviderID] [int] NOT NULL,
	[AccountName] [varchar](100) NOT NULL,
	[CurrencyID] [tinyint] NOT NULL,
	[AccountTypeID] [tinyint] NULL,
	[AccountHolderName] [varchar](100) NOT NULL,
	[AccountHolderAddress] [varchar](500) NOT NULL,
	[AccountNo] [varchar](100) NULL,
	[IBAN] [varchar](100) NULL,
	[BSB] [varchar](100) NULL,
	[BankCode] [varchar](100) NULL,
	[BankName] [varchar](100) NOT NULL,
	[SWIFT] [varchar](100) NOT NULL,
	[CountryID] [tinyint] NOT NULL,
	[BranchAddress] [varchar](500) NULL,
	[Reference] [varchar](500) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_ProviderBankAccount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProviderBankAccountView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProviderBankAccountView]
AS
SELECT pb.id, pb.ProviderID, pb.AccountName, pb.CurrencyID, pb.AccountTypeID, pb.AccountHolderName, pb.AccountHolderAddress, pb.AccountNo, pb.IBAN, pb.BSB, pb.BankCode, pb.BankName, pb.SWIFT, pb.CountryID, pb.BranchAddress, 
                  pb.Reference, pb.DateTimeModified, pb.DateTimeAdded, p.ProviderName, cu.CurrencyName, at.AccountTypeName, co.CountryName
FROM     dbo.ProviderBankAccount AS pb LEFT OUTER JOIN
                  dbo.Provider AS p ON pb.ProviderID = p.id LEFT OUTER JOIN
                  dbo.Currency AS cu ON pb.CurrencyID = cu.id LEFT OUTER JOIN
                  dbo.AccountType AS at ON pb.AccountTypeID = at.id LEFT OUTER JOIN
                  dbo.Country AS co ON pb.CountryID = co.id
GO
/****** Object:  Table [dbo].[ProviderTradingProfile]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderTradingProfile](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProviderID] [int] NOT NULL,
	[ProviderTradingProfileName] [varchar](100) NOT NULL,
	[CurrencyPairID] [smallint] NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_ProviderTradingProfile] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProviderTradingProfileView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProviderTradingProfileView]
AS
SELECT pt.id, pt.ProviderID, pt.ProviderTradingProfileName, pt.CurrencyPairID, pt.DateTimeModified, pt.DateTimeAdded, p.ProviderName, cp.CurrencyPairName
FROM     dbo.ProviderTradingProfile AS pt LEFT OUTER JOIN
                  dbo.Provider AS p ON pt.ProviderID = p.id LEFT OUTER JOIN
                  dbo.CurrencyPair AS cp ON pt.CurrencyPairID = cp.id
GO
/****** Object:  Table [dbo].[ProviderTradingProfileIntroducerMap]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderTradingProfileIntroducerMap](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProviderTradingProfileID] [int] NOT NULL,
	[IntroducerID] [int] NOT NULL,
	[IntroducerCommissionTypeID] [tinyint] NULL,
	[IntroducerCommissionRate] [decimal](7, 4) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_ProviderTradingProfileIntroducerMap] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProviderTradingProfileIntroducerMapView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProviderTradingProfileIntroducerMapView]
AS
SELECT map.id, map.ProviderTradingProfileID, map.IntroducerID, map.IntroducerCommissionTypeID, map.IntroducerCommissionRate, map.DateTimeModified, map.DateTimeAdded, p.ProviderTradingProfileName, i.IntroducerName, 
                  ict.IntroducerCommissionTypeName
FROM     dbo.ProviderTradingProfileIntroducerMap AS map LEFT OUTER JOIN
                  dbo.ProviderTradingProfile AS p ON map.ProviderTradingProfileID = p.id LEFT OUTER JOIN
                  dbo.Introducer AS i ON map.IntroducerID = i.id LEFT OUTER JOIN
                  dbo.IntroducerCommissionType AS ict ON map.IntroducerCommissionTypeID = ict.id
GO
/****** Object:  View [dbo].[CurrencyPairView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CurrencyPairView]
AS
SELECT cp1.id, cp1.CurrencyPairName, cp2.id AS CurrencyPairID2, cp2.CurrencyPairName AS CurrencyPairName2, c1.id AS id1, trim(c1.CurrencyName) AS CurrencyName1, c2.id AS id2, trim(c2.CurrencyName) AS CurrencyName2
FROM     dbo.CurrencyPair AS cp1 LEFT OUTER JOIN
                  dbo.CurrencyPair AS cp2 ON RIGHT(cp1.CurrencyPairName, 3) + LEFT(cp1.CurrencyPairName, 3) = cp2.CurrencyPairName LEFT OUTER JOIN
                  dbo.Currency AS c1 ON LEFT(cp1.CurrencyPairName, 3) = c1.CurrencyName LEFT OUTER JOIN
                  dbo.Currency AS c2 ON RIGHT(cp1.CurrencyPairName, 3) = c2.CurrencyName
GO
/****** Object:  Table [dbo].[Txn]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Txn](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceNo] [varchar](20) NOT NULL,
	[Type] [char](1) NOT NULL,
	[LinkedDepositID] [int] NULL,
	[TradeDate] [date] NOT NULL,
	[ClientTradingProfileID] [int] NOT NULL,
	[Status] [char](1) NULL,
	[ClientPriceRate] [decimal](10, 4) NULL,
	[ClientCurrencyPairID] [smallint] NULL,
	[ClientDfrRate] [decimal](10, 4) NULL,
	[ClientUniqueDfr] [bit] NOT NULL,
	[ClientExRate] [decimal](10, 4) NULL,
	[ClientCurrencyIDIn] [tinyint] NOT NULL,
	[ClientAmountIn] [decimal](18, 4) NOT NULL,
	[ClientCurrencyIDOut] [tinyint] NULL,
	[ClientAmountOut] [decimal](18, 4) NULL,
	[ClientPayOutAccountID] [int] NULL,
	[ProviderTradingProfileID] [int] NULL,
	[ProviderCurrencyID] [tinyint] NULL,
	[ProviderCostDate] [date] NULL,
	[ProviderCostRate] [decimal](10, 4) NULL,
	[ProviderExpectedPayInAmount] [decimal](18, 4) NULL,
	[ProviderBankAccountID] [int] NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Txn] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TxnView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TxnView]
AS
SELECT t.id, t.ReferenceNo, t.Type, t.LinkedDepositID, t.TradeDate, t.ClientTradingProfileID, t.Status, t.ClientPriceRate, t.ClientCurrencyPairID, t.ClientDfrRate, t.ClientUniqueDfr, t.ClientExRate, t.ClientCurrencyIDIn, t.ClientAmountIn, 
                  t.ClientCurrencyIDOut, t.ClientAmountOut, t.ClientPayOutAccountID, t.ProviderTradingProfileID, t.ProviderCurrencyID, t.ProviderCostDate, t.ProviderCostRate, t.ProviderExpectedPayInAmount, t.ProviderBankAccountID, 
                  t.DateTimeModified, t.DateTimeAdded, t2.ReferenceNo AS LinkedDepositReferenceNo, cl.ClientTradingProfileName, cp.CurrencyPairName AS ClientCurrencyPairName, cri.CurrencyName AS ClientCurrencyNameIn, 
                  cro.CurrencyName AS ClientCurrencyNameOut, ac.AccountName AS ClientPayOutAccountName, p.ProviderTradingProfileName, crp.CurrencyName AS ProviderCurrencyName, pb.AccountName AS ProviderBankAccountName,
	
	iif(isnull(t.ClientPriceRate,0)=0,'PRICE ','')+
	iif(isnull(t.ClientDfrRate,0)=0,'DFR ','')+
	iif(isnull(t.ProviderCostRate,0)=0,'COST ','') as Alert
	 
 
FROM     dbo.Txn AS t LEFT OUTER JOIN
                  dbo.Txn AS t2 ON t.LinkedDepositID = t2.id LEFT OUTER JOIN
                  dbo.ClientTradingProfile AS cl ON t.ClientTradingProfileID = cl.id LEFT OUTER JOIN
                  dbo.CurrencyPair AS cp ON t.ClientCurrencyPairID = cp.id LEFT OUTER JOIN
                  dbo.Currency AS cri ON t.ClientCurrencyIDIn = cri.id LEFT OUTER JOIN
                  dbo.Currency AS cro ON t.ClientCurrencyIDOut = cro.id LEFT OUTER JOIN
                  dbo.Account AS ac ON t.ClientPayOutAccountID = ac.id LEFT OUTER JOIN
                  dbo.ProviderTradingProfile AS p ON t.ProviderTradingProfileID = p.id LEFT OUTER JOIN
                  dbo.Currency AS crp ON t.ProviderCurrencyID = crp.id LEFT OUTER JOIN
                  dbo.ProviderBankAccount AS pb ON t.ProviderBankAccountID = pb.id
GO
/****** Object:  Table [dbo].[Sender]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sender](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SenderName] [varchar](200) NOT NULL,
	[ProviderID] [int] NOT NULL,
	[Unit] [varchar](50) NULL,
	[AddressLine1] [varchar](200) NULL,
	[AddressLine2] [varchar](200) NULL,
	[City] [varchar](50) NULL,
	[Country] [varchar](20) NULL,
	[PostalCode] [varchar](20) NULL,
	[DirectorFirstName] [varchar](50) NULL,
	[DirectorLastName] [varchar](50) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Sender] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slip]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slip](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceNo] [varchar](20) NOT NULL,
	[ProviderTradingProfileID] [int] NOT NULL,
	[TxnID] [int] NULL,
	[AccountBankAccountID] [int] NOT NULL,
	[SenderID] [int] NULL,
	[SlipAmount] [decimal](18, 4) NOT NULL,
	[ActualAmount] [decimal](18, 4) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Slip] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SlipView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SlipView]
AS
SELECT s.id, s.ReferenceNo, s.ProviderTradingProfileID, s.TxnID, s.AccountBankAccountID, s.SenderID, s.SlipAmount, s.ActualAmount, s.DateTimeModified, s.DateTimeAdded, p.ProviderTradingProfileName, t.ReferenceNo AS TxnReferenceNo, 
                  a.AccountName AS AccountBankAccountName, sdr.SenderName
FROM     dbo.Slip AS s LEFT OUTER JOIN
                  dbo.ProviderTradingProfile AS p ON s.ProviderTradingProfileID = p.id LEFT OUTER JOIN
                  dbo.Txn AS t ON s.TxnID = t.id LEFT OUTER JOIN
                  dbo.AccountBankAccount AS a ON s.AccountBankAccountID = a.id LEFT OUTER JOIN
                  dbo.Sender AS sdr ON s.SenderID = sdr.id
GO
/****** Object:  View [dbo].[TxnCompleteView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TxnCompleteView]
AS
SELECT t1.id, t1.ReferenceNo, t1.Type, t1.LinkedDepositID, t1.TradeDate, t1.ClientTradingProfileID, t1.Status, t1.ClientPriceRate, t1.ClientCurrencyPairID, t1.ClientDfrRate, t1.ClientUniqueDfr, t1.ClientExRate, t1.ClientCurrencyIDIn, 
                  t1.ClientAmountIn, t1.ClientCurrencyIDOut, t1.ClientAmountOut, t1.ClientPayOutAccountID, t1.ProviderTradingProfileID, t1.ProviderCurrencyID, t1.ProviderCostDate, t1.ProviderCostRate, t1.ProviderExpectedPayInAmount, 
                  t1.ProviderBankAccountID, t1.DateTimeModified, t1.DateTimeAdded, t1.LinkedDepositReferenceNo, t1.ClientTradingProfileName, t1.ClientCurrencyPairName, t1.ClientCurrencyNameIn, t1.ClientCurrencyNameOut, 
                  t1.ClientPayOutAccountName, t1.ProviderTradingProfileName, t1.ProviderCurrencyName, t1.ProviderBankAccountName, ISNULL(t2.ProviderTradingProfileID, t1.ProviderTradingProfileID) AS LinkedProviderTradingProfileID, 
                  ISNULL(t2.ProviderTradingProfileName, t1.ProviderTradingProfileName) AS LinkedProviderTradingProfileName, ISNULL(t2.ProviderCurrencyID, t1.ProviderCurrencyID) AS LinkedProviderCurrencyID, ISNULL(t2.ProviderCurrencyName, 
                  t1.ProviderCurrencyName) AS LinkedProviderCurrencyName, ISNULL(t2.ProviderCostDate, t1.ProviderCostDate) AS LinkedProviderCostDate, ISNULL(t2.ProviderCostRate, t1.ProviderCostRate) AS LinkedProviderCostRate, 
                  ISNULL(t2.ProviderExpectedPayInAmount, t1.ProviderExpectedPayInAmount) AS LinkedProviderExpectedPayInAmount, ISNULL(t2.ProviderBankAccountID, t1.ProviderBankAccountID) AS LinkedProviderBankAccountID, 
                  ISNULL(t2.ProviderBankAccountName, t1.ProviderBankAccountName) AS LinkedProviderBankAccountName
FROM     dbo.TxnView AS t1 LEFT OUTER JOIN
                  dbo.TxnView AS t2 ON t1.LinkedDepositID = t2.id
GO
/****** Object:  Table [dbo].[Payout]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payout](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceNo] [varchar](20) NOT NULL,
	[TxnID] [int] NOT NULL,
	[ClientPayoutCurrencyID] [tinyint] NULL,
	[ClientPayoutAmount] [decimal](18, 4) NOT NULL,
	[ClientPayoutUSDRate] [decimal](10, 4) NULL,
	[ProviderPayinCurrencyID] [tinyint] NULL,
	[ProviderPayinAmount] [decimal](18, 4) NULL,
	[ProviderPayinUSDRate] [decimal](10, 4) NOT NULL,
	[UsedCurrencyID] [tinyint] NOT NULL,
	[UsedAmount] [decimal](18, 4) NOT NULL,
	[UsedClientPayoutFXRate] [decimal](10, 4) NOT NULL,
	[UsedUSDRate] [decimal](10, 4) NOT NULL,
	[AccountBankAccountID] [int] NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Payout] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PayoutView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PayoutView]
AS
SELECT p.id, p.ReferenceNo, p.TxnID, t.ClientCurrencyIDOut AS ClientPayoutCurrencyID, p.ClientPayoutAmount, p.ClientPayoutUSDRate, t.LinkedProviderCurrencyID AS ProviderPayinCurrencyID, 
                  t.LinkedProviderExpectedPayInAmount AS ProviderPayinAmount, p.ProviderPayinUSDRate, p.UsedCurrencyID, p.UsedAmount, p.UsedClientPayoutFXRate, p.UsedUSDRate, p.DateTimeModified, p.DateTimeAdded, 
                  t.ReferenceNo AS TxnReferenceNo, t.TradeDate, t.ClientCurrencyNameOut AS ClientPayoutCurrencyName, t.LinkedProviderCurrencyName AS ProviderPayinCurrencyName, uc.CurrencyName AS UsedCurrencyName, 
                  t.ClientTradingProfileID, t.ClientTradingProfileName, p.AccountBankAccountID, ab.AccountName
FROM     dbo.Payout AS p LEFT OUTER JOIN
                  dbo.TxnCompleteView AS t ON p.TxnID = t.id LEFT OUTER JOIN
                  dbo.Currency AS uc ON p.UsedCurrencyID = uc.id LEFT OUTER JOIN
                  dbo.AccountBankAccount AS ab ON p.AccountBankAccountID = ab.id
GO
/****** Object:  View [dbo].[ReportProvider]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReportProvider]
AS
WITH t(ProviderTradingProfileID, TotalProviderExpectedPayInAmount) AS (SELECT ProviderTradingProfileID, SUM(ISNULL(ProviderExpectedPayInAmount, 0)) AS TotalProviderExpectedPayInAmount
                                                                                                                                                    FROM      dbo.Txn
                                                                                                                                                    WHERE   (ProviderTradingProfileID IS NOT NULL)
                                                                                                                                                    GROUP BY ProviderTradingProfileID), s(ProviderTradingProfileID, TotalSlipAmount, TotalActualAmount) AS
    (SELECT ProviderTradingProfileID, SUM(ISNULL(SlipAmount, 0)) AS TotalSlipAmount, SUM(ISNULL(ActualAmount, 0)) AS TotalActualAmount
     FROM      dbo.Slip
     WHERE   (ProviderTradingProfileID IS NOT NULL)
     GROUP BY ProviderTradingProfileID)
    SELECT t_1.ProviderTradingProfileID, p.ProviderTradingProfileName, t_1.TotalProviderExpectedPayInAmount, ISNULL(s_1.TotalSlipAmount, 0) AS TotalSlipAmount, ISNULL(s_1.TotalActualAmount, 0) AS TotalActualAmount, 
                      t_1.TotalProviderExpectedPayInAmount - ISNULL(s_1.TotalSlipAmount, 0) AS MissingSlipAmount
    FROM     t AS t_1 LEFT OUTER JOIN
                      s AS s_1 ON t_1.ProviderTradingProfileID = s_1.ProviderTradingProfileID LEFT OUTER JOIN
                      dbo.ProviderTradingProfile AS p ON t_1.ProviderTradingProfileID = p.id
GO
/****** Object:  View [dbo].[ReportAccount]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReportAccount]
AS
SELECT a.id, a.AccountName, t.TotalClientAmountOut
FROM     dbo.Account AS a LEFT OUTER JOIN
                      (SELECT ClientPayOutAccountID, SUM(ClientAmountOut) AS TotalClientAmountOut
                       FROM      dbo.Txn
                       WHERE   (ClientPayOutAccountID IS NOT NULL)
                       GROUP BY ClientPayOutAccountID) AS t ON a.id = t.ClientPayOutAccountID
GO
/****** Object:  Table [dbo].[AccountAdjust]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountAdjust](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceNo] [varchar](20) NOT NULL,
	[AccountBankAccountID] [int] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[Reference] [varchar](200) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_AccountAdjust] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AccountAdjustView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AccountAdjustView]
AS
SELECT ad.id, ad.ReferenceNo, ad.AccountBankAccountID, ad.Amount, ad.Reference, ad.DateTimeModified, ad.DateTimeAdded, ab.AccountName
FROM     dbo.AccountAdjust AS ad LEFT OUTER JOIN
                  dbo.AccountBankAccount AS ab ON ad.AccountBankAccountID = ab.id
GO
/****** Object:  Table [dbo].[AccountTransfer]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTransfer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceNo] [varchar](20) NOT NULL,
	[AccountBankAccountIDFrom] [int] NOT NULL,
	[AccountBankAccountIDTo] [int] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[Rate] [decimal](10, 4) NOT NULL,
	[Reference] [varchar](200) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_AccountTransfer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AccountTransferView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AccountTransferView]
AS
SELECT at.id, at.ReferenceNo, at.AccountBankAccountIDFrom, at.AccountBankAccountIDTo, at.Amount, at.Rate, at.Reference, at.DateTimeModified, at.DateTimeAdded, abf.AccountName AS AccountBankAccountNameFrom, 
                  abt.AccountName AS AccountBankAccountNameTo
FROM     dbo.AccountTransfer AS at LEFT OUTER JOIN
                  dbo.AccountBankAccount AS abf ON at.AccountBankAccountIDFrom = abf.id LEFT OUTER JOIN
                  dbo.AccountBankAccount AS abt ON at.AccountBankAccountIDTo = abt.id
GO
/****** Object:  View [dbo].[ReportTxn]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReportTxn]
AS


WITH 
comm1 (ClientTradingProfileID, IntroducerCommissionRate) AS ( 
	select ClientTradingProfileID,sum(isnull(IntroducerCommissionRate,0))/100
	from ClientTradingProfileIntroducerMap
	where IntroducerCommissionTypeID=1
	GROUP BY ClientTradingProfileID
), 
comm2 (ClientTradingProfileID, IntroducerCommissionRate) AS ( 
	select ClientTradingProfileID,sum(isnull(IntroducerCommissionRate,0))/100
	from ClientTradingProfileIntroducerMap
	where IntroducerCommissionTypeID=2
	GROUP BY ClientTradingProfileID
), 
pout (TxnID, ClientTradingProfileID, ClientPayoutUSD,ClientPayoutOrig, IntroducerCommissionUSD2) AS ( 
	select p.TxnID,
	p.ClientTradingProfileID,
	sum(p.ClientPayoutAmount * p.UsedClientPayoutFXRate / p.UsedUSDRate) AS ClientPayoutUSD,
	sum(p.ClientPayoutAmount) AS ClientPayoutOrig,
	--sum((p.ClientPayoutAmount / p.ClientPayoutUSDRate) * isnull(comm2.IntroducerCommissionRate,0)) as IntroducerCommissionUSD2
	sum((p.ClientPayoutAmount * p.UsedClientPayoutFXRate / p.UsedUSDRate) * isnull(comm2.IntroducerCommissionRate,0)) as IntroducerCommissionUSD2
	from PayoutView p
	left outer join comm2 on p.ClientTradingProfileID = comm2.ClientTradingProfileID
	group by TxnID,p.ClientTradingProfileID
) 
,pin (TxnID, ProviderPayinUSD) AS ( 
	select TxnID, 
		   ProviderPayinAmount / p.ProviderPayinUSDRate
	from (select TxnID, 
				 ProviderPayinAmount,
				 ProviderPayinUSDRate, 
				 row_number() over(partition by TxnID order by DateTimeModified desc) as rn
		  from PayoutView) as p
	where rn = 1    
)

select i.TxnID, i.ProviderPayinUSD, o.ClientPayoutUSD, o.IntroducerCommissionUSD2, o.ClientPayoutOrig, t.ClientAmountOut-o.ClientPayoutOrig as ClientPayoutMissing, 
t.ReferenceNo, t.TradeDate,t.ClientTradingProfileName,t.ClientCurrencyNameOut,t.ClientAmountOut,
(i.ProviderPayinUSD - o.ClientPayoutUSD - o.IntroducerCommissionUSD2) * isnull(comm1.IntroducerCommissionRate,0) as IntroducerCommissionUSD1,
(i.ProviderPayinUSD - o.ClientPayoutUSD - o.IntroducerCommissionUSD2) * (1-isnull(comm1.IntroducerCommissionRate,0)) as GrossProfitUSD,

(i.ProviderPayinUSD - o.ClientPayoutUSD - o.IntroducerCommissionUSD2) * (1-isnull(comm1.IntroducerCommissionRate,0))*100/o.ClientPayoutUSD as GrossProfitUSDPct
from pin i
inner join pout o on i.TxnID=o.TxnID
left outer join comm1 on o.ClientTradingProfileID=comm1.ClientTradingProfileID
inner join TxnCompleteView t on i.TxnID=t.id

GO
/****** Object:  Table [dbo].[AccountTxn]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTxn](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [char](1) NOT NULL,
	[ReferenceID] [int] NOT NULL,
	[AccountBankAccountID] [int] NOT NULL,
	[AmountCredit] [decimal](18, 4) NULL,
	[AmountDebit] [decimal](18, 4) NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_AccountTxn] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AccountTxnView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AccountTxnView]
AS
SELECT tx.id, tx.Type, tx.ReferenceID, tx.AccountBankAccountID, tx.AmountCredit, tx.AmountDebit, tx.DateTimeModified, tx.DateTimeAdded, 
                  CASE tx.Type WHEN 'P' THEN p.ReferenceNo WHEN 'S' THEN s.ReferenceNo WHEN 'A' THEN ad.ReferenceNo WHEN 'T' THEN at.ReferenceNo ELSE '' END AS ReferenceNo, ab.AccountName
FROM     dbo.AccountTxn AS tx LEFT OUTER JOIN
                  dbo.Payout AS p ON tx.Type = 'P' AND tx.ReferenceID = p.id LEFT OUTER JOIN
                  dbo.Slip AS s ON tx.Type = 'S' AND tx.ReferenceID = s.id LEFT OUTER JOIN
                  dbo.AccountAdjust AS ad ON tx.Type = 'A' AND tx.ReferenceID = ad.id LEFT OUTER JOIN
                  dbo.AccountTransfer AS at ON tx.Type = 'T' AND tx.ReferenceID = at.id LEFT OUTER JOIN
                  dbo.AccountBankAccount AS ab ON tx.AccountBankAccountID = ab.id
GO
/****** Object:  Table [dbo].[Dfr]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dfr](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TradeDate] [date] NOT NULL,
	[CurrencyPairID] [smallint] NOT NULL,
	[Rate] [decimal](10, 4) NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_dfr] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ReportAccountBalance]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReportAccountBalance]
AS

with d (CurrencyPairID, Rate) AS ( 
	select CurrencyPairID, 
		   Rate
	from (select CurrencyPairID, 
				 Rate, 
				 row_number() over(partition by CurrencyPairID order by TradeDate desc) as rn
		  from Dfr) as d
	where rn = 1    
),
bal (AccountBankAccountID,Balance) AS ( 
select AccountBankAccountID, sum(isnull(AmountCredit,0))-Sum(isnull(AmountDebit,0)) as Balance
from AccountTxn 
group by AccountBankAccountID  
)
select ab.id, ab.AccountName, ab.CurrencyName, bal.Balance, bal.Balance*Rate as BalanceUSD,Rate 
from bal   
left outer join AccountBankAccountView ab on bal.AccountBankAccountID=ab.id
left outer join CurrencyPairView cp on ab.CurrencyID=cp.id1 and cp.CurrencyName2='USD'
left outer join d on cp.id = d.CurrencyPairID
GO
/****** Object:  View [dbo].[DfrLatestView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DfrLatestView]
AS
select CurrencyPairID, 
		   Rate
	from (select CurrencyPairID, 
				 Rate, 
				 row_number() over(partition by CurrencyPairID order by TradeDate desc) as rn
		  from Dfr) as d
	where rn = 1   
GO
/****** Object:  View [dbo].[ReportAccountPayable]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReportAccountPayable]
AS
WITH p(TxnID, ClientPayoutAmountTotal) AS (SELECT TxnID, SUM(ISNULL(ClientPayoutAmount, 0)) AS ClientPayoutAmountTotal
                                                                                         FROM      dbo.Payout
                                                                                         GROUP BY TxnID), t(ClientTradingProfileID, ClientTradingProfileName, ClientCurrencyIDOut, ClientCurrencyNameOut, TradeDate, ClientAmountUncompleted) AS
    (SELECT t.ClientTradingProfileID, t.ClientTradingProfileName, t.ClientCurrencyIDOut, t.ClientCurrencyNameOut, t.TradeDate, SUM(ISNULL(t.ClientAmountOut, 0) - ISNULL(p_1.ClientPayoutAmountTotal, 0)) AS ClientAmountUncompleted
     FROM      dbo.TxnView AS t LEFT OUTER JOIN
                       p AS p_1 ON t.id = p_1.TxnID
     GROUP BY t.ClientTradingProfileID, t.ClientTradingProfileName, t.ClientCurrencyIDOut, t.ClientCurrencyNameOut, t.TradeDate)
    SELECT t_1.ClientTradingProfileID, t_1.ClientTradingProfileName, t_1.ClientCurrencyNameOut, t_1.TradeDate, t_1.ClientAmountUncompleted, t_1.ClientAmountUncompleted * d.Rate AS ClientAmountUncompletedUSD, d.Rate
    FROM     t AS t_1 LEFT OUTER JOIN
                      dbo.CurrencyPairView AS cp ON t_1.ClientCurrencyIDOut = cp.id1 AND cp.CurrencyName2 = 'USD' LEFT OUTER JOIN
                      dbo.DfrLatestView AS d ON cp.id = d.CurrencyPairID
GO
/****** Object:  View [dbo].[ReportAccountReceivable]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReportAccountReceivable]
AS
WITH s(ProviderTradingProfileID, TotalSlipAmount) AS (SELECT ProviderTradingProfileID, SUM(ISNULL(SlipAmount, 0)) AS TotalSlipAmount
                                                                                                             FROM      dbo.Slip
                                                                                                             WHERE   (ProviderTradingProfileID IS NOT NULL) AND (ISNULL(ActualAmount, 0) = 0)
                                                                                                             GROUP BY ProviderTradingProfileID)
    SELECT r.ProviderTradingProfileID, r.ProviderTradingProfileName, ISNULL(r.MissingSlipAmount, 0) + ISNULL(s_1.TotalSlipAmount, 0) AS AmountReceivable, cp1.CurrencyName2, (ISNULL(r.MissingSlipAmount, 0) + ISNULL(s_1.TotalSlipAmount, 0)) 
                      * d.Rate AS AmountReceivableUSD, d.Rate
    FROM     dbo.ReportProvider AS r LEFT OUTER JOIN
                      s AS s_1 ON r.ProviderTradingProfileID = s_1.ProviderTradingProfileID LEFT OUTER JOIN
                      dbo.ProviderTradingProfile AS ptp ON r.ProviderTradingProfileID = ptp.id LEFT OUTER JOIN
                      dbo.CurrencyPairView AS cp1 ON ptp.CurrencyPairID = cp1.id LEFT OUTER JOIN
                      dbo.CurrencyPairView AS cp2 ON cp1.id2 = cp2.id1 AND cp2.CurrencyName2 = 'USD' LEFT OUTER JOIN
                      dbo.DfrLatestView AS d ON cp2.id = d.CurrencyPairID
GO
/****** Object:  View [dbo].[SenderView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SenderView]
AS
SELECT s.id, s.SenderName, s.ProviderID, s.Unit, s.AddressLine1, s.AddressLine2, s.City, s.Country, s.PostalCode, s.DirectorFirstName, s.DirectorLastName, s.DateTimeModified, s.DateTimeAdded, p.ProviderName
FROM     dbo.Sender AS s LEFT OUTER JOIN
                  dbo.Provider AS p ON s.ProviderID = p.id
GO
/****** Object:  View [dbo].[DfrView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DfrView]
AS
SELECT d.id, d.TradeDate, d.CurrencyPairID, d.Rate, d.DateTimeModified, d.DateTimeAdded, c.CurrencyPairName
FROM     dbo.Dfr AS d LEFT OUTER JOIN
                  dbo.CurrencyPair AS c ON d.CurrencyPairID = c.id
GO
/****** Object:  Table [dbo].[CostRate]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostRate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TradeDate] [date] NOT NULL,
	[ProviderTradingProfileID] [int] NOT NULL,
	[Rate] [decimal](10, 4) NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[DateTimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_costRate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CostRateView]    Script Date: 6/16/2020 6:23:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CostRateView]
AS
SELECT c.id, c.TradeDate, c.ProviderTradingProfileID, c.Rate, c.DateTimeModified, c.DateTimeAdded, p.ProviderTradingProfileName, iif(isnull(d .Rate, 0) = 0, NULL, c.Rate / d .Rate - 1) AS CostFXRatio
FROM     dbo.CostRate AS c LEFT OUTER JOIN
                  dbo.ProviderTradingProfile AS p ON c.ProviderTradingProfileID = p.id LEFT OUTER JOIN
                  dbo.Dfr AS d ON c.TradeDate = d .TradeDate AND p.CurrencyPairID = d .CurrencyPairID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ad"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ab"
            Begin Extent = 
               Top = 7
               Left = 317
               Bottom = 170
               Right = 565
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountAdjustView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountAdjustView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ab"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 296
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 344
               Bottom = 170
               Right = 565
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cu"
            Begin Extent = 
               Top = 7
               Left = 613
               Bottom = 170
               Right = 834
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "at"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 294
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "co"
            Begin Extent = 
               Top = 175
               Left = 317
               Bottom = 338
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Outp' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'ut = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "at"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 332
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "abf"
            Begin Extent = 
               Top = 7
               Left = 380
               Bottom = 170
               Right = 628
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "abt"
            Begin Extent = 
               Top = 7
               Left = 676
               Bottom = 170
               Right = 924
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountTransferView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountTransferView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tx"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 298
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 346
               Bottom = 170
               Right = 603
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 7
               Left = 651
               Bottom = 170
               Right = 910
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ad"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 298
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "at"
            Begin Extent = 
               Top = 175
               Left = 346
               Bottom = 338
               Right = 630
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ab"
            Begin Extent = 
               Top = 175
               Left = 678
               Bottom = 338
               Right = 926
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountTxnView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountTxnView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'AccountTxnView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cb"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 296
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 344
               Bottom = 170
               Right = 565
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cu"
            Begin Extent = 
               Top = 7
               Left = 613
               Bottom = 170
               Right = 834
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 882
               Bottom = 126
               Right = 1103
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "co"
            Begin Extent = 
               Top = 7
               Left = 1151
               Bottom = 170
               Right = 1372
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Outpu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N't = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "map"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 339
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ctp"
            Begin Extent = 
               Top = 7
               Left = 387
               Bottom = 170
               Right = 654
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 7
               Left = 702
               Bottom = 170
               Right = 941
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ict"
            Begin Extent = 
               Top = 7
               Left = 989
               Bottom = 126
               Right = 1305
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientTradingProfileIntroducerMapView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientTradingProfileIntroducerMapView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ctp"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 315
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 363
               Bottom = 170
               Right = 584
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 7
               Left = 632
               Bottom = 170
               Right = 853
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientTradingProfileView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientTradingProfileView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct"
            Begin Extent = 
               Top = 7
               Left = 317
               Bottom = 126
               Right = 522
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ctp"
            Begin Extent = 
               Top = 7
               Left = 570
               Bottom = 126
               Right = 823
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ClientView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CostRateView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CostRateView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cp1"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c1"
            Begin Extent = 
               Top = 7
               Left = 317
               Bottom = 170
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c2"
            Begin Extent = 
               Top = 7
               Left = 586
               Bottom = 170
               Right = 807
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp2"
            Begin Extent = 
               Top = 7
               Left = 855
               Bottom = 170
               Right = 1076
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CurrencyPairView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CurrencyPairView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DfrLatestView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DfrLatestView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "d"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 317
               Bottom = 170
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DfrView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DfrView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ib"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 296
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 7
               Left = 344
               Bottom = 170
               Right = 583
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 631
               Bottom = 170
               Right = 852
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 900
               Bottom = 126
               Right = 1121
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "co"
            Begin Extent = 
               Top = 7
               Left = 1169
               Bottom = 170
               Right = 1390
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IntroducerBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IntroducerBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IntroducerBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Introducer"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 287
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IntroducerView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IntroducerView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[9] 2[32] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 315
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 7
               Left = 363
               Bottom = 170
               Right = 705
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "uc"
            Begin Extent = 
               Top = 7
               Left = 710
               Bottom = 170
               Right = 931
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ab"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 296
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 21
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PayoutView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'76
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PayoutView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PayoutView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pb"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 296
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 344
               Bottom = 170
               Right = 565
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cu"
            Begin Extent = 
               Top = 7
               Left = 613
               Bottom = 170
               Right = 834
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "at"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 294
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "co"
            Begin Extent = 
               Top = 175
               Left = 317
               Bottom = 338
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Outp' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'ut = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderBankAccountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "map"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 339
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 387
               Bottom = 170
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 7
               Left = 719
               Bottom = 170
               Right = 958
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ict"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 294
               Right = 364
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderTradingProfileIntroducerMapView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderTradingProfileIntroducerMapView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pt"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 332
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 380
               Bottom = 170
               Right = 601
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 7
               Left = 649
               Bottom = 170
               Right = 870
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderTradingProfileView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderTradingProfileView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 7
               Left = 317
               Bottom = 126
               Right = 566
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccountBalance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccountBalance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cp"
            Begin Extent = 
               Top = 7
               Left = 372
               Bottom = 170
               Right = 597
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 7
               Left = 645
               Bottom = 126
               Right = 839
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t_1"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 324
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 2556
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccountPayable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccountPayable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "r"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 381
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ptp"
            Begin Extent = 
               Top = 7
               Left = 736
               Bottom = 170
               Right = 1020
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp1"
            Begin Extent = 
               Top = 126
               Left = 429
               Bottom = 289
               Right = 654
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp2"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 273
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 175
               Left = 702
               Bottom = 294
               Right = 896
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s_1"
            Begin Extent = 
               Top = 7
               Left = 429
               Bottom = 126
               Right = 688
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Wi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccountReceivable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'dth = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccountReceivable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportAccountReceivable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 736
               Bottom = 170
               Right = 1020
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t_1"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 126
               Right = 381
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s_1"
            Begin Extent = 
               Top = 7
               Left = 429
               Bottom = 148
               Right = 688
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportProvider'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportProvider'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[43] 4[6] 2[30] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportTxn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportTxn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "s"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 317
               Bottom = 170
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SenderView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SenderView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[38] 4[16] 2[28] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "s"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 175
               Left = 317
               Bottom = 338
               Right = 601
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t"
            Begin Extent = 
               Top = 7
               Left = 586
               Bottom = 170
               Right = 885
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 317
               Bottom = 170
               Right = 565
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sdr"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SlipView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SlipView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SlipView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[21] 2[28] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "t1"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 347
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t2"
            Begin Extent = 
               Top = 7
               Left = 395
               Bottom = 170
               Right = 694
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 34
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TxnCompleteView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TxnCompleteView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TxnCompleteView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[36] 4[14] 2[27] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "t"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 347
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t2"
            Begin Extent = 
               Top = 7
               Left = 395
               Bottom = 170
               Right = 694
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cl"
            Begin Extent = 
               Top = 7
               Left = 742
               Bottom = 170
               Right = 1009
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 7
               Left = 1011
               Bottom = 170
               Right = 1232
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cri"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cro"
            Begin Extent = 
               Top = 175
               Left = 317
               Bottom = 338
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ac"
            Begin Extent = 
               Top = 175
               Left = 586
               Bottom = 338
               Right = 807
            End
            DisplayFlags = 280
            TopColumn = 0
      ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TxnView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'   End
         Begin Table = "p"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 332
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "crp"
            Begin Extent = 
               Top = 175
               Left = 1124
               Bottom = 338
               Right = 1345
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pb"
            Begin Extent = 
               Top = 343
               Left = 380
               Bottom = 506
               Right = 628
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TxnView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TxnView'
GO
USE [master]
GO
ALTER DATABASE [trading] SET  READ_WRITE 
GO

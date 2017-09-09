/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2016
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [NewReleases]
GO

/****** Object:  Table [dbo].[ReleaseItems]    Script Date: 9/8/2017 11:15:03 PM ******/
DROP TABLE [dbo].[ReleaseItems]
GO

/****** Object:  Table [dbo].[ReleaseItems]    Script Date: 9/8/2017 11:15:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReleaseItems](
	[ReleaseDate] [datetime] NULL,
	[Category] [nvarchar](50) NULL,
	[Publisher] [nvarchar](500) NULL,
	[ItemCode] [nvarchar](50) NULL,
	[Title] [nvarchar](800) NULL,
	[Price] [decimal](18, 2) NULL,
	[Note] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



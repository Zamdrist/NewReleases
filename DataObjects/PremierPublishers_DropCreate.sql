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

/****** Object:  Table [dbo].[PremierPublishers]    Script Date: 9/8/2017 11:13:58 PM ******/
DROP TABLE [dbo].[PremierPublishers]
GO

/****** Object:  Table [dbo].[PremierPublishers]    Script Date: 9/8/2017 11:13:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PremierPublishers](
	[PremierPublisher] [nvarchar](800) NULL
) ON [PRIMARY]
GO


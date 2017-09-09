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

/****** Object:  StoredProcedure [dbo].[InsertReleaseItem]    Script Date: 9/8/2017 11:15:48 PM ******/
DROP PROCEDURE [dbo].[InsertReleaseItem]
GO

/****** Object:  StoredProcedure [dbo].[InsertReleaseItem]    Script Date: 9/8/2017 11:15:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertReleaseItem] 
@releaseDate datetime, @Category nvarchar(50), @Publisher nvarchar(500), @ItemCode nvarchar(50),
@Title nvarchar(800), @Price decimal(18,2),@Note nvarchar(max)

As

Insert Into ReleaseItems
Select @releaseDate, @Category, @Publisher, @ItemCode, @Title, @Price, @Note

GO



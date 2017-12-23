CREATE PROCEDURE [dbo].[InsertReleaseItem] 
@releaseDate datetime, @Category nvarchar(50), @Publisher nvarchar(500), @ItemCode nvarchar(50),
@Title nvarchar(800), @Price decimal(18,2),@Note nvarchar(max)

As

Insert Into ReleaseItems
Select @releaseDate, @Category, @Publisher, @ItemCode, @Title, @Price, @Note




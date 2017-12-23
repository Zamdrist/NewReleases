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



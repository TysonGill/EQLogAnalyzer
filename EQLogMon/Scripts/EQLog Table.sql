USE [DB_130918_bitistry]
GO

/****** Object:  Table [dbo].[EQLog]    Script Date: 12/9/2022 7:46:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EQLog](
	[ParseID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [varchar](20) NULL,
	[FileName] [varchar](100) NULL,
	[FileDate] [datetime] NULL,
	[FileSize] [int] NULL,
	[ParseDate] [datetime] NULL,
	[ParseMode] [varchar](20) NULL,
	[ParseCount] [int] NULL,
	[ParseSecs] [int] NULL,
 CONSTRAINT [PK_EQLog] PRIMARY KEY CLUSTERED 
(
	[ParseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


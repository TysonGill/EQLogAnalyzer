USE [DB_130918_bitistry]
GO

/****** Object:  Table [dbo].[EQLoggers]    Script Date: 12/9/2022 7:47:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EQLoggers](
	[LoggerID] [int] IDENTITY(1,1) NOT NULL,
	[FileOwner] [varchar](15) NOT NULL,
	[Expires] [datetime] NOT NULL,
 CONSTRAINT [PK_EQLoggerStatus] PRIMARY KEY CLUSTERED 
(
	[LoggerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [DB_130918_bitistry]
GO

/****** Object:  Table [dbo].[EQLogDamage]    Script Date: 12/9/2022 7:47:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EQLogDamage](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[LogUTCDate] [datetime] NOT NULL,
	[FileOwner] [varchar](15) NOT NULL,
	[Player] [varchar](15) NOT NULL,
	[Target] [varchar](50) NOT NULL,
	[DamageType] [varchar](15) NOT NULL,
	[Damage] [int] NOT NULL,
 CONSTRAINT [PK_Identity] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [DB_130918_bitistry]
GO

/****** Object:  StoredProcedure [dbo].[EQLogParse]    Script Date: 12/9/2022 7:49:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[EQLogParse](@LogUTCDate datetime, @FileOwner varchar(15), @Player varchar(15), @Target varchar(50), @DamageType varchar(15), @Damage int)
AS
BEGIN
	DECLARE @MinTime datetime;
	DECLARE @MaxTIme datetime;

	SET NOCOUNT ON;

	-- Determine the search window in which to check for duplicates
	SET @MinTime = DATEADD(ss, -1, @LogUTCDate);
	SET @MaxTIme = DATEADD(ss, +1, @LogUTCDate);

	-- Verify that someone else did not already report the message
	IF NOT EXISTS (SELECT * FROM [DB_130918_bitistry].dbo.EQLogDamage 
		WHERE LogUTCDate >= @MinTime AND LogUTCDate <= @MaxTime
		AND [FileOwner] <> @FileOwner
		AND [Player] = @Player
		AND [Target] = @Target
		AND [DamageType] = @DamageType
		AND [Damage] = @Damage)
		BEGIN
			INSERT INTO EQLogDamage ([LogUTCDate],[FileOwner],[Player],[Target],[DamageType],[Damage]) VALUES (@LogUTCDate, @FileOwner, @Player, @Target, @DamageType, @Damage)
		END
END
GO


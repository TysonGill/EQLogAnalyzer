USE [DB_130918_bitistry]
GO

/****** Object:  StoredProcedure [dbo].[EQLogAttack]    Script Date: 12/9/2022 7:48:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[EQLogAttack](@LocalDate datetime, @LogUTCDate datetime, @FileOwner varchar(15), @Player varchar(15), @Target varchar(50), @DamageType varchar(15), @Damage int)
AS
BEGIN
	DECLARE @MinTime datetime;
	DECLARE @MaxTIme datetime;
	DECLARE @TimeDiff int;
	DECLARE @ServerDate datetime;
	DECLARE @ActiveThrough datetime;

	SET NOCOUNT ON;
	SET @ServerDate = GETUTCDATE()
	SET @ActiveThrough = DATEADD(ss, 5, @ServerDate)

	-- Flag the fileowner as actively logging
	IF EXISTS (SELECT * FROM EQLoggers WHERE [FileOwner] = @FileOwner)
	BEGIN
		UPDATE EQLoggers SET [Expires] = @ActiveThrough WHERE [FileOwner] = @FileOwner 
	END
	ELSE
	BEGIN
		INSERT INTO EQLoggers ([FileOwner], [Expires]) VALUES (@FileOwner, @ActiveThrough)
	END

	-- Always reject records about another player who is actively logging
	IF @Player <> @FileOwner AND EXISTS (SELECT * FROM EQLoggers WHERE [FileOwner] = @Player AND [Expires] > @ServerDate)
	BEGIN
		RETURN
	END

	-- Always accept records from a player about themself
	IF @Player = @FileOwner
	BEGIN
		INSERT INTO EQLogDamage ([LogUTCDate],[FileOwner],[Player],[Target],[DamageType],[Damage]) VALUES (@LogUTCDate, @FileOwner, @Player, @Target, @DamageType, @Damage)
		RETURN
	END

	-- Get the time difference between the local computer and the server
	SELECT @TimeDiff = DATEDIFF(ss, @LocalDate, @ServerDate)

	-- Determine the search window in which to check for duplicates
	SET @MinTime = DATEADD(ss, @TimeDiff-1, @LogUTCDate);
	SET @MaxTIme = DATEADD(ss, @TimeDiff+1, @LogUTCDate);

	-- For a player reporting about another player, verify that someone else did not already report the message
	-- but allow duplicates if they come from the same logger
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


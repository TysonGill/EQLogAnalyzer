USE [DB_130918_bitistry]
GO

/****** Object:  StoredProcedure [dbo].[GetDPS]    Script Date: 12/9/2022 7:49:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetDPS] (@StartUTC datetime, @EndUTC datetime, @Target varchar(50), @IncludePets bit)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @RaidDam bigint
	DECLARE @DamageTable TABLE (Player varchar(15), Damage bigint)
	DECLARE @TotalsTable TABLE (Player varchar(15), TotDam bigint, MaxDam int, AvgDam int, NumHits int)
	DECLARE @FinalTable TABLE (Player varchar(15), TotDam bigint, MaxDam int, AvgDam int, NumHits int)

	-- Create a temporary table with the damage for each player
	INSERT INTO @DamageTable 
		SELECT [Player], [Damage]
		FROM EQLogDamage d
		WHERE [LogUTCDate] >= @StartUTC AND [LogUTCDate] <= @EndUTC
		AND ([Target] = @Target OR @Target = '')

	-- Create a temporary table with the damage totals for each player
	INSERT INTO @TotalsTable 
		SELECT [Player], SUM([Damage]) AS 'TotDam', MAX([Damage]) AS 'MaxDam', AVG([Damage]) AS 'AvgDam', COUNT(*) AS 'NumHits'
		FROM @DamageTable
		GROUP BY [Player]

	IF @IncludePets = 1
	BEGIN

		-- Merge pets with owner
		UPDATE @TotalsTable
			SET [Player] = (SELECT [Owner] FROM EQPets WHERE [Pet] = [Player])
			WHERE EXISTS (SELECT * FROM EQPets WHERE [Pet] = [Player])

		-- Roll up pet records with owner
		INSERT INTO @FinalTable
			SELECT [Player], SUM([TotDam]) AS 'TotDam', MAX([MaxDam]) AS 'MaxDam', AVG([AvgDam]) AS 'AvgDam', SUM([NumHits]) AS 'NumHits'
			FROM @TotalsTable
			GROUP BY [Player]

	END
	ELSE
	BEGIN

		-- Get final damage records
		INSERT INTO @FinalTable
			SELECT [Player], [TotDam], [MaxDam], [AvgDam], [NumHits]
			FROM @TotalsTable

	END

	-- Get the total raid damage
	SELECT @RaidDam = SUM([TotDam]) FROM @FinalTable
	
	-- Add % Damage to the damage table
	SELECT [Player] AS 'Damager', CAST(100.0*[TotDam]/@RaidDam AS Decimal(16,2)) AS '% Damage', [TotDam]/1000 AS 'Damage (1000''s)', [NumHits], [MaxDam], [AvgDam]
	FROM @FinalTable
	ORDER BY [% Damage] DESC

END
GO


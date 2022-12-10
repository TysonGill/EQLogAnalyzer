USE [DB_130918_bitistry]
GO

/****** Object:  StoredProcedure [dbo].[GetEncounters]    Script Date: 12/9/2022 7:50:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetEncounters]
AS
BEGIN

SET NOCOUNT ON;

-- Use this opportunity to purge the database
DELETE FROM EQLogDamage WHERE DATEDIFF(dd, [LogUTCDate], GETUTCDATE()) > 5;

-- Get the encounter records
WITH cteLag AS
(
SELECT LogID, LogUTCDate, LAG(LogUTCDate,1) OVER (ORDER BY LogUTCDate) PrevDate
FROM EQLogDamage
),
cteEncs AS
(
SELECT LogID, DATEDIFF(s, PrevDate, LogUTCDate) AS LagTime
FROM cteLag
),
cteGaps AS
(
SELECT LogID, ISNULL(LAG(LogID,1) OVER (ORDER BY LogID), 1) PrevID FROM cteEncs
WHERE LagTime > 360 -- seconds of inactivity
),
ctePeriods AS
(
SELECT PrevID AS 'EncStart', LogID-1 AS 'EncEnd' FROM cteGaps
WHERE LogID - PrevID > 200 -- minimum number ofrecords
),
cteFights AS
(
SELECT EncStart, EncEnd FROM ctePeriods
UNION
SELECT MAX(EncEnd)+1, (SELECT COUNT(*) FROM EQLogDamage) FROM ctePeriods
),
cteFinal AS
(
SELECT EncStart AS 'StartID', EncEnd AS 'EndID', ds.[LogUTCDate] AS 'Start Date', de.[LogUTCDate] AS 'End Date'
FROM cteFights f
JOIN EQLogDamage ds ON f.EncStart = ds.LogID
JOIN EQLogDamage de ON f.EncEnd = de.LogID
) 
SELECT StartID, EndID, [Start Date], [End Date],
(SELECT TOP 1 [Target] FROM EQLogDamage WHERE [DAMAGE] = (SELECT MAX([Damage]) FROM EQLogDamage WHERE LogID >= StartID AND LogID <= EndID)) AS [Target]
FROM cteFinal
ORDER BY [StartID]

END
GO


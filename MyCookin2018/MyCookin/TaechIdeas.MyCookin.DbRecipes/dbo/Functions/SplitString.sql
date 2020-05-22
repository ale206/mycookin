CREATE FUNCTION [dbo].[SplitString]
(
	@String		varchar(max)
,	@Separator	varchar(10)
)
RETURNS TABLE
AS RETURN
(
	WITH
	Split AS (
		SELECT
			LEFT(@String, CHARINDEX(@Separator, @String, 0) - 1) AS StringPart
		,	RIGHT(@String, LEN(@String) - CHARINDEX(@Separator, @String, 0)) AS RemainingString

		UNION ALL
		
		SELECT
			CASE
				WHEN CHARINDEX(@Separator, Split.RemainingString, 0) = 0 THEN Split.RemainingString
				ELSE LEFT(Split.RemainingString, CHARINDEX(@Separator, Split.RemainingString, 0) - 1)
			END AS StringPart
		,	CASE
				WHEN CHARINDEX(@Separator, Split.RemainingString, 0) = 0 THEN ''
				ELSE RIGHT(Split.RemainingString, LEN(Split.RemainingString) - CHARINDEX(@Separator, Split.RemainingString, 0))
			END AS RemainingString
		FROM
			Split
		WHERE
			Split.RemainingString <> ''
	)

	SELECT
		StringPart
	FROM
		Split
)


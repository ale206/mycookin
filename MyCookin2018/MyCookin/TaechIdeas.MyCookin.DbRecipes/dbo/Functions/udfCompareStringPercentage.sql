CREATE FUNCTION [dbo].udfCompareStringPercentage
(
	@String1		varchar(max),	
	@String2	varchar(max)
)
RETURNS INT

AS BEGIN
DECLARE @WordCount INT  
DECLARE @Word1 INT
DECLARE @return INT

SET @String1 = @String1 + ' |'
SET @String2 = @String2 + ' |'

select @Word1 =  Count(StringPart) from SplitString(@String1,' ') 

select @WordCount = COUNT(*) from
(select distinct * FROM SplitString(@String1,' ')) t1 INNER JOIN (select distinct * FROM SplitString(@String2,' ')) t2
ON t1.StringPart=t2.StringPart

select @return = (@WordCount/convert(float,@Word1))*100

RETURN @return
END
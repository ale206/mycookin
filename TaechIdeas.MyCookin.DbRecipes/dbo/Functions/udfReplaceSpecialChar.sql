CREATE FUNCTION [dbo].[udfReplaceSpecialChar]
(
	@String1		nvarchar(max),	
	@String2	nvarchar(10)
)
RETURNS nvarchar(max)

AS BEGIN

WHILE PATINDEX( '%[~,@,#,$,%,&,*,(,),",<,>]%', @String1 ) > 0 
	BEGIN
		SET @String1 = REPLACE( @String1, SUBSTRING( @String1, PATINDEX( '%[~,@,#,$,%,&,*,(,),",<,>]%', @String1 ), 1 ),@String2)
	END

RETURN @String1
END
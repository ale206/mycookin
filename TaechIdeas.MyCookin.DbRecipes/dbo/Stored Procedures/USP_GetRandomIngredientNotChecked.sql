-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 21/06/2012>
-- Description:	<Description, GetRandomIngredient>
-- =============================================
CREATE PROCEDURE [dbo].USP_GetRandomIngredientNotChecked(@IDIngredient uniqueidentifier OUTPUT)
AS
BEGIN

	DECLARE @SqlStatment nvarchar(MAX)
	DECLARE @SqlOutput nvarchar(150)
	
	SET @SqlStatment='SELECT TOP 1 @IDIng = IDIngredient 
					FROM dbo.Ingredients WHERE Checked = 0 ORDER BY NEWID()'
					
	SET @SqlOutput = '@IDIng uniqueidentifier OUTPUT'
				
	--select 	@SqlStatment		
	EXEC sp_executesql @SqlStatment, @SqlOutput, @IDIngredient OUTPUT
	

END

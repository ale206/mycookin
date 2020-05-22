

-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, USP_GetIngredientLanguageByIDIngredientIDLanguage>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIngredientLanguageByIDIngredientIDLanguage](@IDIngredient uniqueidentifier, @IDLanguage INT)
AS
BEGIN
	DECLARE @testExist int
	
	IF EXISTS (SELECT IDIngredientLanguage FROM  dbo.IngredientsLanguages 
		WHERE IDIngredient = @IDIngredient AND  IDLanguage = @IDLanguage)
		SET @testExist = 1
	ELSE
		SET @testExist = 0
			
	IF @testExist = 0
	BEGIN
		SELECT @IDLanguage  = MIN(IDLanguage) FROM dbo.IngredientsLanguages 
		WHERE IDIngredient = @IDIngredient
	END

	SELECT IDIngredientLanguage, IDIngredient, 
			IDLanguage, IngredientSingular, 
			IngredientPlural, IngredientDescription, 
			isAutoTranslate,GeoIDRegion 
	FROM dbo.IngredientsLanguages
	WHERE IDIngredient = @IDIngredient AND  IDLanguage = @IDLanguage
	
END



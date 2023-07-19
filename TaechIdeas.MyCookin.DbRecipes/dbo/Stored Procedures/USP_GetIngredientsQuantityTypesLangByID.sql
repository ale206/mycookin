


-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, USP_GetIngredientsQuantityTypesLangByID>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIngredientsQuantityTypesLangByID](@IDIngredientQuantityTypeLanguage INT,@IDLanguage INT)
AS
BEGIN

	IF EXISTS (SELECT IDIngredientQuantityTypeLanguage FROM  dbo.IngredientsQuantityTypesLanguages 
			WHERE IDLanguage = @IDLanguage AND IDIngredientQuantityType = @IDIngredientQuantityTypeLanguage)
		BEGIN
			SELECT IDIngredientQuantityTypeLanguage, IDIngredientQuantityType, 
				IDLanguage, IngredientQuantityTypeSingular, IngredientQuantityTypePlural, ConvertionRatio, 
				IngredientQuantityTypeX1000Singular, IngredientQuantityTypeX1000Plural, IngredientQuantityTypeWordsShowBefore, IngredientQuantityTypeWordsShowAfter
			FROM dbo.IngredientsQuantityTypesLanguages
			WHERE IDLanguage=@IDLanguage AND IDIngredientQuantityType = @IDIngredientQuantityTypeLanguage
		END
	ELSE
		BEGIN
			SELECT IDIngredientQuantityTypeLanguage, IDIngredientQuantityType, 
			IDLanguage, IngredientQuantityTypeSingular ,IngredientQuantityTypePlural, ConvertionRatio, 
			IngredientQuantityTypeX1000Singular, IngredientQuantityTypeX1000Plural, IngredientQuantityTypeWordsShowBefore, IngredientQuantityTypeWordsShowAfter
			FROM dbo.IngredientsQuantityTypesLanguages
			WHERE IDLanguage=(SELECT MIN(IDLanguage) FROM dbo.IngredientsQuantityTypesLanguages ) AND IDIngredientQuantityType = @IDIngredientQuantityTypeLanguage
		END
END





-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, USP_GetAllIngredientsQuantityTypesByIDLang>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetAllIngredientsQuantityTypesByIDLang](@IDLanguage INT)
AS
BEGIN

	IF EXISTS (SELECT IDIngredientQuantityTypeLanguage FROM  dbo.IngredientsQuantityTypesLanguages 
			WHERE IDLanguage = @IDLanguage)
		BEGIN
			SELECT IDIngredientQuantityTypeLanguage, IngredientsQuantityTypesLanguages.IDIngredientQuantityType, 
				IDLanguage, IngredientQuantityTypeSingular, IngredientQuantityTypePlural, ConvertionRatio, IngredientQuantityTypeX1000Singular, 
				IngredientQuantityTypeX1000Plural, IngredientQuantityTypeWordsShowBefore, IngredientQuantityTypeWordsShowAfter
			FROM dbo.IngredientsQuantityTypesLanguages INNER JOIN IngredientsQuantityTypes 
			ON IngredientsQuantityTypesLanguages.IDIngredientQuantityType = IngredientsQuantityTypes.IDIngredientQuantityType
			WHERE IDLanguage=@IDLanguage  AND EnabledToUser = 1
		END
	ELSE
		BEGIN
			SELECT IDIngredientQuantityTypeLanguage, IngredientsQuantityTypesLanguages.IDIngredientQuantityType, 
			IDLanguage, IngredientQuantityTypeSingular, IngredientQuantityTypePlural, ConvertionRatio, IngredientQuantityTypeX1000Singular, 
			IngredientQuantityTypeX1000Plural, IngredientQuantityTypeWordsShowBefore, IngredientQuantityTypeWordsShowAfter
			FROM dbo.IngredientsQuantityTypesLanguages INNER JOIN IngredientsQuantityTypes 
			ON IngredientsQuantityTypesLanguages.IDIngredientQuantityType = IngredientsQuantityTypes.IDIngredientQuantityType
			WHERE IDLanguage=(SELECT MIN(IDLanguage) FROM dbo.IngredientsQuantityTypesLanguages ) AND EnabledToUser = 1
		END
END


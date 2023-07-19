-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 21/09/2013>
-- Description:	<Description, USP_GetHomeTopRecipes>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIngredientToTranslate] (
@IDLanguageFrom INT, 
@IDLanguageTo INT, 
@NumRow INT)
AS
BEGIN

	SELECT TOP (@NumRow) [IDIngredientLanguage], [IngredientsLanguages].[IDIngredient], [IDLanguage],
		[IngredientsLanguages].IngredientSingular,
	[IngredientsLanguages].IngredientPlural,
	[IngredientsLanguages].IngredientDescription

	FROM [dbo].[IngredientsLanguages] INNER JOIN Ingredients
		ON [IngredientsLanguages].IDIngredient = Ingredients.[IDIngredient]
	WHERE IDLanguage = @IDLanguageFrom 
	AND [IngredientsLanguages].IDIngredient NOT IN (SELECT IDIngredient FROM [IngredientsLanguages] WHERE IDLanguage=@IDLanguageTo)
	AND IngredientEnabled = 1
	
END




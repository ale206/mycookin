-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/11/2015>
-- Description:	<Get Detailed Ingredients by RecipeId and Language>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetDetailedIngredientsByIdRecipeAndLanguage]
	@IDRecipe uniqueidentifier,
	@IDLanguage int

AS
BEGIN
     
	 SELECT        RecipesIngredients.IsPrincipalIngredient, RecipesIngredients.IDRecipe, RecipesIngredients.QuantityNotStd, RecipesIngredients.IDQuantityNotStd, RecipesIngredients.Quantity, 
                         RecipesIngredients.IDQuantityType, RecipesIngredients.QuantityNotSpecified, RecipesIngredients.RecipeIngredientGroupNumber, RecipesIngredients.IDRecipeIngredientAlternative, 
                         RecipesIngredients.IngredientRelevance, IngredientsLanguages.IDIngredientLanguage, IngredientsLanguages.IDLanguage, IngredientsLanguages.IngredientSingular, IngredientsLanguages.IngredientPlural, 
                         IngredientsLanguages.IngredientDescription, IngredientsLanguages.isAutoTranslate, IngredientsLanguages.GeoIDRegion, Ingredients.IDIngredient, Ingredients.IDIngredientPreparationRecipe, 
                         Ingredients.IDIngredientImage, Ingredients.AverageWeightOfOnePiece, Ingredients.Kcal100gr, Ingredients.grProteins, Ingredients.grFats, Ingredients.grCarbohydrates,
						 Ingredients.grAlcohol, Ingredients.IsVegetarian, Ingredients.IsVegan, Ingredients.IsGlutenFree, 
                         Ingredients.IsHotSpicy, Ingredients.Checked, Ingredients.IngredientCreatedBy, Ingredients.IngredientCreationDate, Ingredients.January, Ingredients.February, Ingredients.March, Ingredients.April, 
                         Ingredients.May, Ingredients.June, Ingredients.July, Ingredients.August, Ingredients.September, Ingredients.October, Ingredients.November, Ingredients.December, RecipesIngredients.IDRecipeIngredient,
						 FriendlyId
FROM            RecipesIngredients INNER JOIN
                         IngredientsLanguages INNER JOIN
                         Ingredients ON IngredientsLanguages.IDIngredient = Ingredients.IDIngredient ON RecipesIngredients.IDIngredient = Ingredients.IDIngredient
WHERE        (RecipesIngredients.IDRecipe = @IDRecipe) AND (IngredientsLanguages.IDLanguage = @IDLanguage)


END



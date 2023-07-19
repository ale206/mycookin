-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/11/2015>
-- Description:	<Get Detailed Ingredients by RecipeId>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetDetailedIngredientsByIdRecipe]
	@IDRecipe uniqueidentifier
AS
BEGIN
     
	 SELECT        RecipesIngredients.IsPrincipalIngredient, RecipesIngredients.IDRecipe, RecipesIngredients.QuantityNotStd, RecipesIngredients.IDQuantityNotStd, RecipesIngredients.Quantity, 
                         RecipesIngredients.IDQuantityType, RecipesIngredients.QuantityNotSpecified, RecipesIngredients.RecipeIngredientGroupNumber, RecipesIngredients.IDRecipeIngredientAlternative, 
                         RecipesIngredients.IngredientRelevance, Ingredients.IDIngredient, Ingredients.IDIngredientPreparationRecipe, Ingredients.IDIngredientImage, Ingredients.AverageWeightOfOnePiece, Ingredients.Kcal100gr, 
                         Ingredients.grProteins, Ingredients.IsVegetarian, Ingredients.IsVegan, Ingredients.IsGlutenFree, Ingredients.IsHotSpicy, Ingredients.Checked, Ingredients.IngredientCreatedBy, Ingredients.IngredientCreationDate, 
                         Ingredients.January, Ingredients.February, Ingredients.March, Ingredients.April, Ingredients.May, Ingredients.June, Ingredients.July, Ingredients.August, Ingredients.September, Ingredients.October, 
                         Ingredients.November, Ingredients.December, RecipesIngredients.IDRecipeIngredient
FROM            RecipesIngredients INNER JOIN
                         Ingredients ON RecipesIngredients.IDIngredient = Ingredients.IDIngredient
WHERE        (RecipesIngredients.IDRecipe = @IDRecipe)


END



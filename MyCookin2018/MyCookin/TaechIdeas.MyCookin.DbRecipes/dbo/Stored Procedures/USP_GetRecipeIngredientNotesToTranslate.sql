





-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/01/2013>
-- Description:	<Description, USP_GetHomeTopRecipes>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipeIngredientNotesToTranslate] (@IDLanguageFrom INT, @IDLanguageTo INT, @NumRow INT)
AS
BEGIN

	SELECT TOP (@NumRow) IDRecipeIngredientLanguage,RecipesIngredients.IDRecipeIngredient, RecipesIngredientsLanguages.[IDLanguage]
	FROM [dbo].RecipesIngredientsLanguages INNER JOIN RecipesIngredients
	ON RecipesIngredients.IDRecipeIngredient = RecipesIngredientsLanguages.IDRecipeIngredient INNER JOIN Recipes
	ON RecipesIngredients.IDRecipe = Recipes.IDRecipe
	WHERE RecipesIngredientsLanguages.IDLanguage = @IDLanguageFrom AND Checked=1
	AND Draft = 0 AND DeletedOn IS NULL
	AND ([RecipeIngredientNote]<>'' OR [RecipeIngredientGroupName]<>'')
	AND RecipesIngredients.IDRecipeIngredient NOT IN (SELECT IDRecipeIngredient FROM RecipesIngredientsLanguages WHERE IDLanguage = @IDLanguageTo)
	
END









-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 21/09/2013>
-- Description:	<Description, USP_GetHomeTopRecipes>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetHomeTopRecipes] (
@IDLanguage INT, 
@RecipeToShow INT
)
AS
BEGIN

	SELECT TOP (@RecipeToShow) IDRecipeLanguage, Recipes.IDRecipe, RecipeName, IDRecipeImage, Vegetarian, Vegan, GlutenFree, HotSpicy,
	RecipeDifficulties, RecipePortionKcal, IdRecipeImage, PreparationTimeMinute, CookingTimeMinute, CreationDate, RecipeAvgRating, IDOwner
	FROM Recipes INNER JOIN RecipesLanguages
	ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
	WHERE  IDRecipeImage IS NOT NULL 
			AND IDLanguage = @IDLanguage
			AND IDRecipeImage<>'00000000-0000-0000-0000-000000000000'
			AND RecipeEnabled = 1
			AND Vegetarian = 1
			AND RecipePortionKcal > 2
			AND Checked=1
			AND Draft = 0
			AND isStarterRecipe = 0
			--AND RecipeAvgRating > 3
			--AND RecipeConsulted > 10
	ORDER BY  LastUpdate DESC, RecipeAvgRating DESC--,	LastUpdate DESC
	
END



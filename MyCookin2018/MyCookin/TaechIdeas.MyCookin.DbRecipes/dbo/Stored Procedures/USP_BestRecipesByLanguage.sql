-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/02/2016>
-- Description:	<Get Best Recipes from BestRecipes table. Same return of USP_GetHomeTopRecipes>
-- =============================================
CREATE PROCEDURE [dbo].[USP_BestRecipesByLanguage]
@IDLanguage int

AS
BEGIN

	SELECT IDRecipeLanguage, R.IDRecipe, RecipeName, IDRecipeImage, Vegetarian, Vegan, GlutenFree, HotSpicy,
	RecipeDifficulties, RecipePortionKcal, IdRecipeImage, PreparationTimeMinute, CookingTimeMinute, CreationDate, RecipeAvgRating, IDOwner, RL.FriendlyId
	FROM Recipes R INNER JOIN RecipesLanguages RL
	ON R.IDRecipe = RL.IDRecipe
	INNER JOIN BestRecipes BR ON R.IDRecipe = BR.IDRecipe
	WHERE  BR.Enabled = 1 AND RL.IDLanguage = 2
	
END



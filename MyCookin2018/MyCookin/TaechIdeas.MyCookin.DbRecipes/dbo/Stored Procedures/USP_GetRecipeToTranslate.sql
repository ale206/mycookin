-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/01/2013>
-- Description:	<>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipeToTranslate] 
	@IDLanguageFrom INT, 
	@IDLanguageTo INT, 
	@NumRow INT
	
AS
BEGIN

	SELECT TOP (@NumRow) 
	RecipeName, RecipeHistory, RecipeNote, RecipeSuggestion, RecipeLanguageTags,
	IDRecipeLanguage, RecipesLanguages.IDRecipe, [IDLanguage]
	FROM [dbo].RecipesLanguages INNER JOIN Recipes
		ON RecipesLanguages.IDRecipe = Recipes.IDRecipe
	WHERE IDLanguage = @IDLanguageFrom 
	AND RecipesLanguages.IDRecipe NOT IN (SELECT IDRecipe FROM RecipesLanguages WHERE IDLanguage = @IDLanguageTo)
	AND RecipeEnabled = 1 
	AND RecipePortionKcal > 1
	AND Checked = 1
	AND Draft = 0
	
END





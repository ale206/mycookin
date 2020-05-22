



-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/01/2013>
-- Description:	<Description, USP_GetHomeTopRecipes>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipeStepsToTranslate] (@IDLanguageFrom INT, @IDLanguageTo INT, @NumRow INT)
AS
BEGIN

	SELECT TOP (@NumRow) t2.IDRecipeStep, t1.[IDRecipeLanguage] AS IDRecipeLanguage FROM
	(
		SELECT RecipesLanguages.[IDRecipeLanguage], IDRecipe, IDRecipeStep, IDLanguage FROM [RecipesSteps] RIGHT JOIN RecipesLanguages
			ON RecipesLanguages.IDRecipeLanguage = [RecipesSteps].IDRecipeLanguage
			WHERE IDLanguage = @IDLanguageTo AND IDRecipeStep IS NULL
	) t1
	INNER JOIN
	(
	SELECT RecipesLanguages.[IDRecipeLanguage], IDRecipe, IDRecipeStep, IDLanguage FROM [RecipesSteps] INNER JOIN RecipesLanguages
			ON RecipesLanguages.IDRecipeLanguage = [RecipesSteps].IDRecipeLanguage
			WHERE IDLanguage = @IDLanguageFrom
	) t2 ON t1.IDRecipe = t2.IDRecipe
	
END





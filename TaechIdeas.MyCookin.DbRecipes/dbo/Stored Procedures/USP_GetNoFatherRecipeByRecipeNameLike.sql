


-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, [USP_GetNoFatherRecipeByRecipeNameLike]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetNoFatherRecipeByRecipeNameLike](@RecipeName nvarchar(100), @IDLanguage INT)
AS
BEGIN
	SELECT NEWID() AS IDRecipeLanguage, NULL AS IDRecipe, 'Select Value' AS RecipeName
	UNION
	SELECT IDRecipeLanguage, Recipes.IDRecipe,RecipeName
	FROM dbo.RecipesLanguages INNER JOIN dbo.Recipes ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
	WHERE  RecipeName LIKE '%'+@RecipeName+'%' AND RecipeDisabled = 0 AND IDLanguage = @IDLanguage
			AND Recipes.IDRecipeFather IS NULL
	
END


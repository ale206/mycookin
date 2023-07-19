


-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, USP_GetRecipeByRecipeNameLike>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipeByRecipeNameLike](@RecipeName nvarchar(100), @IDLanguage INT)
AS
BEGIN
	SELECT NEWID() AS IDRecipeLanguage, NULL AS IDRecipe, 'Select Value' AS RecipeName
	UNION
	SELECT IDRecipeLanguage, IDRecipe,RecipeName
	FROM dbo.RecipesLanguages 
	WHERE  RecipeName LIKE '%'+@RecipeName+'%' AND RecipeDisabled = 0 AND IDLanguage = @IDLanguage
	
END


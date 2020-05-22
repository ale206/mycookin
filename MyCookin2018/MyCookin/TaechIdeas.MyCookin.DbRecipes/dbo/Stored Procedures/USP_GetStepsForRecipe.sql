-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/11/2015>
-- Description:	<Get Steps according to Recipe>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetStepsForRecipe]
	@IDRecipeLanguage uniqueidentifier 

AS
BEGIN

SELECT        RecipesSteps.*
FROM            RecipesSteps INNER JOIN
                         RecipesLanguages ON RecipesSteps.IDRecipeLanguage = RecipesLanguages.IDRecipeLanguage INNER JOIN
                         Recipes ON RecipesLanguages.IDRecipe = Recipes.IDRecipe
WHERE        (RecipesLanguages.IDRecipeLanguage = @IDRecipeLanguage)

	
END



-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/11/2015>
-- Description:	<Get Steps according to Recipe and language>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetStepsByIDRecipeLang]
	@IDRecipe uniqueidentifier,
	@IDLanguage int

AS
BEGIN

SELECT        RecipesSteps.*
FROM            RecipesSteps INNER JOIN
                         RecipesLanguages ON RecipesSteps.IDRecipeLanguage = RecipesLanguages.IDRecipeLanguage
WHERE        (RecipesLanguages.IDLanguage = @IDLanguage) AND (RecipesLanguages.IDRecipe = @IDRecipe)

	
END



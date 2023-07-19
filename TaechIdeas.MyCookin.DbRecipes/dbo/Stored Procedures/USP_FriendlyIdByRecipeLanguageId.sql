-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <09/01/2016>
-- Description:	<Get FriendlyId By RecipeLanguageId>
-- =============================================
CREATE PROCEDURE [dbo].[USP_FriendlyIdByRecipeLanguageId]
	@IDRecipeLanguage uniqueidentifier

AS
BEGIN

  SELECT FriendlyId AS FriendlyId
  FROM RecipesLanguages
  WHERE IDRecipeLanguage = @IDRecipeLanguage

	
END



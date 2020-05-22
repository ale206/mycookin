-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/11/2015>
-- Description:	<Get Ingredients by RecipeId>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIngredientsByIdRecipe]
	@IDRecipe uniqueidentifier

AS
BEGIN
     SELECT * FROM dbo.RecipesIngredients
                     WHERE IDRecipe = @IDRecipe AND IDRecipeIngredientAlternative IS NULL
                     ORDER BY RecipeIngredientGroupNumber,IngredientRelevance DESC, Quantity DESC

END



-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/11/20152>
-- Description:	<Get Recipe detail by its id>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipeById]
	@IDRecipe uniqueidentifier

AS
BEGIN
    SELECT     IDRecipe, IDRecipeFather, IDOwner, NumberOfPerson, PreparationTimeMinute, CookingTimeMinute, RecipeDifficulties, IDRecipeImage, IDRecipeVideo, IDCity,
    CreationDate, LastUpdate, UpdatedByUser, RecipeConsulted, RecipeAvgRating, isStarterRecipe, DeletedOn, BaseRecipe, RecipeEnabled, Checked,RecipeCompletePerc, RecipePortionKcal, RecipePortionProteins, RecipePortionFats, RecipePortionCarbohydrates,RecipePortionQta, Vegetarian,Vegan,GlutenFree,HotSpicy,RecipePortionAlcohol,Draft,RecipeRated
    FROM         Recipes
    WHERE     (IDRecipe = @IDRecipe AND DeletedOn IS NULL)
END



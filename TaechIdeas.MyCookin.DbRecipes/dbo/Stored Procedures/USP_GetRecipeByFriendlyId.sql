-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <09/01/2016>
-- Description:	<Get Recipe detail by its friendly id>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipeByFriendlyId]
	@FriendlyId nvarchar(250)

AS
BEGIN
    SELECT     R.IDRecipe, R.IDRecipeImage, IDLanguage, IDRecipeFather, IDOwner, NumberOfPerson, PreparationTimeMinute, CookingTimeMinute, RecipeDifficulties, IDRecipeImage, IDRecipeVideo, IDCity,
    CreationDate, LastUpdate, UpdatedByUser, RecipeConsulted, RecipeAvgRating, isStarterRecipe, DeletedOn, BaseRecipe, RecipeEnabled, Checked,RecipeCompletePerc, RecipePortionKcal, RecipePortionProteins, RecipePortionFats, RecipePortionCarbohydrates,RecipePortionQta, Vegetarian,Vegan,GlutenFree,HotSpicy,RecipePortionAlcohol,Draft,RecipeRated
    FROM         Recipes R
	INNER JOIN RecipesLanguages L ON R.IDRecipe = L.IDRecipe
    WHERE     (FriendlyId = @FriendlyId AND DeletedOn IS NULL)
END



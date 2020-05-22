

-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 26/08/2013>
-- Description:	<Description, [USP_GetRecipesWithIngredient]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipesWithIngredient](@NumberOfResult INT, 
														@IDIngredient UNIQUEIDENTIFIER, 
														@Checked BIT, 
														@IngredientName NVARCHAR(20))
AS
BEGIN
	DECLARE @testVegetarian bit;
	SELECT @testVegetarian = Ingredients.IsVegetarian FROM Ingredients WHERE IDIngredient=@IDIngredient

	SELECT DISTINCT TOP (@NumberOfResult) IDRecipe FROM
	(
		SELECT DISTINCT TOP (@NumberOfResult) Recipes.IDRecipe, RecipeAvgRating, 0 AS Preference
		FROM [dbo].[Recipes] INNER JOIN [dbo].[RecipesIngredients]
		on [Recipes].IDRecipe = [RecipesIngredients].IDRecipe inner join RecipesLanguages
		on [Recipes].IDRecipe = RecipesLanguages.IDRecipe
		where IDIngredient =@IDIngredient
			AND RecipeEnabled=1 
			AND Checked=1 
			AND IsPrincipalIngredient=1
			AND [RecipePortionKcal] > 50
			AND RecipeName LIKE '%' + @IngredientName + '%'
			AND Vegetarian = @testVegetarian
			AND Draft = 0
		UNION
		SELECT DISTINCT TOP (@NumberOfResult) Recipes.IDRecipe, RecipeAvgRating, 1 AS Preference  
		FROM [dbo].[Recipes] INNER JOIN [dbo].[RecipesIngredients]
		on [Recipes].IDRecipe = [RecipesIngredients].IDRecipe
		where IDIngredient =@IDIngredient 
			AND RecipeEnabled=1 
			AND Checked=1
			AND IsPrincipalIngredient=1
			AND [RecipePortionKcal] > 50
			AND Vegetarian = @testVegetarian
			AND Draft = 0
		UNION
		SELECT DISTINCT TOP (@NumberOfResult) Recipes.IDRecipe, RecipeAvgRating, 2 AS Preference  
		FROM [dbo].[Recipes] INNER JOIN [dbo].[RecipesIngredients]
		on [Recipes].IDRecipe = [RecipesIngredients].IDRecipe
		where IDIngredient =@IDIngredient 
			AND RecipeEnabled=1 
			AND Checked=1
			AND [RecipePortionKcal] > 50
			AND Vegetarian = @testVegetarian
			AND Draft = 0
		order by Preference ASC, RecipeAvgRating desc
	) t
END






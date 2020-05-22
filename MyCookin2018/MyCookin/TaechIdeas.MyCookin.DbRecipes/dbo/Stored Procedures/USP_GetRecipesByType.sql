



-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 06/02/2014>
-- Description:	<Description, [USP_GetRecipesByType]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipesByType] (@RecipeType INT,
														@OffsetRows INT, 
														@FetchRows INT,
														@Vegan BIT,
														@Vegetarian BIT,
														@GlutenFree BIT,
														@LightThreshold FLOAT,
														@QuickThreshold INT,
														@IDLanguage INT)
AS
	--@RecipeType if 0 show all recipe type, else show only the specified type 
BEGIN
	SELECT DISTINCT Recipes.IDRecipe, IDRecipeImage, dbo.udfReplaceSpecialChar(RecipeName,'') AS RecipeName, IDOwner, RecipeAvgRating
		FROM Recipes INNER JOIN [dbo].[RecipesPropertiesValues]
		ON Recipes.IDRecipe = [RecipesPropertiesValues].IDRecipe INNER JOIN [dbo].[RecipesLanguages]
		ON [RecipesPropertiesValues].IDRecipe = [RecipesLanguages].IDRecipe
		WHERE ([IDRecipeProperty] IN (
									SELECT [IDRecipeProperty] 
										FROM [dbo].[RecipeProperties] 
										WHERE [IDRecipePropertyType] = 1 
											AND [IDRecipeProperty] = CASE WHEN @RecipeType = 0 THEN [IDRecipeProperty] ELSE @RecipeType END) 
											AND [Value] = 1
										)
			AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
						OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
						OR isnull(vegan,0)=@vegan)
					AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
						OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
						OR isnull(Vegetarian,0)=@Vegetarian)
					AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
						OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
						OR isnull(GlutenFree,0)=@GlutenFree)
					AND RecipeEnabled = 1
					AND DeletedOn IS NULL
					AND Checked = 1
					AND Draft = 0
					AND IDLanguage = @IDLanguage
					AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 1 AND @QuickThreshold )
					AND [RecipePortionKcal] BETWEEN 1 AND @LightThreshold
			order by RecipeAvgRating desc
			OFFSET @OffsetRows ROWS
    		FETCH NEXT @FetchRows ROWS ONLY	
END





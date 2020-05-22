


-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 27/09/2013>
-- Description:	<Description, USP_GetRecipesInRecipeBook>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipesInRecipeBook] (@IDUser uniqueidentifier, 
														@RecipeType INT,
														@ShowFilter INT,
														@RecipeNameFilter NVARCHAR(250),
														@Vegan BIT,
														@Vegetarian BIT,
														@GlutenFree BIT,
														@LightThreshold FLOAT,
														@QuickThreshold INT,
														@ShowDraft BIT,
														@IDLanguage INT, 
														@OffsetRows INT, 
														@FetchRows INT)
AS
	--@ShowFilter for recipe type, 0: All recipes, 1: only my recipes, 2: only others recipes
	--@RecipeType if 0 show all recipe type, else show only the specified type 
BEGIN

	IF @ShowFilter = 0
		BEGIN
			SELECT IDRecipeBookRecipe, IDUser, IDRecipe, RecipeAddedOn, RecipeOrder, SourceType, MIN(RecipeName) AS RecipeName, IDRecipeImage FROM
			(
			SELECT IDRecipeBookRecipe, [Recipes].IDOwner AS IDUser, RecipesBooksRecipes.IDRecipe, RecipeAddedOn, RecipeOrder, 'Book' AS SourceType, RecipeName, Recipes.IDRecipeImage
				FROM dbo.RecipesBooksRecipes INNER JOIN [dbo].[RecipesPropertiesValues]
					ON RecipesBooksRecipes.IDRecipe = [RecipesPropertiesValues].IDRecipe INNER JOIN [dbo].[RecipesLanguages]
					ON [RecipesPropertiesValues].IDRecipe = [RecipesLanguages].IDRecipe INNER JOIN [dbo].[Recipes]
					ON [RecipesLanguages].IDRecipe = [dbo].[Recipes].IDRecipe
				WHERE IDUser = @IDUser
						AND ([IDRecipeProperty] IN (SELECT [IDRecipeProperty] FROM [dbo].[RecipeProperties] WHERE [IDRecipePropertyType] = 1 AND [IDRecipeProperty] = CASE WHEN @RecipeType = 0 THEN [IDRecipeProperty] ELSE @RecipeType END) AND [Value] = 1)
						AND RecipeName LIKE CASE @RecipeNameFilter WHEN '' THEN  RecipeName ELSE @RecipeNameFilter+'%' END
						AND IDLanguage = @IDLanguage
						AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
							OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
							OR isnull(vegan,0)=@vegan)
						AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
							OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
							OR isnull(Vegetarian,0)=@Vegetarian)
						AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
							OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
							OR isnull(GlutenFree,0)=@GlutenFree)
						AND (Draft = CASE  WHEN @ShowDraft = 1 THEN 1 ELSE 0 END
							OR Draft = CASE  WHEN @ShowDraft = 1 THEN 0 ELSE 0 END)
						AND RecipeEnabled = 1
						AND DeletedOn IS NULL
						AND Checked = 1
						AND ((([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 0 AND @QuickThreshold ) OR ([PreparationTimeMinute] IS NULL))
						AND (([RecipePortionKcal] BETWEEN 0 AND @LightThreshold) OR ([RecipePortionKcal] IS NULL))
			UNION
			SELECT Recipes.[IDRecipe] AS IDRecipeBookRecipe, [Recipes].IDOwner AS IDUser, Recipes.[IDRecipe], [CreationDate] AS RecipeAddedOn, 0 AS RecipeOrder, 'MyRecipes' AS SourceType, RecipeName, Recipes.IDRecipeImage
				FROM dbo.Recipes INNER JOIN [dbo].[RecipesPropertiesValues]
					ON Recipes.IDRecipe = [RecipesPropertiesValues].IDRecipe INNER JOIN [dbo].[RecipesLanguages]
					ON [RecipesPropertiesValues].IDRecipe = [RecipesLanguages].IDRecipe
				WHERE [IDOwner] = @IDUser
						AND ([IDRecipeProperty] IN (SELECT [IDRecipeProperty] FROM [dbo].[RecipeProperties] WHERE [IDRecipePropertyType] = 1 AND [IDRecipeProperty] = CASE WHEN @RecipeType = 0 THEN [IDRecipeProperty] ELSE @RecipeType END) AND [Value] = 1)
						AND RecipeName LIKE CASE @RecipeNameFilter WHEN '' THEN  RecipeName ELSE @RecipeNameFilter+'%' END
						AND IDLanguage = @IDLanguage
						AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
							OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
							OR isnull(vegan,0)=@vegan)
						AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
							OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
							OR isnull(Vegetarian,0)=@Vegetarian)
						AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
							OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
							OR isnull(GlutenFree,0)=@GlutenFree)
						AND (Draft = CASE  WHEN @ShowDraft = 1 THEN 1 ELSE 0 END
							OR Draft = CASE  WHEN @ShowDraft = 1 THEN 0 ELSE 0 END)
						AND RecipeEnabled = 1
						AND DeletedOn IS NULL
						AND Checked = 1
						AND ((([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 0 AND @QuickThreshold ) OR ([PreparationTimeMinute] IS NULL))
						AND (([RecipePortionKcal] BETWEEN 0 AND @LightThreshold) OR ([RecipePortionKcal] IS NULL))
			) t1
				GROUP BY IDRecipeBookRecipe, IDUser, IDRecipe, RecipeAddedOn, RecipeOrder, SourceType, IDRecipeImage
				ORDER BY RecipeName ASC, RecipeOrder ASC,  RecipeAddedOn DESC
				OFFSET @OffsetRows ROWS
				FETCH NEXT @FetchRows ROWS ONLY
		END
	IF @ShowFilter = 1
		BEGIN
			SELECT DISTINCT Recipes.[IDRecipe] AS IDRecipeBookRecipe, [Recipes].IDOwner AS IDUser, Recipes.[IDRecipe], [CreationDate] AS RecipeAddedOn, 0 AS RecipeOrder, 'MyRecipes' AS SourceType,RecipeName, Recipes.IDRecipeImage
				FROM dbo.Recipes INNER JOIN [dbo].[RecipesPropertiesValues]
					ON Recipes.IDRecipe = [RecipesPropertiesValues].IDRecipe INNER JOIN [dbo].[RecipesLanguages]
					ON [RecipesPropertiesValues].IDRecipe = [RecipesLanguages].IDRecipe
				WHERE [IDOwner] = @IDUser
						AND ([IDRecipeProperty] IN (SELECT [IDRecipeProperty] FROM [dbo].[RecipeProperties] WHERE [IDRecipePropertyType] = 1 AND [IDRecipeProperty] = CASE WHEN @RecipeType = 0 THEN [IDRecipeProperty] ELSE @RecipeType END) AND [Value] = 1)
						AND RecipeName LIKE CASE @RecipeNameFilter WHEN '' THEN  RecipeName ELSE @RecipeNameFilter+'%' END
						AND IDLanguage = @IDLanguage
						AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
							OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
							OR isnull(vegan,0)=@vegan)
						AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
							OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
							OR isnull(Vegetarian,0)=@Vegetarian)
						AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
							OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
							OR isnull(GlutenFree,0)=@GlutenFree)
						AND (Draft = CASE  WHEN @ShowDraft = 1 THEN 1 ELSE 0 END
							OR Draft = CASE  WHEN @ShowDraft = 1 THEN 0 ELSE 0 END)
						AND RecipeEnabled = 1
						AND DeletedOn IS NULL
						AND Checked = 1
						AND ((([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 0 AND @QuickThreshold ) OR ([PreparationTimeMinute] IS NULL))
						AND (([RecipePortionKcal] BETWEEN 0 AND @LightThreshold) OR ([RecipePortionKcal] IS NULL))
				ORDER BY RecipeName ASC, RecipeOrder ASC,  RecipeAddedOn DESC
				OFFSET @OffsetRows ROWS
				FETCH NEXT @FetchRows ROWS ONLY
		END
	IF @ShowFilter = 2
		BEGIN
			SELECT DISTINCT IDRecipeBookRecipe, [Recipes].IDOwner AS IDUser, RecipesBooksRecipes.IDRecipe, RecipeAddedOn, RecipeOrder, 'Book' AS SourceType,RecipeName, Recipes.IDRecipeImage
				FROM dbo.RecipesBooksRecipes INNER JOIN [dbo].[RecipesPropertiesValues]
					ON RecipesBooksRecipes.IDRecipe = [RecipesPropertiesValues].IDRecipe INNER JOIN [dbo].[RecipesLanguages]
					ON [RecipesPropertiesValues].IDRecipe = [RecipesLanguages].IDRecipe INNER JOIN [dbo].[Recipes]
					ON [RecipesLanguages].IDRecipe = [dbo].[Recipes].IDRecipe
				WHERE IDUser = @IDUser
						AND ([IDRecipeProperty] IN (SELECT [IDRecipeProperty] FROM [dbo].[RecipeProperties] WHERE [IDRecipePropertyType] = 1 AND [IDRecipeProperty] = CASE WHEN @RecipeType = 0 THEN [IDRecipeProperty] ELSE @RecipeType END) AND [Value] = 1)
						AND RecipeName LIKE CASE @RecipeNameFilter WHEN '' THEN  RecipeName ELSE @RecipeNameFilter+'%' END
						AND IDLanguage = @IDLanguage
						AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
							OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
							OR isnull(vegan,0)=@vegan)
						AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
							OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
							OR isnull(Vegetarian,0)=@Vegetarian)
						AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
							OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
							OR isnull(GlutenFree,0)=@GlutenFree)
						AND (Draft = CASE  WHEN @ShowDraft = 1 THEN 1 ELSE 0 END
							OR Draft = CASE  WHEN @ShowDraft = 1 THEN 0 ELSE 0 END)
						AND RecipeEnabled = 1
						AND DeletedOn IS NULL
						AND Checked = 1
						AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 0 AND @QuickThreshold )
						AND [RecipePortionKcal] BETWEEN 0 AND @LightThreshold
				ORDER BY RecipeName ASC, RecipeOrder ASC,  RecipeAddedOn DESC
					OFFSET @OffsetRows ROWS
					FETCH NEXT @FetchRows ROWS ONLY
		END

END




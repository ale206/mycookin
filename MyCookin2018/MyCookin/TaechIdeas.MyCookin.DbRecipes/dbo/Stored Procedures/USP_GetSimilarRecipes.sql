

-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 08/10/2013>
-- Description:	<Description, [USP_GetSimilarRecipes]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetSimilarRecipes] (@IDRecipe UNIQUEIDENTIFIER, 
													@RecipeName NVARCHAR(250),
													@Vegan BIT,
													@Vegetarian BIT,
													@GlutenFree BIT,
													@IDLanguage INT)
AS
BEGIN

	DECLARE @IngrIDsListDistinct TABLE
				(
					IDIngredient UNIQUEIDENTIFIER
				) 
	DECLARE @SimilaRecipesTable TABLE
				(
					IDRecipe UNIQUEIDENTIFIER,
					Relevance INT,
					NumIngr INT,
					isStarterRecipe BIT,
					RecipeAvgRating INT,
					Preference INT,
					[rank] INT
				) 

	IF @RecipeName = '' OR @RecipeName = ' '
		BEGIN
			INSERT INTO @SimilaRecipesTable(IDRecipe, Relevance, NumIngr, isStarterRecipe, Preference)
			SELECT TOP 4 IDRecipe, 0 AS Relevance, 0 AS NumIngr, 0 AS isStarterRecipe, 0 AS Preference
				FROM Recipes
				WHERE (IDRecipeFather IS NULL OR IDRecipeFather='00000000-0000-0000-0000-000000000000')
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
								AND Recipes.Checked = 1
								AND Draft = 0
								AND (([PreparationTimeMinute] + [CookingTimeMinute]) > 1 )
								AND [RecipePortionKcal] > 1
								AND DeletedOn IS NULL
				ORDER BY NEWID()
		END
	ELSE
		BEGIN
			INSERT INTO @IngrIDsListDistinct
						SELECT DISTINCT IDIngredient FROM RecipesIngredients
								where IDRecipe = @IDRecipe 
								and IngredientRelevance = 4
		
					INSERT INTO @SimilaRecipesTable(IDRecipe, Relevance, NumIngr, isStarterRecipe, Preference)
					SELECT TOP 4 t2.IDRecipe, Relevance,COUNT(RecipesIngredients.IDRecipeIngredient) AS NumIngr, isStarterRecipe, 0 AS Preference FROM
					(
						SELECT IDRecipe, Relevance, isStarterRecipe FROM
						(
							SELECT Recipes.IDRecipe, tIngr.IDIngredient, sum(IngredientRelevance) as Relevance, isStarterRecipe
								FROM @IngrIDsListDistinct tIngr INNER JOIN Ingredients
									ON tIngr.IDIngredient = Ingredients.IDIngredient INNER JOIN RecipesIngredients
									ON  Ingredients.IDIngredient = RecipesIngredients.IDIngredient INNER JOIN Recipes
									ON RecipesIngredients.IDRecipe = Recipes.IDRecipe 
								WHERE (IDRecipeFather IS NULL OR IDRecipeFather='00000000-0000-0000-0000-000000000000')
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
								AND Recipes.Checked = 1
								AND Draft = 0
								AND (([PreparationTimeMinute] + [CookingTimeMinute]) > 1 )
								AND [RecipePortionKcal] > 1
								AND IngredientRelevance = 4
								AND DeletedOn IS NULL
								AND Recipes.IDRecipe <> @IDRecipe
								GROUP BY Recipes.IDRecipe, tIngr.IDIngredient, isStarterRecipe
								--ORDER BY NumSearchedIngr DESC
							) t1
							GROUP BY IDRecipe,Relevance,isStarterRecipe
						) t2 INNER JOIN RecipesIngredients
						ON RecipesIngredients.IDRecipe = t2.IDRecipe
						GROUP BY t2.IDRecipe, Relevance, isStarterRecipe
						ORDER BY Relevance DESC, isStarterRecipe, NumIngr  ASC


				INSERT INTO @SimilaRecipesTable(IDRecipe, RecipeAvgRating, Preference, rank)
				SELECT DISTINCT TOP 4 IDRecipe, RecipeAvgRating, MIN(Preference) AS Preference, MAX(rank) AS rank FROM
					(
						SELECT IDRecipeLanguage, Recipes.IDRecipe, RecipeAvgRating, 1 AS Preference, rank FROM Recipes INNER JOIN RecipesLanguages
							ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
							INNER JOIN freetexttable(RecipesLanguages,RecipeName,@RecipeName) as t
								on RecipesLanguages.[IDRecipeLanguage] = t.[KEY]
						WHERE  Recipes.IDRecipe <> @IDRecipe
								AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
									OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
									OR isnull(vegan,0)=@vegan)
								AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
									OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
									OR isnull(Vegetarian,0)=@Vegetarian)
								AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
									OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
									OR isnull(GlutenFree,0)=@GlutenFree)
								AND isStarterRecipe = 0
								AND RecipeEnabled = 1
								AND Recipes.Checked = 1
								AND Draft = 0
								AND DeletedOn IS NULL
								AND (([PreparationTimeMinute] + [CookingTimeMinute]) > 1 )
								AND [RecipePortionKcal] > 1
						UNION
						SELECT IDRecipeLanguage, Recipes.IDRecipe, RecipeAvgRating, 2 AS Preference, rank FROM Recipes INNER JOIN RecipesLanguages
							ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
							INNER JOIN freetexttable(RecipesLanguages,RecipeName,@RecipeName) as t
								on RecipesLanguages.[IDRecipeLanguage] = t.[KEY]
						WHERE  Recipes.IDRecipe <> @IDRecipe
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
								AND Recipes.Checked = 1
								AND Draft = 0
								AND DeletedOn IS NULL
								AND (([PreparationTimeMinute] + [CookingTimeMinute]) > 1 )
								AND [RecipePortionKcal] > 1
					) t
					GROUP BY IDRecipe, RecipeAvgRating
					ORDER BY rank, Preference, RecipeAvgRating DESC	
		END
		SELECT DISTINCT TOP 4 tbRecipe.IDRecipe,RecipeName,IDRecipeImage,IDOwner,Preference FROM @SimilaRecipesTable tbRecipe INNER JOIN Recipes
			ON tbRecipe.IDRecipe = Recipes.IDRecipe INNER JOIN RecipesLanguages
			ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
			WHERE IDLanguage = @IDLanguage
			ORDER BY Preference
END



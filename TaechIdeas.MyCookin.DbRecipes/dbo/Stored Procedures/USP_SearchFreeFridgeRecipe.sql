



-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 29/08/2013>
-- Description:	<Description, [USP_SearchFreeFridgeRecipe]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SearchFreeFridgeRecipe](@IngrList NVARCHAR(250),
																	@Vegan BIT,
																	@Vegetarian BIT,
																	@GlutenFree BIT,
																	@LightThreshold FLOAT,
																	@QuickThreshold INT,
																	@OffsetRows INT,
																	@FetchRows INT)
AS
	BEGIN TRY
		DECLARE @IngrIDsList TABLE
				(
					IDIngredient UNIQUEIDENTIFIER,
					IngredientName NVARCHAR(150),
					IngrOrigName NVARCHAR(150)
				) 
		DECLARE @IngrIDsListDistinct TABLE
				(
					IDIngredient UNIQUEIDENTIFIER,
					IngredientName NVARCHAR(150),
					IngrOrigName NVARCHAR(150)
				) 

		DECLARE IngredientToSearch CURSOR
		FOR
		SELECT StringPart FROM SplitString(@IngrList,',')
		DECLARE @Ingr NVARCHAR(50)
		OPEN IngredientToSearch
		FETCH NEXT FROM IngredientToSearch INTO @Ingr
		WHILE(@@FETCH_STATUS<>-1)
			BEGIN
				--SELECT @Ingr
				INSERT INTO @IngrIDsList
					SELECT Ingredients.IDIngredient,IngredientSingular, @Ingr AS IngrOrigName  
						FROM Ingredients INNER JOIN IngredientsLanguages
						ON Ingredients.IDIngredient=IngredientsLanguages.IDIngredient
						WHERE IngredientSingular=@Ingr
							OR IngredientPlural=@Ingr
							OR IngredientSingular LIKE '% '+@Ingr+' %'
							OR IngredientPlural LIKE '% '+@Ingr+' %'
							OR IngredientSingular LIKE @Ingr+' %'
							OR IngredientPlural LIKE @Ingr+' %'
				FETCH NEXT FROM IngredientToSearch INTO @Ingr
			END
		CLOSE IngredientToSearch
		DEALLOCATE IngredientToSearch

		INSERT INTO @IngrIDsListDistinct
			SELECT DISTINCT IDIngredient, IngredientName, IngrOrigName FROM @IngrIDsList
		
		SELECT t2.IDRecipe, NumSearchedIngr,COUNT(RecipesIngredients.IDRecipeIngredient) AS NumIngr FROM
		(
			SELECT IDRecipe, COUNT(IngrOrigName) AS NumSearchedIngr FROM
			(
				SELECT RecipesLanguages.IDRecipe, RecipeName, IngrOrigName
					FROM @IngrIDsListDistinct tIngr INNER JOIN Ingredients
						ON tIngr.IDIngredient = Ingredients.IDIngredient INNER JOIN RecipesIngredients
						ON  Ingredients.IDIngredient = RecipesIngredients.IDIngredient INNER JOIN Recipes
						ON RecipesIngredients.IDRecipe = Recipes.IDRecipe INNER JOIN RecipesLanguages
						ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
					WHERE (IDRecipeFather IS NULL OR IDRecipeFather='00000000-0000-0000-0000-000000000000')
					AND (Recipes.vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
						OR Recipes.vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
						OR isnull(Recipes.vegan,0)=@vegan)
					AND (Recipes.Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
						OR Recipes.Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
						OR isnull(Recipes.Vegetarian,0)=@Vegetarian)
					AND (Recipes.GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
						OR Recipes.GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
						OR isnull(Recipes.GlutenFree,0)=@GlutenFree)
					AND RecipeEnabled = 1
					AND Recipes.Checked = 1
					AND Draft = 0
					AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 1 AND @QuickThreshold )
					AND [RecipePortionKcal] BETWEEN 1 AND @LightThreshold
					GROUP BY RecipesLanguages.IDRecipe, RecipeName, IngrOrigName
					--ORDER BY NumSearchedIngr DESC
				) t1
				GROUP BY IDRecipe, RecipeName
			) t2 INNER JOIN RecipesIngredients
			ON RecipesIngredients.IDRecipe = t2.IDRecipe
			GROUP BY t2.IDRecipe, NumSearchedIngr
			ORDER BY NumSearchedIngr DESC, NumIngr ASC
			OFFSET @OffsetRows ROWS
    		FETCH NEXT @FetchRows ROWS ONLY
--SELECT DISTINCT IDIngredient, IngredientName, IngrOrigName FROM @IngrIDsList
	--order by IngrOrigName

	END TRY
	BEGIN CATCH
		SELECT 'Error' as ERROR;
	END CATCH
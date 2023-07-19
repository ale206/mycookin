



-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 16/03/2014>
-- Description:	<Description, [Populate index for recipes searchs>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PopulateRecipesIndex]
AS
BEGIN
	DECLARE @CurrentDate smalldatetime
	SET @CurrentDate = GETUTCDATE()

	DECLARE @MaxIndexDate smalldatetime
	SELECT @MaxIndexDate = MAX([IndexInserted]) FROM [dbo].[RecipesIndex]

	IF @MaxIndexDate IS NULL
		SET @MaxIndexDate = '2000-01-01'

	DECLARE @NewRecipesForIndex TABLE
			(
			  IDRecipe uniqueidentifier
			)
	DECLARE @UpdatedRecipesForIndex TABLE
			(
			  IDRecipe uniqueidentifier
			)

	-- Get all recipes updated from last index update
	INSERT INTO @UpdatedRecipesForIndex(IDRecipe)
		SELECT IDRecipe FROM Recipes
			WHERE Checked = 1
				AND Draft = 0
				AND RecipePortionKcal > 1
				AND (([PreparationTimeMinute] + [CookingTimeMinute]) > 1 )
				AND LastUpdate > @MaxIndexDate
				AND IDRecipe IN (SELECT IDRecipe FROM RecipesIndex)

	-- Clear Index for updated recipes
	DELETE FROM RecipesIndex
		FROM @UpdatedRecipesForIndex UpdRec INNER JOIN RecipesIndex
			ON UpdRec.IDRecipe = RecipesIndex.IDRecipe

	-- Get all recipes created from last index update
	INSERT INTO @NewRecipesForIndex(IDRecipe)
		SELECT IDRecipe FROM Recipes
			WHERE Checked = 1
				AND Draft = 0
				AND RecipePortionKcal > 1
				AND (([PreparationTimeMinute] + [CookingTimeMinute]) > 1 )
				AND LastUpdate > @MaxIndexDate
				AND IDRecipe NOT IN (SELECT IDRecipe FROM RecipesIndex)

	-- *******************************************
	-- First preference, info from recipe name
	-- *******************************************
	INSERT INTO [dbo].[RecipesIndex]
           ([IDRecipeLanguage]
           ,[IDRecipe]
           ,[RecipeName]
           ,[IDLanguage]
           ,[SearchPreference]
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,[IndexInserted])
	SELECT [IDRecipeLanguage]
           ,Recipes.IDRecipe
           ,[RecipeName]
           ,[IDLanguage]
           ,1 AS SearchPreference
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,@CurrentDate AS IndexInserted
	FROM @NewRecipesForIndex NewRecipes INNER JOIN Recipes
					ON NewRecipes.IDRecipe = Recipes.IDRecipe INNER JOIN RecipesLanguages
					ON Recipes.IDRecipe = RecipesLanguages.IDRecipe

	-- *******************************************
	-- First preference, info from recipe name
	-- END FIRST PREFERENCE
	-- *******************************************


	-- *******************************************
	-- Second preference, info from recipe ingredient
	-- *******************************************
	INSERT INTO [dbo].[RecipesIndex]
           ([IDRecipeLanguage]
           ,[IDRecipe]
           ,[RecipeName]
           ,[IDLanguage]
           ,[SearchPreference]
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,[IndexInserted])
	SELECT RecLang.[IDRecipeLanguage]
           ,Rec.IDRecipe
		    ,RecLang.RecipeName + ' - ' + STUFF((
			SELECT IngredientPlural + ', '
		    FROM [dbo].[IngredientsLanguages] IngrLang INNER JOIN [dbo].[RecipesIngredients] RecIngr
				ON IngrLang.IDIngredient = RecIngr.IDIngredient
			WHERE RecIngr.IDRecipe = Rec.IDRecipe
				AND IngrLang.IDLanguage = RecLang.IDLanguage
			FOR XML PATH('')
			),1,0,'') AS RecipeName
			,RecLang.IDLanguage
			,2 AS SearchPreference
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,@CurrentDate AS IndexInserted
	FROM @NewRecipesForIndex NewRecipes INNER JOIN	Recipes Rec 
						ON NewRecipes.IDRecipe = Rec.IDRecipe INNER JOIN RecipesLanguages RecLang
						ON Rec.IDRecipe = RecLang.IDRecipe INNER JOIN [RecipesIngredients]
						ON Rec.IDRecipe = [RecipesIngredients].IDRecipe INNER JOIN [IngredientsLanguages]
						ON [dbo].[IngredientsLanguages].IDIngredient = [dbo].[RecipesIngredients].IDIngredient
	GROUP BY RecLang.[IDRecipeLanguage]
			   ,Rec.IDRecipe
			   ,RecLang.RecipeName
			   ,RecLang.IDLanguage
			   ,[PreparationTimeMinute]
			   ,[CookingTimeMinute]
			   ,[RecipeDifficulties]
			   ,[IDRecipeImage]
			   ,[CreationDate]
			   ,[LastUpdate]
			   ,[RecipeConsulted]
			   ,[RecipeAvgRating]
			   ,[isStarterRecipe]
			   ,[DeletedOn]
			   ,[RecipeEnabled]
			   ,[Checked]
			   ,[RecipePortionKcal]
			   ,[Vegetarian]
			   ,[Vegan]
			   ,[GlutenFree]
			   ,[Draft]

	-- *******************************************
	-- SECOND preference, info from recipe ingredient
	-- END SECOND PREFERENCE
	-- *******************************************

	-- *******************************************
	-- 3rd preference, info from recipe property
	-- *******************************************
	INSERT INTO [dbo].[RecipesIndex]
           ([IDRecipeLanguage]
           ,[IDRecipe]
           ,[RecipeName]
           ,[IDLanguage]
           ,[SearchPreference]
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,[IndexInserted])
	SELECT RecLang.[IDRecipeLanguage]
           ,Rec.IDRecipe
		    ,RecLang.RecipeName + ' - ' + STUFF((
			SELECT RecProLang.RecipeProperty + ',' + RecProLang.RecipePropertyToolTip + ', '
		    FROM [dbo].[RecipePropertiesLanguages] RecProLang INNER JOIN [dbo].[RecipesPropertiesValues] RecPropVal
			ON RecProLang.IDRecipeProperty = RecPropVal.IDRecipeProperty
			WHERE RecPropVal.IDRecipe = Rec.IDRecipe
				AND RecProLang.IDLanguage = RecLang.IDLanguage
			FOR XML PATH('')
			),1,0,'') AS RecipeName
			,RecLang.IDLanguage
			,3 AS SearchPreference
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,@CurrentDate AS IndexInserted
		FROM @NewRecipesForIndex NewRecipes INNER JOIN Recipes Rec 
							ON NewRecipes.IDRecipe = Rec.IDRecipe INNER JOIN RecipesLanguages RecLang
							ON Rec.IDRecipe = RecLang.IDRecipe INNER JOIN [RecipesIngredients]
							ON Rec.IDRecipe = [RecipesIngredients].IDRecipe INNER JOIN [IngredientsLanguages]
							ON [dbo].[IngredientsLanguages].IDIngredient = [dbo].[RecipesIngredients].IDIngredient
		GROUP BY RecLang.[IDRecipeLanguage]
				   ,Rec.IDRecipe
				   ,RecLang.RecipeName
				   ,RecLang.IDLanguage
				   ,[PreparationTimeMinute]
				   ,[CookingTimeMinute]
				   ,[RecipeDifficulties]
				   ,[IDRecipeImage]
				   ,[CreationDate]
				   ,[LastUpdate]
				   ,[RecipeConsulted]
				   ,[RecipeAvgRating]
				   ,[isStarterRecipe]
				   ,[DeletedOn]
				   ,[RecipeEnabled]
				   ,[Checked]
				   ,[RecipePortionKcal]
				   ,[Vegetarian]
				   ,[Vegan]
				   ,[GlutenFree]
				   ,[Draft]

	-- *******************************************
	-- 3rd preference, info from recipe property
	-- END 3rd PREFERENCE
	-- *******************************************

	-- *******************************************
	-- 4th preference, info from recipe calculate tags
	-- *******************************************
	INSERT INTO [dbo].[RecipesIndex]
           ([IDRecipeLanguage]
           ,[IDRecipe]
           ,[RecipeName]
           ,[IDLanguage]
           ,[SearchPreference]
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,[IndexInserted])
	SELECT [IDRecipeLanguage]
           ,Recipes.IDRecipe
           ,[RecipeName] + ' - ' + RecipeLanguageTags AS RecipeName
           ,[IDLanguage]
           ,4 AS SearchPreference
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,@CurrentDate AS IndexInserted
	FROM @NewRecipesForIndex NewRecipes INNER JOIN Recipes
					ON NewRecipes.IDRecipe = Recipes.IDRecipe INNER JOIN RecipesLanguages
					ON Recipes.IDRecipe = RecipesLanguages.IDRecipe

	-- *******************************************
	-- 4th preference, info from recipe calculate tags
	-- END 4th PREFERENCE
	-- *******************************************

	-- *******************************************
	-- 5th preference, info from recipe steps
	-- *******************************************
	INSERT INTO [dbo].[RecipesIndex]
           ([IDRecipeLanguage]
           ,[IDRecipe]
           ,[RecipeName]
           ,[IDLanguage]
           ,[SearchPreference]
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,[IndexInserted])
	SELECT RecLang.[IDRecipeLanguage]
           ,Rec.IDRecipe
		    ,RecLang.RecipeName + ' - ' + STUFF((
			SELECT RecStep.RecipeStep + ', '
		    FROM RecipesLanguages RecLangInt INNER JOIN [dbo].[RecipesSteps] RecStep
			ON RecLangInt.IDRecipeLanguage = RecStep.IDRecipeLanguage
			WHERE RecLangInt.IDRecipe = Rec.IDRecipe
				AND RecLangInt.IDLanguage = RecLang.IDLanguage
			FOR XML PATH('')
			),1,0,'') AS RecipeName
			,RecLang.IDLanguage
			,5 AS SearchPreference
           ,[PreparationTimeMinute]
           ,[CookingTimeMinute]
           ,[RecipeDifficulties]
           ,[IDRecipeImage]
           ,[CreationDate]
           ,[LastUpdate]
           ,[RecipeConsulted]
           ,[RecipeAvgRating]
           ,[isStarterRecipe]
           ,[DeletedOn]
           ,[RecipeEnabled]
           ,[Checked]
           ,[RecipePortionKcal]
           ,[Vegetarian]
           ,[Vegan]
           ,[GlutenFree]
           ,[Draft]
           ,@CurrentDate AS IndexInserted
	FROM @NewRecipesForIndex NewRecipes INNER JOIN Recipes Rec 
							ON NewRecipes.IDRecipe = Rec.IDRecipe INNER JOIN RecipesLanguages RecLang
						ON Rec.IDRecipe = RecLang.IDRecipe INNER JOIN [RecipesIngredients]
						ON Rec.IDRecipe = [RecipesIngredients].IDRecipe INNER JOIN [IngredientsLanguages]
						ON [dbo].[IngredientsLanguages].IDIngredient = [dbo].[RecipesIngredients].IDIngredient
	GROUP BY RecLang.[IDRecipeLanguage]
			   ,Rec.IDRecipe
			   ,RecLang.RecipeName
			   ,RecLang.IDLanguage
			   ,[PreparationTimeMinute]
			   ,[CookingTimeMinute]
			   ,[RecipeDifficulties]
			   ,[IDRecipeImage]
			   ,[CreationDate]
			   ,[LastUpdate]
			   ,[RecipeConsulted]
			   ,[RecipeAvgRating]
			   ,[isStarterRecipe]
			   ,[DeletedOn]
			   ,[RecipeEnabled]
			   ,[Checked]
			   ,[RecipePortionKcal]
			   ,[Vegetarian]
			   ,[Vegan]
			   ,[GlutenFree]
			   ,[Draft]

	-- *******************************************
	-- 5th preference, info from recipe steps
	-- END 5th PREFERENCE
	-- *******************************************

	--Clear Emty Values
	DELETE FROM [dbo].[RecipesIndex]
		WHERE RecipeName IS NULL OR RecipeName = ''
END





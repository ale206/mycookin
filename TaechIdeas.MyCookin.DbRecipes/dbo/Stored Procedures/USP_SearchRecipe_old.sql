




-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 26/08/2013>
-- Description:	<Description, [USP_SearchRecipe]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SearchRecipe_old](@QueryString NVARCHAR(100),
														@Vegan BIT,
														@Vegetarian BIT,
														@GlutenFree BIT,
														@LightThreshold FLOAT,
														@QuickThreshold INT,
														@OffsetRows INT,
														@FetchRows INT)
AS
	BEGIN TRY
		DECLARE @FulltextVar nvarchar(100);
		DECLARE @ContainsVar  nvarchar(100);
		SET @FulltextVar = REPLACE(REPLACE(@QueryString,'"',' '),'''',' ')
		SET @ContainsVar = '"'+@FulltextVar+'*"'
		SELECT IDRecipe, min(Preference) as Preference, avg(rank) as rank, avg(RecipeAvgRating) as RecipeAvgRating  FROM
		(
			SELECT TOP 100 Recipes.IDRecipe, 0 AS Preference, 0 as rank, RecipeAvgRating, RecipeName, 
						IDRecipeFather, Vegan, Vegetarian, GlutenFree, RecipeEnabled, PreparationTimeMinute, 
						CookingTimeMinute, RecipePortionKcal, Draft
			FROM Recipes INNER JOIN RecipesLanguages
			ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
			WHERE RecipeName = @QueryString
			UNION
			SELECT TOP 100 Recipes.IDRecipe, 1 AS Preference, 1 as rank, RecipeAvgRating, RecipeName, 
						IDRecipeFather, Vegan, Vegetarian, GlutenFree, RecipeEnabled, PreparationTimeMinute, 
						CookingTimeMinute, RecipePortionKcal, Draft
			FROM Recipes INNER JOIN RecipesLanguages
			ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
			WHERE CONTAINS(RecipeName,@ContainsVar)
			UNION
			SELECT TOP 100 Recipes.IDRecipe, 2 AS Preference, rank, RecipeAvgRating, RecipeName, 
						IDRecipeFather, Vegan, Vegetarian, GlutenFree, RecipeEnabled, PreparationTimeMinute, 
						CookingTimeMinute, RecipePortionKcal, Draft
			FROM Recipes INNER JOIN RecipesLanguages
			ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
			INNER JOIN freetexttable(RecipesLanguages,RecipeName,@FulltextVar) as t
			on RecipesLanguages.[IDRecipeLanguage] = t.[KEY]
			UNION
			SELECT TOP 100 Recipes.IDRecipe, 3 AS Preference, rank, RecipeAvgRating, RecipeName, 
						IDRecipeFather, Vegan, Vegetarian, GlutenFree, RecipeEnabled, PreparationTimeMinute, 
						CookingTimeMinute, RecipePortionKcal, Draft
			FROM Recipes INNER JOIN RecipesLanguages
			ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
			INNER JOIN freetexttable(RecipesLanguages,RecipeLanguageTags,@FulltextVar) as t
			on RecipesLanguages.[IDRecipeLanguage] = t.[KEY]
			UNION
			SELECT TOP 100 Recipes.IDRecipe, 4 AS Preference, 1000 AS rank, RecipeAvgRating, RecipeName, 
						IDRecipeFather, Vegan, Vegetarian, GlutenFree, RecipeEnabled, PreparationTimeMinute, 
						CookingTimeMinute, RecipePortionKcal, Draft
			FROM Recipes INNER JOIN RecipesLanguages
			ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
			INNER JOIN RecipesSteps
			on RecipesLanguages.IDRecipeLanguage = RecipesSteps.IDRecipeLanguage
			WHERE freetext(RecipeStep,@FulltextVar)
			) t
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
					AND Draft = 0
					AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 1 AND @QuickThreshold )
					AND [RecipePortionKcal] BETWEEN 1 AND @LightThreshold
			group by IDRecipe
			order by Preference ASC, rank desc, RecipeAvgRating desc
			OFFSET @OffsetRows ROWS
    		FETCH NEXT @FetchRows ROWS ONLY		
		
	END TRY
	BEGIN CATCH
		SELECT IDRecipe, min(Preference) as Preference, avg(rank) as rank, avg(RecipeAvgRating) as RecipeAvgRating FROM
		(
			SELECT TOP 100 Recipes.IDRecipe, 0 AS Preference, 0 as rank, RecipeAvgRating, RecipeName, 
						IDRecipeFather, Vegan, Vegetarian, GlutenFree, RecipeEnabled, PreparationTimeMinute, 
						CookingTimeMinute, RecipePortionKcal 
			FROM Recipes INNER JOIN RecipesLanguages
			ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
			WHERE RecipeName = @QueryString
					AND (IDRecipeFather IS NULL OR IDRecipeFather='00000000-0000-0000-0000-000000000000')
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
					AND Draft = 0
					AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 1 AND @QuickThreshold )
					AND [RecipePortionKcal] BETWEEN 1 AND @LightThreshold
			UNION
			SELECT TOP 100 Recipes.IDRecipe, 1 AS Preference, 1 as rank, RecipeAvgRating, RecipeName, 
						IDRecipeFather, Vegan, Vegetarian, GlutenFree, RecipeEnabled, PreparationTimeMinute, 
						CookingTimeMinute, RecipePortionKcal 
			FROM Recipes INNER JOIN RecipesLanguages
			ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
			WHERE RecipeName like @QueryString+'%' 
					AND (IDRecipeFather IS NULL OR IDRecipeFather='00000000-0000-0000-0000-000000000000')
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
					AND Draft = 0
					AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 1 AND @QuickThreshold )
					AND [RecipePortionKcal] BETWEEN 1 AND @LightThreshold
				) t
			group by IDRecipe
			order by Preference ASC, rank desc, RecipeAvgRating desc
			OFFSET @OffsetRows ROWS
    		FETCH NEXT @FetchRows ROWS ONLY
	  
			 DECLARE @ErrorNumber varchar(MAX)		= ERROR_NUMBER();
			 DECLARE @ErrorSeverity varchar(MAX)	= ERROR_SEVERITY();
			 DECLARE @ErrorState varchar(MAX)		= ERROR_STATE();
			 DECLARE @ErrorProcedure varchar(MAX)	= ERROR_PROCEDURE();
			 DECLARE @ErrorLine varchar(MAX)		= ERROR_LINE();
			 DECLARE @ErrorMessage varchar(MAX)		= ERROR_MESSAGE();
			 DECLARE @DateError smalldatetime		= GETUTCDATE();
			 DECLARE @ErrorMessageCode varchar(MAX)	= 'RC-ER-9999';

	
			
 
			 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
			 DECLARE @FileOrigin varchar(150) = '';
	 
			 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
																@ErrorProcedure, @ErrorLine, @ErrorMessage,
																@FileOrigin, @DateError, @ErrorMessageCode,
																1,0,NULL,0,0
	END CATCH



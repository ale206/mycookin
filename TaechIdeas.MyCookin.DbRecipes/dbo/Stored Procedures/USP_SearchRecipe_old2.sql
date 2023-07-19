-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 26/08/2013>
-- Description:	<Description, [USP_SearchRecipe]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SearchRecipe](@QueryString NVARCHAR(100),
														@IDLanguage INT,
														@Vegan BIT,
														@Vegetarian BIT,
														@GlutenFree BIT,
														@LightThreshold FLOAT,
														@QuickThreshold INT,
														@OffsetRows INT,
														@FetchRows INT)
AS
	BEGIN TRY
		DECLARE @LanguageCode nvarchar(5);
		DECLARE @FulltextVar nvarchar(100);
		DECLARE @ContainsVar  nvarchar(100);
		DECLARE @Sql nvarchar(MAX);
		DECLARE @ParameterSql nvarchar(MAX);
		SET @ParameterSql='@QueryString NVARCHAR(100),
														@IDLanguage INT,
														@Vegan BIT,
														@Vegetarian BIT,
														@GlutenFree BIT,
														@LightThreshold FLOAT,
														@QuickThreshold INT,
														@OffsetRows INT,
														@FetchRows INT,
														@FulltextVar nvarchar(100)'
		SET @FulltextVar = REPLACE(REPLACE(@QueryString,'"',' '),'''',' ')
		SET @ContainsVar = '"'+@FulltextVar+'*"'

		SELECT @LanguageCode = CASE @IDLanguage
			WHEN 1 THEN 'En'
			WHEN 2 THEN 'It'
			WHEN 3 THEN 'Es'
			ELSE 'En'
		END

		IF @FetchRows = 1
			BEGIN
				DECLARE @NumFound INT
				SELECT @NumFound = COUNT(IDRecipe) FROM RecipesLanguages 
					WHERE RecipeName = @QueryString AND IDLanguage = @IDLanguage
			END

		IF @NumFound > 0
			BEGIN
				SELECT IDRecipe, 0 as Preference, 100 as rank, 5 as RecipeAvgRating FROM RecipesLanguages 
					WHERE RecipeName = @QueryString AND IDLanguage = @IDLanguage
			END
		ELSE
			BEGIN
				--14/11/2015	Added ", RecipeName" and IDRecipeImage in SELECT... (row 57) and in group by (row 73)
				SET @Sql ='SELECT IDRecipe, min(SearchPreference) as Preference, avg(rank) as rank, avg(RecipeAvgRating) as RecipeAvgRating, RecipeName, IDRecipeImage  
							FROM freetexttable(vIndexedRecipesLang_'+@LanguageCode+',RecipeName,@FulltextVar) t1
									INNER JOIN RecipesIndex ON t1.[KEY] = RecipesIndex.IDRecipeIndex
							WHERE (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
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
								group by IDRecipe, isStarterRecipe, RecipeName, IDRecipeImage
								order by Preference ASC, rank desc, isStarterRecipe, RecipeAvgRating desc
								OFFSET @OffsetRows ROWS
    							FETCH NEXT @FetchRows ROWS ONLY'
					EXECUTE sp_executesql @Sql,	@ParameterSql,@QueryString=@QueryString,
																@Vegan=@Vegan,
																@Vegetarian=@Vegetarian,
																@GlutenFree=@GlutenFree,
																@QuickThreshold = @QuickThreshold,
																@LightThreshold = @LightThreshold,
																@OffsetRows = @OffsetRows,
																@FetchRows = @FetchRows,
																@FulltextVar = @FulltextVar,
																@IDLanguage = @IDLanguage
			END
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




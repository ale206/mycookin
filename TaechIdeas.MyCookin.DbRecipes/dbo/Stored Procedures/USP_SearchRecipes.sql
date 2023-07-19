
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <14/05/2016>
-- Description:	<Search Recipes>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SearchRecipes]
	@IDLanguage INT,
	@Vegan BIT,
	@Vegetarian BIT,
	@GlutenFree BIT,
	@LightThreshold FLOAT,
	@QuickThreshold INT,
	@offset int,
	@count int,
	@orderBy nvarchar(100),
	@isAscendent bit,			
	@search nvarchar(250)
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
	
	SET NOCOUNT ON;

	DECLARE @FulltextVar nvarchar(100);
	SET @FulltextVar = REPLACE(REPLACE(@search,'"',' '),'''',' ')
	
	--TODO: IMPROVE THIS! THE ONLY CHANGE IS WHICH VIEW MUST BE USED

		
			
		IF (@IDLanguage = 1)
		BEGIN
			WITH Results AS 
				(
					SELECT Rank, IDRecipeLanguage, IDRecipe, RecipeAvgRating, RecipeName, IDRecipeImage, FriendlyId,
			
					--FOR ALL SP WITH PAGINATION
					ROW_NUMBER() OVER 
						(	
							--CHANGE ORDERS HERE
							ORDER BY rank DESC, RecipeAvgRating DESC
						) as RowNumber,

					--FOR ALL SP WITH PAGINATION
					COUNT(*) OVER () as TotalCount
				FROM	
				(	
					SELECT [rank] as Rank, RL.IDRecipeLanguage, RL.IDRecipe, R.RecipeAvgRating, RL.RecipeName, R.IDRecipeImage, RL.FriendlyId
					FROM freetexttable(vAllRecipesNames_En, RecipeName, @FulltextVar) T 
							INNER JOIN RecipesLanguages RL ON T.[KEY] = RL.IDRecipeLanguage
							INNER JOIN Recipes R ON RL.IDRecipe = R.IDRecipe
					WHERE	RecipeEnabled = 1
							AND Draft = 0
							AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 0 AND @QuickThreshold )
							AND [RecipePortionKcal] BETWEEN 0 AND @LightThreshold
							AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
								OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
								OR isnull(vegan,0)=@vegan)
							AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
								OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
								OR isnull(Vegetarian,0)=@Vegetarian)
							AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
								OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
								OR isnull(GlutenFree,0)=@GlutenFree)
					--ORDER BY rank DESC, RecipeAvgRating DESC
					) as cpo
				)
			--FOR ALL SP WITH PAGINATION
			SELECT *, COUNT(*) OVER () AS RowsReturned FROM Results
			WHERE
				RowNumber between @offset and (@offset+@count)	
			ORDER BY 
				CASE WHEN @isAscendent = 1 THEN RowNumber END ASC,
				CASE WHEN @isAscendent = 0 THEN RowNumber END DESC
		END
		ELSE IF(@IDLanguage = 2)
			BEGIN
				WITH Results AS 
				(
					SELECT Rank, IDRecipeLanguage, IDRecipe, RecipeAvgRating, RecipeName, IDRecipeImage, FriendlyId,
			
					--FOR ALL SP WITH PAGINATION
					ROW_NUMBER() OVER 
						(	
							--CHANGE ORDERS HERE
							ORDER BY rank DESC, RecipeAvgRating DESC
						) as RowNumber,

					--FOR ALL SP WITH PAGINATION
					COUNT(*) OVER () as TotalCount
				FROM	
				(	
					SELECT [rank] as Rank, RL.IDRecipeLanguage, RL.IDRecipe, R.RecipeAvgRating, RL.RecipeName, R.IDRecipeImage, RL.FriendlyId
					FROM freetexttable(vAllRecipesNames_It, RecipeName, @FulltextVar) T 
							INNER JOIN RecipesLanguages RL ON T.[KEY] = RL.IDRecipeLanguage
							INNER JOIN Recipes R ON RL.IDRecipe = R.IDRecipe
					WHERE	RecipeEnabled = 1
							AND Draft = 0
							AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 0 AND @QuickThreshold )
							AND [RecipePortionKcal] BETWEEN 0 AND @LightThreshold
							AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
								OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
								OR isnull(vegan,0)=@vegan)
							AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
								OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
								OR isnull(Vegetarian,0)=@Vegetarian)
							AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
								OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
								OR isnull(GlutenFree,0)=@GlutenFree)
					--ORDER BY rank DESC, RecipeAvgRating DESC
					) as cpo
				)
			--FOR ALL SP WITH PAGINATION
			SELECT *, COUNT(*) OVER () AS RowsReturned FROM Results
			WHERE
				RowNumber between @offset and (@offset+@count)	
			ORDER BY 
				CASE WHEN @isAscendent = 1 THEN RowNumber END ASC,
				CASE WHEN @isAscendent = 0 THEN RowNumber END DESC
			END
		ELSE IF(@IDLanguage = 3)
			BEGIN
				WITH Results AS 
				(
					SELECT Rank, IDRecipeLanguage, IDRecipe, RecipeAvgRating, RecipeName, IDRecipeImage, FriendlyId,
			
					--FOR ALL SP WITH PAGINATION
					ROW_NUMBER() OVER 
						(	
							--CHANGE ORDERS HERE
							ORDER BY rank DESC, RecipeAvgRating DESC
						) as RowNumber,

					--FOR ALL SP WITH PAGINATION
					COUNT(*) OVER () as TotalCount
				FROM	
				(	
					SELECT [rank] as Rank, RL.IDRecipeLanguage, RL.IDRecipe, R.RecipeAvgRating, RL.RecipeName, R.IDRecipeImage, RL.FriendlyId
					FROM freetexttable(vAllRecipesNames_Es, RecipeName, @FulltextVar) T 
							INNER JOIN RecipesLanguages RL ON T.[KEY] = RL.IDRecipeLanguage
							INNER JOIN Recipes R ON RL.IDRecipe = R.IDRecipe
					WHERE	RecipeEnabled = 1
							AND Draft = 0
							AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 0 AND @QuickThreshold )
							AND [RecipePortionKcal] BETWEEN 0 AND @LightThreshold
							AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
								OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
								OR isnull(vegan,0)=@vegan)
							AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
								OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
								OR isnull(Vegetarian,0)=@Vegetarian)
							AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
								OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
								OR isnull(GlutenFree,0)=@GlutenFree)
					--ORDER BY rank DESC, RecipeAvgRating DESC
					) as cpo
				)
			--FOR ALL SP WITH PAGINATION
			SELECT *, COUNT(*) OVER () AS RowsReturned FROM Results
			WHERE
				RowNumber between @offset and (@offset+@count)	
			ORDER BY 
				CASE WHEN @isAscendent = 1 THEN RowNumber END ASC,
				CASE WHEN @isAscendent = 0 THEN RowNumber END DESC
			END
		
END TRY
BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

  -- There was an error
  IF @@TRANCOUNT > 0
     ROLLBACK

	 SET @ResultExecutionCode = 'RC-ER-9999' --Error in the StoredProcedure
	 SET @isError = 1
	  
	 DECLARE @ErrorNumber nvarchar(MAX)			= ERROR_NUMBER();
	 DECLARE @ErrorSeverity nvarchar(MAX)		= ERROR_SEVERITY();
	 DECLARE @ErrorState nvarchar(MAX)			= ERROR_STATE();
	 DECLARE @ErrorProcedure nvarchar(MAX)		= ERROR_PROCEDURE();
	 DECLARE @ErrorLine nvarchar(MAX)			= ERROR_LINE();
	 DECLARE @ErrorMessage nvarchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime			= GETUTCDATE();
	 DECLARE @ErrorMessageCode nvarchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorMessage
	
	 --SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin nvarchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
																		 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================
END CATCH
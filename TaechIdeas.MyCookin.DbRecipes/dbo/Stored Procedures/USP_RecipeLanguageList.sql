-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <30/01/2016>
-- Description:	<Get a list of all Recipes with filter>
-- =============================================
CREATE PROCEDURE [dbo].[USP_RecipeLanguageList]
	@IDLanguage int,
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

	WITH Results AS 
	(
		SELECT	
			--Recipe
			IDRecipe,IDRecipeFather,IDOwner,NumberOfPerson,PreparationTimeMinute,CookingTimeMinute,RecipeDifficulties,IDRecipeImage,IDRecipeVideo,
			IDCity,CreationDate,LastUpdate,UpdatedByUser,RecipeConsulted,RecipeAvgRating,isStarterRecipe,DeletedOn,BaseRecipe,RecipeEnabled,Checked,
			RecipeCompletePerc,RecipePortionKcal,RecipePortionProteins,RecipePortionFats,RecipePortionCarbohydrates,RecipePortionQta,Vegetarian,Vegan,
			GlutenFree,HotSpicy,RecipePortionAlcohol,Draft,RecipeRated,
			--RecipeLanguage
			IDRecipeLanguage, IDLanguage,RecipeName,RecipeLanguageAutoTranslate,RecipeHistory,RecipeHistoryDate,RecipeNote,RecipeSuggestion,RecipeDisabled,GeoIDRegion,
			RecipeLanguageTags,OriginalVersion,TranslatedBy,FriendlyId,

			--FOR ALL SP WITH PAGINATION
			ROW_NUMBER() OVER 
				(	
					--CHANGE ORDERS HERE
					ORDER BY
						CASE WHEN @orderby='RecipeName' THEN RecipeName END,				
						CASE WHEN @orderby='CreationDate' THEN CreationDate END,
						CASE WHEN @orderby='LastUpdate' THEN LastUpdate END
				) as RowNumber,

			--FOR ALL SP WITH PAGINATION
		    COUNT(*) OVER () as TotalCount
		FROM	
		(	
			SELECT
				--Recipe
				R.IDRecipe,R.IDRecipeFather,R.IDOwner,R.NumberOfPerson,R.PreparationTimeMinute,R.CookingTimeMinute,R.RecipeDifficulties,R.IDRecipeImage,R.IDRecipeVideo,
				R.IDCity,R.CreationDate,R.LastUpdate,R.UpdatedByUser,R.RecipeConsulted,R.RecipeAvgRating,R.isStarterRecipe,R.DeletedOn,R.BaseRecipe,R.RecipeEnabled,R.Checked,
				R.RecipeCompletePerc,R.RecipePortionKcal,R.RecipePortionProteins,R.RecipePortionFats,R.RecipePortionCarbohydrates,R.RecipePortionQta,R.Vegetarian,R.Vegan,
				R.GlutenFree,R.HotSpicy,R.RecipePortionAlcohol,R.Draft,R.RecipeRated,
				--RecipeLanguage
				L.IDRecipeLanguage, IDLanguage,L.RecipeName,L.RecipeLanguageAutoTranslate,L.RecipeHistory,L.RecipeHistoryDate,L.RecipeNote,L.RecipeSuggestion,L.RecipeDisabled,L.GeoIDRegion,
				L.RecipeLanguageTags,L.OriginalVersion,L.TranslatedBy,L.FriendlyId
			FROM Recipes R	INNER JOIN RecipesLanguages L
				ON R.IDRecipe = L.IDRecipe
				WHERE 
					IDLanguage = @IDLanguage 
					and ((RecipeName LIKE '%' + @search + '%') OR (FriendlyId LIKE '%' + @search + '%') )
		) as cpo
	)
	--FOR ALL SP WITH PAGINATION
	SELECT *, COUNT(*) OVER () AS RowsReturned FROM Results
	WHERE
		RowNumber between @offset and (@offset+@count)	
	ORDER BY 
		CASE WHEN @isAscendent = 1 THEN RowNumber END ASC,
		CASE WHEN @isAscendent = 0 THEN RowNumber END DESC

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




	




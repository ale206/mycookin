

-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, USP_GetRecipeByIDRecipeIDLanguage>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipeByIDRecipeIDLanguage](@IDRecipe uniqueidentifier, @IDLanguage INT)
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY

	DECLARE @testExist int
	
	IF EXISTS (SELECT IDRecipeLanguage FROM  dbo.RecipesLanguages 
				WHERE IDRecipe = @IDRecipe AND  IDLanguage = @IDLanguage)
		SET @testExist = 1
	ELSE
		SET @testExist = 0
	
	IF @testExist = 0
	BEGIN
		SELECT @IDLanguage  = MIN(IDLanguage) FROM dbo.RecipesLanguages 
		WHERE IDRecipe = @IDRecipe
	END

	SELECT Vegetarian, Vegan, GlutenFree, IDRecipeLanguage, dbo.RecipesLanguages.IDRecipe, 
			IDLanguage, RecipeName, 
			RecipeLanguageAutoTranslate, RecipeHistory, RecipeHistoryDate,
			RecipeNote,RecipeSuggestion, RecipeDisabled, GeoIDRegion, RecipeLanguageTags,OriginalVersion,TranslatedBy
	FROM dbo.RecipesLanguages INNER JOIN dbo.Recipes
	ON dbo.RecipesLanguages.IDRecipe = dbo.Recipes.IDRecipe
	WHERE  dbo.RecipesLanguages.IDRecipe = @IDRecipe 
			AND IDLanguage = @IDLanguage
			AND DeletedOn IS NULL
			AND RecipeDisabled = 0
	
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
					
	SELECT 0 AS NewIngredientToRecipeAdded													 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH



-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <29/02/2016,,>
-- Last Edit:   <,,>
-- Description:	<Get Properties List By Recipe And Language,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_PropertiesByRecipeAndLanguage]
	@IDLanguage int,
	@IDRecipe uniqueidentifier
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY

	SELECT RecipePropertiesTypesLanguages.IDRecipePropertyType,RecipePropertiesTypesLanguages.RecipePropertyType,RecipePropertiesLanguages.RecipeProperty
	FROM  RecipePropertiesTypes INNER JOIN
			 RecipePropertiesTypesLanguages ON RecipePropertiesTypes.IDRecipePropertyType = RecipePropertiesTypesLanguages.IDRecipePropertyType INNER JOIN
			 RecipeProperties ON RecipePropertiesTypes.IDRecipePropertyType = RecipeProperties.IDRecipePropertyType INNER JOIN
			 RecipesPropertiesValues ON RecipeProperties.IDRecipeProperty = RecipesPropertiesValues.IDRecipeProperty INNER JOIN
			 RecipePropertiesLanguages ON RecipeProperties.IDRecipeProperty = RecipePropertiesLanguages.IDRecipeProperty
	WHERE (RecipePropertiesTypesLanguages.IDLanguage = @IDLanguage) AND (RecipesPropertiesValues.IDRecipe = @IDRecipe) AND (RecipePropertiesTypes.Enabled = 1) AND (RecipePropertiesLanguages.IDLanguage = @IDLanguage)
	ORDER BY RecipePropertiesTypesLanguages.IDRecipePropertyType

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
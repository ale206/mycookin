-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <09/02/2016,,>
-- Last Edit:   <,,>
-- Description:	<Get Properties List By Type, Language and Recipe,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetPropertiesListByTypeLanguageAndRecipe]
	@IDLanguage int,
	@IDRecipePropertyType int,
	@IDRecipe uniqueidentifier
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY

	SELECT	RPT.IDRecipePropertyType, RPTL.RecipePropertyType, RPL.IDRecipeProperty, RPL.RecipeProperty, RPL.IDLanguage, 
			RPT.isPeriodType, RPT.isUseType, RPT.isEatType, RPT.isCookingType, RPT.isDishType, RPT.isColorType,
			RPV.Value
	FROM	RecipeProperties AS RP INNER JOIN
				RecipePropertiesLanguages AS RPL ON RP.IDRecipeProperty = RPL.IDRecipeProperty INNER JOIN
				RecipePropertiesTypes AS RPT ON RP.IDRecipePropertyType = RPT.IDRecipePropertyType INNER JOIN
				RecipePropertiesTypesLanguages AS RPTL ON RPT.IDRecipePropertyType = RPTL.IDRecipePropertyType INNER JOIN
				RecipesPropertiesValues RPV ON RP.IDRecipeProperty = RPV.IDRecipeProperty
	WHERE	(RPT.IDRecipePropertyType = @IDRecipePropertyType) AND 
			(RPTL.IDLanguage = @IDLanguage) AND 
			(RPL.IDLanguage = @IDLanguage) AND 
			(RP.[Enabled] = 1) AND
			(RPV.IDRecipe = @IDRecipe)
	ORDER BY RPL.RecipeProperty


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
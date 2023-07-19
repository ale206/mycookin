-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/02/2016,,>
-- Description:	<Update Ingredient Language>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UpdateIngredientLanguage]
	@IDIngredientLanguage uniqueidentifier,
	@IngredientSingular nvarchar(250),
	@IngredientPlural nvarchar(250),
	@IngredientDescription nvarchar(500),
	@isAutoTranslate bit,
	@IDIngredient uniqueidentifier,
	@IDLanguage int,
	@GeoIDRegion int
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

BEGIN TRY
   
   --insert
   IF EXISTS (SELECT IDIngredientLanguage FROM dbo.IngredientsLanguages WHERE IDIngredientLanguage = @IDIngredientLanguage)
   BEGIN 
			
	UPDATE  IngredientsLanguages
		SET IngredientSingular = @IngredientSingular,
			IngredientPlural = @IngredientPlural,
			IngredientDescription = @IngredientDescription,
			isAutoTranslate = @isAutoTranslate,
			GeoIDRegion = @GeoIDRegion
		WHERE  IDIngredientLanguage = @IDIngredientLanguage
		
		SELECT 1 AS IngredientLanguageUpdated
   END
ELSE
	BEGIN
		
	SELECT 0 AS IngredientLanguageUpdated
			                            
	END
   
		
		
END TRY

BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

  -- There was an error
  IF @@TRANCOUNT > 0
     ROLLBACK

	 SET @ResultExecutionCode = 'US-ER-9999' --Error in the StoredProcedure
	 SET @isError = 1
	  
	 DECLARE @ErrorNumber varchar(MAX)		= ERROR_NUMBER();
	 DECLARE @ErrorSeverity varchar(MAX)	= ERROR_SEVERITY();
	 DECLARE @ErrorState varchar(MAX)		= ERROR_STATE();
	 DECLARE @ErrorProcedure varchar(MAX)	= ERROR_PROCEDURE();
	 DECLARE @ErrorLine varchar(MAX)		= ERROR_LINE();
	 DECLARE @ErrorMessage varchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime		= GETUTCDATE();
	 DECLARE @ErrorMessageCode varchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorMessage
	
	 --SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin varchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
	
	SELECT 0 AS IngredientLanguageUpdated
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH




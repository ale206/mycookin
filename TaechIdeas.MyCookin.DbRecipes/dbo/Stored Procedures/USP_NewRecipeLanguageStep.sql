-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <30/01/2016,,>
-- Last Edit:   <,,>
-- Description:	<New RecipeLanguage Step,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_NewRecipeLanguageStep]
     @IDRecipeLanguage uniqueidentifier,
     @StepGroup nvarchar(150),
     @StepNumber int,
     @RecipeStep nvarchar(max),
     @StepTimeMinute int,
     @IDRecipeStepImage uniqueidentifier

AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
				
	DECLARE @NewGuid uniqueidentifier
	set @NewGuid= NEWID()

		INSERT INTO dbo.RecipesSteps
           (IDRecipeStep
           ,IDRecipeLanguage
           ,StepGroup
           ,StepNumber
           ,RecipeStep
           ,StepTimeMinute
           ,IDRecipeStepImage)
     VALUES
           (@NewGuid
           ,@IDRecipeLanguage
           ,@StepGroup
           ,@StepNumber
           ,@RecipeStep
           ,@StepTimeMinute
           ,@IDRecipeStepImage)
	
	-- If we reach here, success!
	COMMIT

	SELECT @NewGuid AS NewRecipeLanguageStepId, 1 AS NewRecipeLanguageStepInserted

END TRY

BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

  -- There was an error
  IF @@TRANCOUNT > 0
     ROLLBACK

	 SET @ResultExecutionCode = 'US-ER-9999' --Error in the StoredProcedure
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
					
	SELECT 0 AS NewRecipeLanguageStepInserted
														 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH
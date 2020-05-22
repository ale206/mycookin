-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <09/02/2016,,>
-- Description:	<>
-- =============================================
CREATE PROCEDURE [dbo].[USP_AddRecipeBeverage]	
	 @IDRecipe uniqueidentifier,
	 @IDBeverage uniqueidentifier,
	 @IDUserSuggestedBy uniqueidentifier	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

DECLARE @IDBeverageRecipe uniqueidentifier

BEGIN TRY
   
	
		IF NOT EXISTS (SELECT IDBeverageRecipe FROM BeveragesRecipes WHERE IDRecipe = @IDRecipe AND IDBeverage = @IDBeverage)
			BEGIN

			SET @IDBeverageRecipe = NEWID()

				INSERT INTO [dbo].BeveragesRecipes 
					(IDBeverageRecipe
					   ,IDRecipe
					   ,IDBeverage
					   ,IDUserSuggestedBy
					   ,DateSuggestion
					   ,BeverageRecipeAvgRating) 
				
				VALUES (@IDBeverageRecipe
					   ,@IDRecipe
					   ,@IDBeverage
					   ,@IDUserSuggestedBy
					   ,GETUTCDATE()
					   ,0)
			END
		ELSE
			BEGIN
				SELECT @IDBeverageRecipe = IDBeverageRecipe FROM BeveragesRecipes WHERE IDRecipe = @IDRecipe AND IDBeverage = @IDBeverage
			END
	  
	  SELECT @IDBeverageRecipe AS BeverageRecipeId

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
	
	SELECT @IDBeverageRecipe AS BeverageRecipeId
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH







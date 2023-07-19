




-- ================================================
-- =============================================
-- Author:		<Saverio Cammarata>
-- Create date: <14/04/2013,,>
-- Description:	<[USP_ManageRecipeBeverage],,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageRecipeBeverage]
	
	 @IDBeverageRecipe uniqueidentifier,
     @IDRecipe uniqueidentifier,
     @IDBeverage uniqueidentifier,
     @IDUserSuggestedBy uniqueidentifier,
     @DateSuggestion smalldatetime,
     @BeverageRecipeAvgRating float,
     @isDeleteOperation bit
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
	IF @isDeleteOperation = 1
		BEGIN
			DELETE FROM  dbo.BeveragesRecipes
			WHERE IDBeverageRecipe = @IDBeverageRecipe		
		END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT IDBeverageRecipe FROM BeveragesRecipes WHERE IDBeverageRecipe = @IDBeverageRecipe)
			BEGIN
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
					   ,@DateSuggestion
					   ,@BeverageRecipeAvgRating)
			END
		ELSE
			BEGIN
				UPDATE [dbo].BeveragesRecipes 
				   SET IDRecipe = @IDRecipe,
					   IDBeverage = @IDBeverage,
					   IDUserSuggestedBy = @IDUserSuggestedBy,
					   DateSuggestion = @DateSuggestion,
					   BeverageRecipeAvgRating = @BeverageRecipeAvgRating
				 WHERE IDBeverageRecipe=@IDBeverageRecipe
			END
	END
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='';
		SET @USPReturnValue =@IDBeverageRecipe;
					                            
   COMMIT
		
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
		-- =======================================
		
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
	
	 SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin varchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH







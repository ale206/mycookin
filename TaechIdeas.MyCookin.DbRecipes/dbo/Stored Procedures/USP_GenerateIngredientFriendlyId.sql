-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <27/01/2015,,>
-- Description:	<Generate Friendly Id,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GenerateIngredientFriendlyId]
	@IDIngredientLanguage uniqueidentifier
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================


BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
	
	  DECLARE @FriendlyId nvarchar(500)
	  DECLARE @IngredientSingular nvarchar(500)
	  DECLARE @numberOfSameIngredients int

	  SELECT @IngredientSingular = IngredientSingular, @FriendlyId = [dbo].urlencode(IngredientSingular)
	  FROM [dbo].[IngredientsLanguages]
	  WHERE IDIngredientLanguage = @IDIngredientLanguage

	SELECT @numberOfSameIngredients = COUNT(IngredientSingular) 
	FROM [dbo].[IngredientsLanguages] 
	WHERE [IngredientSingular] = @IngredientSingular AND IDIngredientLanguage <> @IDIngredientLanguage

	IF @numberOfSameIngredients > 0
		BEGIN

			DECLARE @NumberOfIngredients int
			SELECT @NumberOfIngredients = COUNT(1) FROM [dbo].[IngredientsLanguages]
			SET @FriendlyId += '-' + CONVERT(varchar, @NumberOfIngredients)

		END

	  UPDATE [dbo].[IngredientsLanguages] SET [FriendlyId] = @FriendlyId WHERE IDIngredientLanguage = @IDIngredientLanguage
			
	---- If we reach here, success!
   COMMIT
   
	  SELECT @FriendlyId AS FriendlyId
		
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
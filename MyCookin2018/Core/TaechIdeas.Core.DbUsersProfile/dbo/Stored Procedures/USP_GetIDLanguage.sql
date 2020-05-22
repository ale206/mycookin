-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/07/2012>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Get IDLanguage according to language code ("it","en",etc.)> 
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIDLanguage] 
	@LanguageCode nvarchar(50)
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IDLanguage int;
DECLARE @DefaultLanguageId int;

SET @DefaultLanguageId = 1;	--DEFAULT ID LANGUAGE FOR ENGLISH

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
		SELECT TOP 1 @IDLanguage = IDLanguage FROM Languages WHERE LanguageCode = @LanguageCode AND Enabled = 1
	
		-- SET IDLANGUAGE TO 1 (ENGLISH) AS DEFAULT LANGUAGE
		IF (@IDLanguage IS NULL)
			BEGIN
				SET @ResultExecutionCode = @DefaultLanguageId
				SET @isError = 0
			END
		ELSE
			BEGIN
				SET @ResultExecutionCode = @IDLanguage
				SET @isError = 0
			END
			
   -- If we reach here, success!
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
	 
	 DECLARE @ErrorNumber nvarchar(MAX)			= ERROR_NUMBER();
	 DECLARE @ErrorSeverity nvarchar(MAX)		= ERROR_SEVERITY();
	 DECLARE @ErrorState nvarchar(MAX)			= ERROR_STATE();
	 DECLARE @ErrorProcedure nvarchar(MAX)		= ERROR_PROCEDURE();
	 DECLARE @ErrorLine nvarchar(MAX)			= ERROR_LINE();
	 DECLARE @ErrorMessage nvarchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime			= GETUTCDATE();
	 DECLARE @ErrorMessageCode nvarchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorMessage
	
	 SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
	 
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
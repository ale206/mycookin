-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <02/11/2015>
-- Last Edit:   <>
-- Description:	<Get LanguageCode by Id> 
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetLanguageCodeById] 
	@IdLanguage int
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @LanguageCode nvarchar(10);
DECLARE @DefaultLanguageCode nvarchar(10);

SET @DefaultLanguageCode = 'en-GB';	--DEFAULT LANGUAGE ENGLISH

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction

		SELECT TOP 1 @LanguageCode = LanguageCode FROM dbo.Languages WHERE IDLanguage = @IDLanguage AND Enabled = 1
		--SELECT TOP 1 @IDLanguage = IDLanguage FROM Languages WHERE LanguageCode = @LanguageCode AND Enabled = 1
	
		-- SET IDLANGUAGE TO 1 (ENGLISH) AS DEFAULT LANGUAGE
		IF (@LanguageCode IS NULL)
			BEGIN
				SELECT @DefaultLanguageCode				
			END
		ELSE
			BEGIN
				SELECT @LanguageCode
			END
			
   -- If we reach here, success!
   COMMIT
   
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		--SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
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
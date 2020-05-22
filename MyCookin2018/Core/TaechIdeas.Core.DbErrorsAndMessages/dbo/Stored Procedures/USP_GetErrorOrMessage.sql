-- Batch submitted through debugger: USP_GetErrorOrMEssage.sql|0|0|C:\Documents and Settings\zzitibld\Documenti\SQL Server Management Studio\Projects\USP_GetErrorOrMEssage.sql
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/07/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Get Error or Message,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetErrorOrMessage]
	@IDLanguage int,
	@ErrorOrMessageCode nvarchar(10)
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @ErrorOrMessage nvarchar(MAX);

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction

	SELECT TOP 1 @ErrorOrMessage = ErrorMessage FROM ErrorsAndMessages WHERE IDLanguage = @IDLanguage AND ErrorMessageCode = @ErrorOrMessageCode

	IF (@ErrorOrMessage IS NULL)	--IF NULL, GET THE DEFAULT MESSAGE WITH DEFAULT LANGUAGE (ENGLISH)
		BEGIN
			SELECT TOP 1 @ErrorOrMessage = ErrorMessage FROM ErrorsAndMessages WHERE PreferredForErrorCode = 1 AND ErrorMessageCode = @ErrorOrMessageCode

			IF (@ErrorOrMessage IS NULL)	--IF NULL AGAIN, NOT EXISTS ERROR OR MESSAGE FOR THIS CODE
				BEGIN
					SET @ResultExecutionCode = 'ErrorMessage Not Found! Check the ErrorCode sent to USP_GetErrorOrMessage'
					SET @isError = 1					
				END
			ELSE
				BEGIN
					SET @ResultExecutionCode = @ErrorOrMessage
					SET @isError = 0
				END			
		END
	ELSE	--ERROR OR MESSAGE FOUND CORRECTLY
		BEGIN
			SET @ResultExecutionCode = @ErrorOrMessage
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
	 DECLARE @ErrorOrLogMessage nvarchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime			= GETUTCDATE();
	 DECLARE @ErrorMessageCode nvarchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorOrLogMessage
	
	 SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
	 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin varchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorOrLogMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)
	 
-- =======================================  
END CATCH
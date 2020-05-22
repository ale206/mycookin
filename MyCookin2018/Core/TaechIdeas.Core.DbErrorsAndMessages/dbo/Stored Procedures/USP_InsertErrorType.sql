-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <18/06/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Insert New Error Type in ErrorsAndMessages Table ,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_InsertErrorType]
	@IDLanguage int,
	@NewErrorMessageCode nvarchar(10),
	@PreferredForErrorCode bit,
	@NewErrorMessage nvarchar(MAX)
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @MessageExistence nvarchar(MAX);

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
	

	IF NOT EXISTS ( SELECT IDErrorAndMessage FROM ErrorsAndMessages 
					WHERE ErrorMessageCode = @NewErrorMessageCode AND IDLanguage = @IDLanguage) 
		BEGIN
			
			DECLARE @guid uniqueidentifier
			set @guid= NEWID()
			
			INSERT INTO dbo.ErrorsAndMessages(IDErrorAndMessage, IDLanguage, ErrorMessageCode,
												 PreferredForErrorCode, ErrorMessage) 
						    VALUES (@guid, @IDLanguage, @NewErrorMessageCode, @PreferredForErrorCode, 
								   @NewErrorMessage)
			
			SET @ResultExecutionCode = @guid --MESSAGE ADDED CORRECTLY, RETURN GUID
			SET @isError = 0
		END	
	ELSE
		BEGIN
			SET @ResultExecutionCode = 'xx-xx-xxxx' --THE MESSAGE ALREADY EXISTS
			SET @isError = 1
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

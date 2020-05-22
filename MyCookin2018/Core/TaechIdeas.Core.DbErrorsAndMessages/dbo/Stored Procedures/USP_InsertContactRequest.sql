-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <12/09/2013,,>
-- Description:	<Insert new request from Contact Page.>
-- =============================================
CREATE PROCEDURE [dbo].[USP_InsertContactRequest]
	 
	@IDLanguage int,
	@IDContactRequestType int,
	@FirstName nvarchar(250),
	@LastName nvarchar(250),
	@Email nvarchar(250),
	@RequestText nvarchar(2000),
	@PrivacyAccept bit,
	@RequestDate datetime,
	@IpAddress nvarchar(15),
	@IsRequestClosed bit
		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
	BEGIN TRANSACTION    -- Start the transaction
		DECLARE @uid uniqueidentifier;

			set @uid= NEWID()
	
			INSERT INTO dbo.ContactRequests (IDContactRequest, IDLanguage, IDContactRequestType, FirstName, LastName, Email, RequestText,
											 PrivacyAccept, RequestDate, IpAddress, IsRequestClosed)
							   VALUES (@uid, @IDLanguage, @IDContactRequestType, @FirstName, @LastName, @Email, @RequestText,
											 @PrivacyAccept, @RequestDate, @IpAddress, @IsRequestClosed)

		--Return Values
				SET @ResultExecutionCode = ''			-- NEW MESSAGE INSERTED
				SET @USPReturnValue = @uid				--ID OF THE ACTION
				SET @isError = 0

			-- If we reach here, success!
		   COMMIT
   	
			-- FOR ALL STORED PROCEDURE
				-- =======================================
				SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
				-- =======================================

END TRY

BEGIN CATCH

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

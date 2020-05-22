-- Batch submitted through debugger: USP_ActivateUser.sql|0|0|C:\Documents and Settings\zzitibld\Documenti\SQL Server Management Studio\Projects\USP_ActivateUser.sql
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <20/07/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Activate User,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ActivateUser]
	@IDUser uniqueidentifier,
	@MailConfirmedOn smalldatetime,
	@UserEnabled bit,
	@UserLocked bit,
	@LastIpAddress nvarchar(50),
	@AccountExpireOn smalldatetime		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
		-- Check for already activated user
		IF EXISTS (SELECT IDUser FROM dbo.Users WHERE MailConfirmedOn IS NOT NULL AND IDUser = @IDUser) 
			BEGIN
				SET @ResultExecutionCode = 'US-WN-0007' --USER ALREADY ACTIVATED
				SET @USPReturnValue = @IDUser --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
				SET @isError = 0
			END
		ELSE
			BEGIN
				UPDATE dbo.Users SET MailConfirmedOn = @MailConfirmedOn, UserEnabled = @UserEnabled, UserLocked = @UserLocked, LastIpAddress = @LastIpAddress, AccountExpireOn = @AccountExpireOn 
					WHERE IDUser = @IDUser
					
					SET @ResultExecutionCode = 'US-IN-0002' --USER ACTIVATED CORRECTLY
					SET @USPReturnValue = @IDUser --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
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
	  
	 DECLARE @ErrorNumber nvarchar(MAX)		= ERROR_NUMBER();
	 DECLARE @ErrorSeverity nvarchar(MAX)	= ERROR_SEVERITY();
	 DECLARE @ErrorState nvarchar(MAX)		= ERROR_STATE();
	 DECLARE @ErrorProcedure nvarchar(MAX)	= ERROR_PROCEDURE();
	 DECLARE @ErrorLine nvarchar(MAX)		= ERROR_LINE();
	 DECLARE @ErrorMessage nvarchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime		= GETUTCDATE();
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


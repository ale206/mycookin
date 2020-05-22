
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <10/03/2013,,>
-- Last Edit:   <,,>
-- Description:	<Logout User,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_LogoutUser]
	@IDUser uniqueidentifier,
	@LogoutTime smalldatetime,
	@Offset int,
	@UserIsOnline bit,			--0	
	@WebsiteId int
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
   --LOGOUT USER
	--In the future remove all the columns in User that are replicated in UserMembership
	UPDATE dbo.Users SET LastLogout = @LogoutTime, Offset = @Offset, UserIsOnLine = @UserIsOnline
						WHERE (IDUser = @IDUser)


							
	UPDATE dbo.UserMembership 
					SET LastLogout = @LogoutTime, Offset = @Offset, UserIsOnLine = @UserIsOnline
					WHERE UserId = @IDUser AND WebsiteId = @WebsiteID

	SET @ResultExecutionCode = 'US-IN-0010' 
	SET @USPReturnValue = @IDUser --USER BLOCKED CORRECTLY, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
	SET @isError = 0

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


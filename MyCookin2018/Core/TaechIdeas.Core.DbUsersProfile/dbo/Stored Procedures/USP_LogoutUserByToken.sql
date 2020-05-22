-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <24/10/2015,,>
-- Last Edit:   <,,>
-- Description:	<Logout of a user by Token Id,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_LogoutUserByToken]
	@UserToken nvarchar(max)
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @UserLoggedOut bit = 0
DECLARE @IDUser uniqueidentifier
DECLARE @WebsiteId int
DECLARE @ExpireDate datetime = GETUTCDATE()

BEGIN TRY
   BEGIN TRANSACTION
	
	IF EXISTS (SELECT UserId FROM dbo.UserTokens WHERE UserToken = @UserToken AND ExpireAt > @ExpireDate)
		BEGIN		
			SELECT @IDUser = (SELECT UserId FROM dbo.UserTokens WHERE UserToken = @UserToken AND ExpireAt > @ExpireDate)
			SELECT @WebsiteId = (SELECT WebsiteId FROM dbo.UserTokens WHERE UserToken = @UserToken AND UserId = @IDUser)

			--LOGOUT USER
			UPDATE dbo.Users SET LastLogout = GETUTCDATE(), UserIsOnLine = 0 WHERE (IDUser = @IDUser)

			UPDATE dbo.UserMembership 
					SET LastLogout = GETUTCDATE(), UserIsOnLine = 0
					WHERE UserId = @IDUser AND WebsiteId = @WebsiteID

			UPDATE dbo.UserTokens SET ExpireAt = GETUTCDATE() WHERE UserToken = @UserToken
			
			set @UserLoggedOut = 1
		END	
	ELSE
		BEGIN
			set @UserLoggedOut = 0
		END

			-- If we reach here, success!
   COMMIT
   
		-- =======================================
		SELECT @UserLoggedOut AS UserLoggedOut
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


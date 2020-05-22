-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <16/10/2015,,>
-- Last Edit:   <,,>
-- Description:	<Get new token for a user,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetNewToken]
	@IDUser uniqueidentifier,
	@TokenExpireMinutes int,
	@WebsiteId int
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @UserToken uniqueidentifier
DECLARE @guid uniqueidentifier
DECLARE @ExistingToken uniqueidentifier
DECLARE @ExpireDate datetime = DATEADD(MINUTE, @TokenExpireMinutes, GETUTCDATE())

set @UserToken = NEWID()
set @guid= NEWID()

BEGIN TRY
   BEGIN TRANSACTION    -- CHECK IF THE USER HAS ALREADY A TOKEN

		-- Check if already exist a valid token for the user
		IF EXISTS (SELECT UserToken FROM dbo.UserTokens WHERE UserId = @IDUser AND WebsiteId = @WebsiteId AND ExpireAt > GETUTCDATE())
			BEGIN
				SELECT @ExistingToken = UserToken FROM dbo.UserTokens WHERE UserId = @IDUser AND WebsiteId = @WebsiteId AND ExpireAt > GETUTCDATE()

				SET @UserToken = @ExistingToken
			END
		ELSE
			BEGIN
				INSERT INTO dbo.UserTokens (Id, UserToken, UserId, CreatedAt, ExpireAt, WebsiteId) 
						    VALUES (@guid, @UserToken, @IDUser, GETUTCDATE(), DATEADD(MINUTE, @TokenExpireMinutes, GETUTCDATE()), @WebsiteId)
			END
			-- If we reach here, success!

			SELECT @UserToken AS UserToken
   COMMIT
   		
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


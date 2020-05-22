-- Batch submitted through debugger: USP_ActivateUser.sql|0|0|C:\Documents and Settings\zzitibld\Documenti\SQL Server Management Studio\Projects\USP_ActivateUser.sql
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <07/02/2016,,>
-- Last Edit:   <,,>
-- Description:	<Delete Account,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_DeleteAccount]
	@UserToken nvarchar(max)
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY

	DECLARE @IDUser uniqueidentifier
	DECLARE @WebsiteId int
	DECLARE @ExpireDate datetime = GETUTCDATE()

	SELECT @IDUser = (SELECT UserId FROM dbo.UserTokens WHERE UserToken = @UserToken AND ExpireAt > @ExpireDate)
	SELECT @WebsiteId = (SELECT WebsiteId FROM dbo.UserTokens WHERE UserToken = @UserToken AND UserId = @IDUser)

	--DO NOT USE THIS IN THE FUTURE
   UPDATE Users SET AccountDeletedOn = GETUTCDATE() WHERE IDUser = @IDUser

   UPDATE UserMembership SET  AccountDeletedOn = GETUTCDATE() WHERE UserId = @IDUser AND WebsiteId = @WebsiteId

   SELECT 1 AS AccountDeleted
		
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
	
	 --SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin nvarchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
		SELECT 0 AS AccountDeleted		
													 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH


﻿-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <16/10/2015,,>
-- Last Edit:   <,,>
-- Description:	<Check if it is valid a token for a user,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CheckToken]
	@UserToken uniqueidentifier,
	@IDUser uniqueidentifier,
	@TokenRenewMinutes int,
	@WebsiteId int
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IsTokenValid bit = 0

BEGIN TRY
   BEGIN TRANSACTION
	
	IF EXISTS (SELECT UserToken FROM dbo.UserTokens WHERE UserToken = @UserToken AND UserId = @IDUser AND WebsiteId = @WebsiteId AND ExpireAt > GETUTCDATE())
		BEGIN
			--RENEW TOKEN
			UPDATE dbo.UserTokens SET ExpireAt = DATEADD(MINUTE, @TokenRenewMinutes, GETUTCDATE()) WHERE UserToken = @UserToken

			set @IsTokenValid = 1
		END	
			
			--SET @ResultExecutionCode = 'US-IN-0001' --USER ADDED CORRECTLY
			--SET @USPReturnValue = @guid --USER ADDED CORRECTLY, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
			--SET @isError = 0
			-- If we reach here, success!
   COMMIT
   
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		SELECT @IsTokenValid AS IsTokenValid
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


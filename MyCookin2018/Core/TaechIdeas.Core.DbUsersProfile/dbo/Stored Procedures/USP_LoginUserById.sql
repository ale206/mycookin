
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <25/02/2016,,>
-- Last Edit:   <>
-- Description:	<Login User By Id,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_LoginUserById]
	@IDUser uniqueidentifier,
	@Offset int,
	@LastIpAddress nvarchar(50)	--user ip	

AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @UserEnabled bit;
DECLARE @UserLoggedIn bit;


BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
	
	--HERE CHECK ALSO IF THE USER EXIST, BUT DELETED HIS ACCOUNT (AccountDeletedOn not null)	
	IF EXISTS (SELECT IDUser FROM dbo.Users WHERE IDUser = @IDUser AND AccountDeletedOn IS NULL)
	BEGIN
		
		--ELSE
			-- VERIFY IF USER IS LOCKED	(COSA ERA USERENABLED...?)
			SELECT @UserEnabled = UserEnabled FROM dbo.Users WHERE IDUser = @IDUser
			
			IF @UserEnabled = 1
				BEGIN
					
					--UPDATE TABLE USERS
					UPDATE Users SET LastLogon = GETUTCDATE(), Offset = @Offset, UserIsOnLine = 1, LastIpAddress = @LastIpAddress
								 WHERE IDUser = @IDUser

					SET @UserLoggedIn = 1
				END
			ELSE
				BEGIN
					
					SET @UserLoggedIn = 0

				END
	END
	ELSE	
		--IF (@IDUser IS NULL)
			BEGIN
				SET @UserLoggedIn = 0
			END
			
			
	-- If we reach here, success!
   COMMIT
   
		SELECT @UserLoggedIn AS UserLoggedIn, @IDUser AS UserId

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

SELECT 0 AS UserLoggedIn, @IDUser AS UserId

END CATCH
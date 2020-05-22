
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/07/2012,,>
-- Last Edit:   <19/03/2015,,>
-- Description:	<Login User,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_LoginUser]
	@eMail nvarchar(150),
	@PasswordHash nvarchar(100),
	@LastLogon smalldatetime,	--now()
	@Offset int,
	@UserIsOnline bit,			--1
	@LastIpAddress nvarchar(50),	--user ip
	@WebsiteId int		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IDUser uniqueidentifier = NULL;
DECLARE @UserEnabled bit;
DECLARE @UserLocked bit;

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
	
	--HERE CHECK ALSO IF THE USER EXIST, BUT DELETED HIS ACCOUNT (AccountDeletedOn not null)	
	IF EXISTS (
			SELECT IDUser 
			FROM Users U
				INNER JOIN UserMembership UM ON U.IDUser = UM.UserId
			WHERE	U.eMail = @eMail 
				AND U.PasswordHash = @PasswordHash
				AND UM.WebsiteId = @WebsiteId
				AND UM.AccountDeletedOn IS NULL
	)
	BEGIN
			SELECT @IDUser = IDUser 
			FROM Users U
				INNER JOIN UserMembership UM ON U.IDUser = UM.UserId
			WHERE	U.eMail = @eMail 
				AND U.PasswordHash = @PasswordHash
				AND UM.WebsiteId = @WebsiteId
				AND UM.UserEnabled = 1
				AND UM.UserLocked = 0

			SELECT @UserEnabled = UserEnabled FROM dbo.Users WHERE IDUser = @IDUser
			SELECT @UserLocked = UserLocked FROM dbo.Users WHERE IDUser = @IDUser
			
			IF (@UserEnabled = 1 OR @UserLocked = 1)
				BEGIN
					SET @ResultExecutionCode = 'US-IN-0009' --USER LOGGED, NO ERROR.
					SET @USPReturnValue = @IDUser --USER LOGGED, NO ERROR. (NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
					SET @isError = 0
			
					--UPDATE TABLE USERS
					--In the future remove all the columns in User that are replicated in UserMembership
					UPDATE Users SET LastLogon = @LastLogon, Offset = @Offset, UserIsOnLine = @UserIsOnline, LastIpAddress = @LastIpAddress
								 WHERE IDUser = @IDUser

					UPDATE dbo.UserMembership 
					SET LastLogon = @LastLogon, Offset = @Offset, UserIsOnLine = @UserIsOnline, LastIpAddress = @LastIpAddress
					WHERE UserId = @IDUser AND WebsiteId = @WebsiteID

				END
			ELSE
				BEGIN
					SET @ResultExecutionCode = 'US-WN-0002'	--USER NOT ENABLED OR LOCKED
					SET @USPReturnValue = 'USER NOT ENABLED OR LOCKED'
					SET @isError = 1
				END
	END
	ELSE	
		--IF (@IDUser IS NULL)
			BEGIN
				SET @ResultExecutionCode = 'US-WN-0001'	--WRONG USERNAME OR PASSWORD
				SET @USPReturnValue = 'WRONG USERNAME OR PASSWORD'
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
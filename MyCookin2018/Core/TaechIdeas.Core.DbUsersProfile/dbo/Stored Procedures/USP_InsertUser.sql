

-- Batch submitted through debugger: USP_InsertUser.sql|0|0|C:\Documents and Settings\zzitibld\Documenti\SQL Server Management Studio\Projects\USP_InsertUser.sql
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/07/2012,,>
-- Last Edit:   <10/03/2013,,>
-- Description:	<Insert User,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_InsertUser]
	@Name nvarchar(MAX),
	@Surname nvarchar(MAX),
	@UserName nvarchar(MAX),
	@eMail nvarchar(MAX),
	@PasswordHash nvarchar(MAX),
	@BirthDate smalldatetime,
	@LastPasswordChange smalldatetime,
	@PasswordExpireOn smalldatetime,
	@ChangePasswordNextLogon bit,
	@ContractSigned bit,
	@IDLanguage int,
	@UserEnabled bit,
	@UserLocked bit,
	@MantainanceMode bit,
	@DateMembership smalldatetime,
	@AccountExpireOn smalldatetime,
	@LastIpAddress nvarchar(50),
	@LastLogon smalldatetime,
	@UserDomain int,
	@UserType int,
	@MailConfirmedOn smalldatetime,
	@Mobile nvarchar(50),
	@MobileConfirmedOn smalldatetime,
	@MobileConfirmationCode nvarchar(50),
	@IDCity int,
	@IDProfilePhoto uniqueidentifier,
	@IDSecurityQuestion int,
	@SecurityAnswer nvarchar(250),
	@LastProfileUpdate smalldatetime,
	@UserIsOnline bit,
	@IDVisibility int,
	@IDGender int,
	@AccountDeletedOn smalldatetime,
	@LastLogout smalldatetime,
	@Offset int,
	@WebsiteId int
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IDUser uniqueidentifier = NULL;

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
	
	--IF THE USER EXIST, BUT DELETED HIS ACCOUNT (AccountDeletedOn not null), RESTORE HIS REGISTRATION
	IF EXISTS (
			SELECT IDUser 
			FROM Users U
				INNER JOIN UserMembership UM ON U.IDUser = UM.UserId
			WHERE	U.eMail = @eMail 
				AND UM.WebsiteId = @WebsiteId
				AND UM.AccountDeletedOn IS NOT NULL
				AND UM.UserEnabled = 1
				AND UM.UserLocked = 0
	)
		BEGIN
			SELECT @IDUser = IDUser 
			FROM Users U
				INNER JOIN UserMembership UM ON U.IDUser = UM.UserId
			WHERE	U.eMail = @eMail 
				AND UM.WebsiteId = @WebsiteId
			
			--SET AccountDeletedOn TO NULL, AGAIN
			--In the future remove all the columns in User that are replicated in UserMembership
			UPDATE dbo.Users SET AccountDeletedOn = NULL, Name = @Name, Surname = @Surname, BirthDate = @BirthDate, 
								 UserName = @UserName, PasswordHash = @PasswordHash, PasswordExpireOn = @PasswordExpireOn,
								 ChangePasswordNextLogon = @ChangePasswordNextLogon, IDLanguage = @IDLanguage,
								 UserEnabled = 1, UserLocked = 0, MantainanceMode = @MantainanceMode,
								 DateMembership = @DateMembership, AccountExpireOn = @AccountExpireOn, LastLogon = @LastLogon,
								 LastIpAddress = @LastIpAddress
							 WHERE IDUser = @IDUser

			UPDATE dbo.UserMembership SET AccountDeletedOn = NULL, 
						UserEnabled = 1, UserLocked = 0,
						DateMembership = @DateMembership, AccountExpireOn = @AccountExpireOn, LastLogon = @LastLogon,
						LastIpAddress = @LastIpAddress
					WHERE UserId = @IDUser AND WebsiteId = @WebsiteID
			
			SET @ResultExecutionCode = 'US-IN-0037' --USER RESTORED CORRECTLY
			SET @USPReturnValue = @IDUser --USER RESTORED CORRECTLY, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
			SET @isError = 0
			
		END
	-- If already registered in another website
	ELSE IF EXISTS (
			SELECT TOP 1 IDUser 
			FROM Users U
				INNER JOIN UserMembership UM ON U.IDUser = UM.UserId
			WHERE	U.eMail = @eMail 
				AND UM.WebsiteId != @WebsiteId
				AND UM.AccountDeletedOn IS NULL
				AND UM.UserEnabled = 1
				AND UM.UserLocked = 0
	)
		BEGIN
			SELECT TOP 1 @IDUser = IDUser 
			FROM Users U
				INNER JOIN UserMembership UM ON U.IDUser = UM.UserId
			WHERE	U.eMail = @eMail 
				AND UM.WebsiteId != @WebsiteId
				AND UM.AccountDeletedOn IS NULL
				AND UM.UserEnabled = 1
				AND UM.UserLocked = 0

			-- Insert in Membership table
			INSERT INTO [dbo].[UserMembership] 
							([UserId],[WebsiteId],[UserEnabled],[UserLocked],
							[DateMembership],[AccountExpireOn],[LastLogon],[LastLogout],[Offset],
							[UserIsOnline],[LastIpAddress],[AccountDeletedOn])
			VALUES
							(@IDUser, @WebsiteId, @UserEnabled, @UserLocked,
							@DateMembership, @AccountExpireOn, @LastLogon, @LastLogout, @Offset,
							@UserIsOnline, @LastIpAddress, @AccountDeletedOn) 
		END

	ELSE	--NEW USER
		BEGIN
			
			DECLARE @guid uniqueidentifier
			set @guid= NEWID()
			
			INSERT INTO dbo.Users (IDUser, Name, Surname, UserName, PasswordHash, BirthDate, 
								   LastPasswordChange, PasswordExpireOn, ChangePasswordNextLogon,
								   ContractSigned, eMail,
								   IDLanguage, UserEnabled, UserLocked, 
								   MantainanceMode, DateMembership, AccountExpireOn,
								   LastIpAddress, LastLogon, UserDomain, UserType, MailConfirmedOn,
								   Mobile, MobileConfirmedOn, MobileConfirmationCode, IDCity, IDProfilePhoto,
								   IDSecurityQuestion, SecurityAnswer, LastProfileUpdate, UserIsOnLine,
								   IDVisibility, IDGender, AccountDeletedOn, LastLogout, Offset) 
							VALUES (@guid, @Name, @Surname, @UserName, @PasswordHash, @BirthDate, 
								   @LastPasswordChange, @PasswordExpireOn, @ChangePasswordNextLogon,
								   @ContractSigned, @eMail,
								   @IDLanguage, @UserEnabled, @UserLocked, 
								   @MantainanceMode, @DateMembership, @AccountExpireOn,
								   @LastIpAddress, @LastLogon, @UserDomain, @UserType, @MailConfirmedOn,
								   @Mobile, @MobileConfirmedOn, @MobileConfirmationCode, @IDCity,
								   @IDProfilePhoto, @IDSecurityQuestion, @SecurityAnswer, @LastProfileUpdate,
								   @UserIsOnline, @IDVisibility, @IDGender, @AccountDeletedOn,
								   @LastLogout, @Offset)
			

			INSERT INTO [dbo].[UserMembership] 
							([UserId],[WebsiteId],[UserEnabled],[UserLocked],
							[DateMembership],[AccountExpireOn],[LastLogon],[LastLogout],[Offset],
							[UserIsOnline],[LastIpAddress],[AccountDeletedOn])
			VALUES
							(@guid, @WebsiteId, @UserEnabled, @UserLocked,
							@DateMembership, @AccountExpireOn, @LastLogon, @LastLogout, @Offset,
							@UserIsOnline, @LastIpAddress, @AccountDeletedOn)


			SET @ResultExecutionCode = 'US-IN-0001' --USER ADDED CORRECTLY
			SET @USPReturnValue = @guid --USER ADDED CORRECTLY, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
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


-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <26/10/2015>
-- Last Edit:   <,,>
-- Description:	<Get User By Id>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetUserById]
	@IDUser uniqueidentifier
		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   
	  --SELECT  Users.IDUser, Users.Name, Users.Surname, Users.UserName, Users.PasswordHash, 
			--  Users.PasswordExpireOn, Users.ChangePasswordNextLogon, Users.BirthDate, Users.eMail, 
			--  Users.IDLanguage, Users.IDCity, Users.IDProfilePhoto, Users.UserEnabled, Users.UserLocked,
			--   Users.IDSecurityQuestion,  Users.DateMembership, Users.AccountExpireOn, 
			--   Users.Offset, Users.UserIsOnLine, Users.IDGender
			--  FROM  Users LEFT OUTER JOIN
			--		UsersCook ON Users.IDUser = UsersCook.IDUserCook
			--  WHERE Users.IDUser = @IDUser AND Users.AccountDeletedOn IS NULL

	   SELECT  Users.IDUser, Users.Name, Users.Surname, Users.UserName, Users.UserDomain, Users.UserType, Users.PasswordHash, Users.LastPasswordChange,
			  Users.PasswordExpireOn, Users.ChangePasswordNextLogon, Users.ContractSigned, Users.BirthDate, Users.eMail, Users.MailConfirmedOn, Users.Mobile,
			  Users.MobileConfirmationCode, Users.MobileConfirmedOn, Users.IDLanguage, Users.IDCity, Users.IDProfilePhoto, Users.UserEnabled, Users.UserLocked,
			  Users.MantainanceMode, Users.IDSecurityQuestion, Users.SecurityAnswer, Users.DateMembership, Users.AccountExpireOn, Users.LastLogon, Users.LastLogout, Users.Offset,
			  Users.LastProfileUpdate, Users.UserIsOnLine, Users.LastIpAddress, Users.IDVisibility, Users.IDGender, UsersCook.IsProfessionalCook, UsersCook.CookInRestaurant,
			  UsersCook.CookAtHome, UsersCook.CookDescription, UsersCook.CookMembership
			  FROM  Users LEFT OUTER JOIN
					UsersCook ON Users.IDUser = UsersCook.IDUserCook
			  WHERE Users.IDUser = @IDUser AND Users.AccountDeletedOn IS NULL

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
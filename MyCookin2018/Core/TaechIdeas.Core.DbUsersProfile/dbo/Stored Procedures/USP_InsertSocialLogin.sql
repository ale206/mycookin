﻿-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <27/10/2015>
-- Last Edit:   <>
-- Description:	<Insert User Social Logins info>
-- =============================================
CREATE PROCEDURE [dbo].[USP_InsertSocialLogin]
	@IdSocialNetwork int,
	@IdUser uniqueidentifier,
	@IdUserSocial nvarchar(250),
	@Link nvarchar(500),
	@VerifiedEmail bit,
	@PictureUrl nvarchar(500),
	@Locale nvarchar(50),
	@AccessToken nvarchar(500),
	@RefreshToken nvarchar(500)
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
		
		DECLARE @IDSocialLogin uniqueidentifier
			set @IDSocialLogin= NEWID()
			
		INSERT INTO SocialLogins
				(IDSocialLogin, IDSocialNetwork, IDUser, IDUserSocial, Link, VerifiedEmail, PictureUrl, Locale, AccessToken, RefreshToken)
		VALUES  (@IDSocialLogin, @IDSocialNetwork, @IDUser, @IDUserSocial, @Link, @VerifiedEmail, @PictureUrl, @Locale, @AccessToken, @RefreshToken);
	
		Select 1 AS NewSocialLoginCreated
   	
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

	 Select 0 AS NewSocialLoginCreated
-- =======================================    
END CATCH


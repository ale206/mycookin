-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <26/10/2015,,>
-- Last Edit:   <,,>
-- Description:	<Update Social Tokens,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UpdateSocialTokens]
	@AccessToken nvarchar(500),
	@RefreshToken nvarchar(500),
	@IDUser uniqueidentifier,
	@IDSocialNetwork int
		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================


BEGIN TRY
   IF EXISTS (SELECT IDSocialLogin FROM dbo.SocialLogins WHERE IDUser = @IDUser AND IDSocialNetwork = @IDSocialNetwork)
		BEGIN
			UPDATE SocialLogins
				SET AccessToken = @AccessToken , RefreshToken = @RefreshToken
				WHERE IDUser = @IDUser AND IDSocialNetwork = @IDSocialNetwork
	
			Select 1 AS SocialTokenUpdated	--OK
	END
	ELSE
		BEGIN
			Select 0 AS SocialTokenUpdated	--Error
		END

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
				
				
	Select 0	--Error									 
	
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH	
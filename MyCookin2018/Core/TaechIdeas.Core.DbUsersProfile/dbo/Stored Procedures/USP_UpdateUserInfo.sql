-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <08/02/2016,,>
-- Last Edit:   <,,>
-- Description:	<Update user information,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UpdateUserInfo]
	  @IDUser uniqueidentifier,
      @Name nvarchar(250),
      @Surname nvarchar(250),
      @UserName nvarchar(50),
      @BirthDate smalldatetime,
      @eMail nvarchar(150),
      @Mobile nvarchar(50),
      @IDLanguage int,
      @IDCity int,
      @IDProfilePhoto uniqueidentifier,
      @IDSecurityQuestion int,
      @SecurityAnswer nvarchar(250),
      @Offset int,
      @LastIpAddress nvarchar(50),
      @IDGender int
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IsTokenValid bit = 0

BEGIN TRY
   UPDATE [dbo].[Users]
   SET [Name] = @Name
      ,[Surname] = @Surname
      ,[UserName] = @UserName
      ,[BirthDate] = @BirthDate
      ,[eMail] = @eMail
      ,[Mobile] = @Mobile
      ,[IDLanguage] = @IDLanguage
      ,[IDCity] = @IDCity
      ,[IDProfilePhoto] = @IDProfilePhoto
      ,[IDSecurityQuestion] = @IDSecurityQuestion
      ,[SecurityAnswer] = @SecurityAnswer
      ,[Offset] = @Offset
      ,[LastProfileUpdate] = GETUTCDATE()
      ,[LastIpAddress] = @LastIpAddress
      ,[IDGender] =@IDGender
 WHERE IDUser = @IDUser
		
		SELECT 1 AS UserInfoUpdated

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
	
	 --SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin nvarchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
			
			SELECT 0 AS UserInfoUpdated												 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH


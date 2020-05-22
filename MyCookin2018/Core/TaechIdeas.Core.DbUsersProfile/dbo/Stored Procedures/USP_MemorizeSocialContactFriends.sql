

-- Batch submitted through debugger: USP_InsertUser.sql|0|0|C:\Documents and Settings\zzitibld\Documenti\SQL Server Management Studio\Projects\USP_InsertUser.sql
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <01/12/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Memorize User Information get from social Network,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_MemorizeSocialContactFriends]
	@IDSocialNetwork int,
	@IDUser uniqueidentifier,
	@FullName nvarchar(150),
	@GivenName nvarchar(150),
	@FamilyName nvarchar(150),
	@Emails nvarchar(MAX),
	@Phones nvarchar(MAX),
	@PhotoUrl nvarchar(MAX),
	@IDUserOnSocial nvarchar(150)			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IDSocialFriend uniqueidentifier = NULL;

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
   -- Check if the user is already registered in table SocialFriends
   -- If exist, get his id
   IF EXISTS (SELECT IDSocialFriend FROM dbo.SocialFriends WHERE IDUserOnSocial = @IDUserOnSocial)
		BEGIN
			SELECT @IDSocialFriend = IDSocialFriend FROM dbo.SocialFriends WHERE IDUserOnSocial = @IDUserOnSocial
			
			--UPDATE INFORMATIONS
			UPDATE dbo.SocialFriends 
			SET FullName = @FullName, GivenName = @GivenName, FamilyName = @FamilyName, Emails = @Emails,
				 Phones = @Phones, PhotoUrl = @PhotoUrl,IDUserOnSocial = @IDUserOnSocial
			WHERE IDSocialFriend = @IDSocialFriend
			
			SET @ResultExecutionCode = 'US-IN-0039' --UserFriend Updated correctly
			SET @USPReturnValue = @IDSocialFriend --RETURN GUID 
			SET @isError = 0
			
		END
   ELSE
	-- If not exist, insert in table SocialFriends and get his id		
		BEGIN
			DECLARE @guid uniqueidentifier
			SET @guid= NEWID()
			
			INSERT INTO dbo.SocialFriends
				(IDSocialFriend, IDSocialNetwork, FullName, GivenName, FamilyName, 
				Emails, Phones, PhotoUrl, IDUserOnSocial)
			VALUES
			    (@guid, @IDSocialNetwork, @FullName, @GivenName, @FamilyName, 
			    @Emails,@Phones, @PhotoUrl, @IDUserOnSocial)
			    
			SET @IDSocialFriend = @guid
			
			--  Insert in table SocialUserFriends
			DECLARE @guid2 uniqueidentifier
			SET @guid2= NEWID()
					
			INSERT INTO dbo.SocialUserFriends (IDSocialUserFriend, IDUser, IDSocialFriend)
			VALUES (@guid2, @IDUser, @IDSocialFriend)
		   
			SET @ResultExecutionCode = 'US-IN-0038' --UserFriend Added correctly
			--SET @USPReturnValue = @guid2 --RETURN GUID 
			SET @USPReturnValue = @IDSocialFriend --RETURN GUID 
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
	 DECLARE @FileOrigin varchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH


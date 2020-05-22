-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <14/02/2016,,>
-- Last Edit:   <,,>
-- Description:	<,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SocialFriends]
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
   
	
                      SELECT        SocialUserFriends.IDUser, SocialFriends.IDSocialNetwork, SocialFriends.LastTimeContacted, SocialFriends.IDSocialFriend, SocialFriends.ContactAgain,
                      SocialFriends.FullName, SocialFriends.GivenName, SocialFriends.FamilyName, SocialFriends.Emails, SocialFriends.Phones, SocialFriends.PhotoUrl,
                      SocialFriends.IDUserOnSocial
                      FROM            SocialFriends INNER JOIN
                      SocialUserFriends ON SocialFriends.IDSocialFriend = SocialUserFriends.IDSocialFriend
                      WHERE        (SocialUserFriends.IDUser = @IDUser) AND (SocialFriends.IDSocialNetwork = @IDSocialNetwork)
                    
		
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
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH	
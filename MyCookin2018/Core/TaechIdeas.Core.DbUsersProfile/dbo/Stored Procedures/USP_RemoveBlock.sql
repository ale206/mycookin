
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <30/08/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Remove Friendship>
-- =============================================
CREATE PROCEDURE [dbo].[USP_RemoveBlock]
	@IDUser1 uniqueidentifier,
	@IDUser2 uniqueidentifier		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
   --CHECK IF THE USER ARE FRIENDS
	IF EXISTS (SELECT IDUserFriend FROM dbo.UsersFriends WHERE IDUser1 = @IDUser1 AND IDUser2 = @IDUser2 AND FriendshipCompletedDate IS NOT NULL)
		BEGIN
		
			--REMOVE BLOCK WITH UPDATE
			UPDATE dbo.UsersFriends SET UserBlocked = NULL WHERE IDUser1 = @IDUser1 AND IDUser2 = @IDUser2
			
		END	
	ELSE
		BEGIN
			
			--REMOVE BLOCK WITH DELETE
			DELETE FROM dbo.UsersFriends WHERE IDUser1 = @IDUser1 AND IDUser2 = @IDUser2
		
		END	
		
		SET @ResultExecutionCode = 'US-IN-0032' 
		SET @USPReturnValue = @IDUser2 --BLOCK REMOVED CORRECTLY, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
		SET @isError = 0

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


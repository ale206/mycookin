
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <30/08/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Remove Friendship>
-- =============================================
CREATE PROCEDURE [dbo].[USP_RemoveFriendship]
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
   
   --CHECK EXISTENCE USER1, USER2
	IF EXISTS (SELECT IDUserFriend FROM dbo.UsersFriends WHERE IDUser1 = @IDUser1 AND IDUser2 = @IDUser2)
		BEGIN
			
			--DELETE
			DELETE FROM dbo.UsersFriends WHERE IDUser1 = @IDUser1 AND IDUser2 = @IDUser2
			DELETE FROM dbo.UsersFriends WHERE IDUser2 = @IDUser1 AND IDUser1 = @IDUser2
				
					SET @ResultExecutionCode = 'US-IN-0014' 
					SET @USPReturnValue = @IDUser2 --FRIENDHSIP REMOVED CORRECTLY, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
					SET @isError = 0
		END	
	ELSE
		BEGIN	--WE SHOULD NEVER GO HERE (USER 2 IS NOT FRIEND OF USER 1)
			SET @ResultExecutionCode = 'US-ER-0009' --THE FRIENDSHIP DOES NOT EXIST
			SET @isError = 1 --SHOULD BE A WARNING, MORE THAN AN ERROR
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


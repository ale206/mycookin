
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <29/08/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Request or Accept Friendhsip. Friendship is done with two couples given by USER1-USER2 and USER2-USER1,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_RequestOrAcceptFriendship]
	@IDUser1 uniqueidentifier,
	@IDUser2 uniqueidentifier,
	@FriendshipDate smalldatetime		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @FriendshipExistence nvarchar(MAX);

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
   --CREATE FRIENDSHIP ADDING A ROW WITH USER1, USER2, DATE, BLOCKED false
	IF NOT EXISTS (SELECT IDUserFriend FROM dbo.UsersFriends WHERE IDUser1 = @IDUser1 AND IDUser2 = @IDUser2)
		BEGIN
			
			DECLARE @guid uniqueidentifier
			set @guid= NEWID()
			
			INSERT INTO dbo.UsersFriends (IDUserFriend, IDUser1, IDUser2) 
						    VALUES (@guid, @IDUser1, @IDUser2)
			
			--CHECK IF IS A FRIENDSHIP COMPLETE OR IS A NEW REQUEST
			IF EXISTS (SELECT IDUserFriend FROM dbo.UsersFriends WHERE IDUser2 = @IDUser1 AND IDUser1 = @IDUser2)
				BEGIN
					--NEW FRIENDSHIP!
					--INSERT COMPLETED FRIENDSHIP DATE IN ALL TWO USERS AND REMOVE BLOCK IF EXISTS
					UPDATE dbo.UsersFriends SET FriendshipCompletedDate = @FriendshipDate, UserBlocked = NULL
						WHERE (IDUser1 = @IDUser1 AND IDUser2 = @IDUser2) OR (IDUser1 = @IDUser2 AND IDUser2 = @IDUser1)
					
					SET @ResultExecutionCode = 'US-IN-0018' 
					SET @USPReturnValue = @IDUser2 --NEW FRIENDSHIP COMPLETED, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
					SET @isError = 0

				END
			ELSE
				BEGIN
					--REQUEST DONE CORRECTLY
					--INSERT REQUEST FRIENDSHIP DATE IN USER THAT START THE REQUEST
					UPDATE dbo.UsersFriends SET FriendshipRequestedDate = @FriendshipDate
						WHERE IDUser1 = @IDUser1 AND IDUser2 = @IDUser2
					
					SET @ResultExecutionCode = 'US-IN-0019' 
					SET @USPReturnValue = @IDUser2 --REQUEST DONE, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
					SET @isError = 0
				
				END
		END	
	ELSE
		BEGIN	--WE SHOULD NEVER GO HERE (USER IS TRYING AGAIN TO REQUEST FRIENDHSIP)
			SET @ResultExecutionCode = 'US-ER-0008' --THE FRIENDSHIP ALREADY EXIST
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


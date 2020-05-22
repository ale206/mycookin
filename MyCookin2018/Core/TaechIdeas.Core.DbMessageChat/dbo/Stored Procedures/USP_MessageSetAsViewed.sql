
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <19/05/2013,,>
-- Last Edit:   <24/08/2013>
-- Description:	<Set Message As Viewed,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_MessageSetAsViewed]
	@IDUserConversationOwner uniqueidentifier
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
	BEGIN TRANSACTION    -- Start the transaction
		
	UPDATE MessagesRecipients 
	SET ViewedOn = GETUTCDATE()
		WHERE IDMessageRecipient IN 
		(
			SELECT  MR.IDMessageRecipient
					--,M.IDMessage, M.[Message], MR.IDUserSender, MR.ViewedOn, MR.IDMessageRecipient
			FROM	Messages M 
				INNER JOIN MessagesRecipients MR
					ON MR.IDMessage = M.IDMessage
				INNER JOIN UsersConversations U
					ON MR.IDUserConversation = U.IDUserConversation
			WHERE	
					(MR.IDUserSender = @IDUserConversationOwner OR MR.IDUserRecipient = @IDUserConversationOwner)
				AND MR.DeletedOn IS NULL
				AND MR.ViewedOn IS NULL
				AND U.IDUser = @IDUserConversationOwner			--i messaggi sono i miei
				AND U.ArchivedOn IS NULL
	)	

	
	SET @ResultExecutionCode = ' '					-- Set Message As Viewed
	SET @USPReturnValue = @IDUserConversationOwner	-- IDUserConversationOwner
	SET @isError = 0

		-- If we reach here, success!
   COMMIT
   
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
		-- =======================================
		
END TRY

BEGIN CATCH

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


-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <19/05/2013,,>
-- Description:	<Insert new Conversation>
-- =============================================
CREATE PROCEDURE [dbo].[USP_InsertConversation]	
	@IDUser uniqueidentifier,
	@IDConversation uniqueidentifier,
	@CreatedOn datetime
		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IDUserConversation uniqueidentifier;

BEGIN TRY
	BEGIN TRANSACTION    -- Start the transaction

		SET @IDUserConversation = NEWID()

		INSERT  INTO UsersConversations (IDUserConversation, IDConversation, IDUser, CreatedOn, ArchivedOn)
				VALUES (@IDUserConversation, @IDConversation, @IDUser, @CreatedOn, null)
		
		--Return Values
		SET @ResultExecutionCode = ''					-- NEW Conversation INSERTED
		SET @USPReturnValue = @IDUserConversation		--ID OF THE CONVERSATION
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


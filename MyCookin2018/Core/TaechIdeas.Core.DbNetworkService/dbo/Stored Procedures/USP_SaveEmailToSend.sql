
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <20/05/2016,,>
-- Description:	<Save Email To Send,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SaveEmailToSend]
	@From nvarchar(max),
	@To nvarchar(max),
	@Cc nvarchar(max),
	@Bcc nvarchar(max),
	@Subject nvarchar(max),
	@Message nvarchar(max),
	@HtmlFilePath nvarchar(max)     
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @emailId uniqueidentifier
	set @emailId= NEWID()

	--INSERT EMAIL
	INSERT INTO [dbo].[Emails]
		   ([Id],[From],[To],[Cc],[Bcc],[Subject],[Message],[HtmlFilePath],[CreatedAt],[UpdatedAt],[DeletedAt],[Deleted],[Enabled])
	 VALUES
		   (@emailId,@From,@To,@Cc,@Bcc,@Subject,@Message,@HtmlFilePath,GETUTCDATE(),null,null,0,1)
				
	--INSERT STATUS
	DECLARE @guid uniqueidentifier
	set @guid= NEWID()

	INSERT INTO [dbo].[StatusRecords]
		   ([Id],[EmailStatus],[CreatedAt],[UpdatedAt],[DeletedAt],[Deleted],[Enabled],[Email_Id])
	 VALUES
		   (@guid,1,GETUTCDATE(),null,null,0,1,@emailId)
			
	-- If we reach here, success!
   COMMIT
   
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		SELECT 1 AS EmailSaved, @emailId AS EmailId
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
		 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin nvarchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)
	 
	 SELECT 0 AS EmailSaved
-- =======================================  
END CATCH
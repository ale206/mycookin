


-- ================================================
-- =============================================
-- Author:		<Saverio Cammarata>
-- Create date: <28/08/2012,,>
-- Description:	<,,AGGIUNGE AuditEvent>
-- =============================================
CREATE PROCEDURE [dbo].[USP_AddAuditEvent]
	@AuditEventMessage nvarchar(max),
	@ObjectID uniqueidentifier,
	@ObjectType nvarchar(50),
	@ObjectTxtInfo nvarchar(max),
	@AuditEventLevel int,
	@EventInsertedOn smalldatetime,
	@EventUpdatedOn smalldatetime,
	@IDEventUpdatedBy uniqueidentifier,
	@AuditEventIsOpen bit
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

DECLARE @IDAuditEvent uniqueidentifier

BEGIN TRY
	BEGIN TRANSACTION    -- Start the transaction
			
	SET @IDAuditEvent = NEWID();
		
	INSERT INTO [DBAudit].[dbo].[AuditEvent]
           ([IDAuditEvent]
           ,[AuditEventMessage]
           ,[ObjectID]
           ,[ObjectType]
           ,[ObjectTxtInfo]
           ,[AuditEventLevel]
           ,[EventInsertedOn]
           ,[EventUpdatedOn]
           ,[IDEventUpdatedBy]
           ,[AuditEventIsOpen])
     VALUES
           (@IDAuditEvent, 
           @AuditEventMessage,
           @ObjectID,
           @ObjectType,
           @ObjectTxtInfo, 
           @AuditEventLevel,
           @EventInsertedOn,
           @EventUpdatedOn, 
           @IDEventUpdatedBy,
           @AuditEventIsOpen)
			
			SET @ResultExecutionCode = 'AU-IN-0001' --Inserted
			SET @USPReturnValue = @IDAuditEvent -- The ID of the new AuditEvent inserted
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
	 
	 DECLARE @ErrorNumber varchar(MAX)		= ERROR_NUMBER();
	 DECLARE @ErrorSeverity varchar(MAX)	= ERROR_SEVERITY();
	 DECLARE @ErrorState varchar(MAX)		= ERROR_STATE();
	 DECLARE @ErrorProcedure varchar(MAX)	= ERROR_PROCEDURE();
	 DECLARE @ErrorLine varchar(MAX)		= ERROR_LINE();
	 DECLARE @ErrorMessage varchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime		= GETUTCDATE();
	 DECLARE @ErrorMessageCode varchar(MAX)	= @ResultExecutionCode;

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


-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <27/06/2012,,>
-- Description:	<Insert error logs that receives from stored procedures. It's called from EVERY STORED PROCEDURE.>
-- =============================================
CREATE PROCEDURE [dbo].[USP_InsertErrorLog]
	 
	@ErrorNumber nvarchar(MAX),
	@ErrorSeverity nvarchar(MAX),
	@ErrorState nvarchar(MAX),
	@ErrorProcedure nvarchar(MAX),
	@ErrorLine nvarchar(MAX),
	@ErrorMessage nvarchar(MAX),
	@FileOrigin nvarchar(MAX),
	@DateError smalldatetime,
	@ErrorMessageCode nvarchar(MAX),
	@isStoredProcedureError bit,
	@isTriggerError bit,
	@IDUser nvarchar(MAX),
	@isApplicationError bit,
	@isApplicationLog bit
		
AS

DECLARE @uid uniqueidentifier;

BEGIN
	set @uid= NEWID()
	
	INSERT INTO dbo.ErrorsLogs (IDErrorLog, ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, 
									  ErrorLine, ErrorMessage, FileOrigin, DateError, 
									  ErrorMessageCode,isStoredProcedureError,isTriggerError,IDUser, isApplicationError,
									  isApplicationLog) 
					   VALUES (@uid, @ErrorNumber, @ErrorSeverity, @ErrorState, @ErrorProcedure, 
									  @ErrorLine, @ErrorMessage, @FileOrigin, @DateError,
									  @ErrorMessageCode,@isStoredProcedureError,@isTriggerError, @IDUser, @isApplicationError,
									  @isApplicationLog)
END

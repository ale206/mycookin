-- Batch submitted through debugger: USP_FindUser.sql|0|0|C:\Documents and Settings\zzitibld\Documenti\SQL Server Management Studio\Projects\USP_ActivateUser.sql
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <24/10/2012,,>
-- Last Edit:   <25/08/2013,,>
-- Description:	<Find User,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_FindUser]
	@Words nvarchar(MAX),
	@NumberOfResults int		
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
	BEGIN TRANSACTION    -- Start the transaction
   
	SELECT TOP (@NumberOfResults) IDUser, Name, Surname 
		FROM dbo.Users
		WHERE ((Name + ' ' + Surname LIKE +@Words+ '%') OR 
		(Surname + ' ' + Name LIKE +@Words+'%') OR
				(UserName LIKE +@Words+'%') OR
				(Name LIKE +@Words+'%') OR
				(Surname LIKE +@Words+'%') OR
				(eMail = @Words))
				AND AccountDeletedOn IS NULL
				AND UserEnabled = 1
	
	SET @ResultExecutionCode = 'Search Completed' 
	SET @USPReturnValue = @Words 
	SET @isError = 0

	-- Write qry to memorize words searched ;)
		
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


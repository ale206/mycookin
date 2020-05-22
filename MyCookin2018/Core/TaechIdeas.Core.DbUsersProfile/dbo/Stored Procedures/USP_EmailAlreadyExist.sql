-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <17/10/2015,,>
-- Last Edit:   <,,>
-- Description:	<Check if an Email already exists in db,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_EmailAlreadyExist]
	@Email nvarchar(max)
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @EmailExists bit = 0

BEGIN TRY
   BEGIN TRANSACTION
	
	IF EXISTS (SELECT Email FROM dbo.Users WHERE Email = @Email)
		BEGIN		
			set @EmailExists = 1
		END	
			
			--SET @ResultExecutionCode = 'US-IN-0001' --USER ADDED CORRECTLY
			--SET @USPReturnValue = @guid --USER ADDED CORRECTLY, RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
			--SET @isError = 0
			-- If we reach here, success!
   COMMIT
   
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		SELECT @EmailExists AS EmailExists
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


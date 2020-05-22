
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <25/10/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Defollow User,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_DefollowUser]
	@IDUser uniqueidentifier,
	@IDUserFollowed uniqueidentifier
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
	
	IF NOT EXISTS (SELECT IDUserFollower FROM dbo.UsersFollowers WHERE IDUser = @IDUser AND IDUserFollowed = @IDUserFollowed)
		BEGIN	--WE SHOULD NEVER GO HERE
			SET @ResultExecutionCode = 'US-ER-0010' --USER1 DOES NOT FOLLOW USER2!
			SET @isError = 1
		END
	ELSE
		BEGIN
					
			DELETE FROM dbo.UsersFollowers WHERE IDUser = @IDUser AND IDUserFollowed = @IDUserFollowed
			
			SET @ResultExecutionCode = 'US-IN-0035' --FOLLOWING DELETED CORRECTLY
			SET @USPReturnValue = @IDUser -- RETURN GUID --(NOTE: ALWAYS RETURN IDUSER FOR USERS SP!)
			SET @isError = 0
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
	 DECLARE @FileOrigin varchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH


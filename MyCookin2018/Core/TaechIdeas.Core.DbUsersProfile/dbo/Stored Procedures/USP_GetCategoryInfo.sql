
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/07/2012,,>
-- Last Edit:   <03/02/2013,,>
-- Description:	<Get All Category Info. Returns 3 values separated by || ,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetCategoryInfo]
	@IDLanguage int,
	@IDUserInfoPropertyCategory int
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IDUserInfoPropertyCategoryLanguage nvarchar(MAX);
DECLARE @UserInfoPropertyCategory nvarchar(MAX);
DECLARE @UserInfoPropertyCategoryToolTip nvarchar(MAX);

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
   SELECT TOP 1	@IDUserInfoPropertyCategoryLanguage = L.IDUserInfoPropertyCategoryLanguage,
				@UserInfoPropertyCategory = L.UserInfoPropertyCategory,
				@UserInfoPropertyCategoryToolTip = L.UserInfoPropertyCategoryToolTip 
				FROM UsersInfoPropertiesCategoriesLanguages L, UsersInfoPropertiesCategories C 
				WHERE L.IDUserInfoPropertyCategory = C.IDUserInfoPropertyCategory AND C.Enabled = 1
						AND L.IDLanguage = @IDLanguage AND L.IDUserInfoPropertyCategory = @IDUserInfoPropertyCategory

	IF (@IDUserInfoPropertyCategoryLanguage IS NULL)	--IF NULL, GET THE DEFAULT INFO WITH DEFAULT LANGUAGE (ENGLISH)
		BEGIN
			SELECT TOP 1	@IDUserInfoPropertyCategoryLanguage = L.IDUserInfoPropertyCategoryLanguage,
							@UserInfoPropertyCategory = L.UserInfoPropertyCategory,
							@UserInfoPropertyCategoryToolTip = L.UserInfoPropertyCategoryToolTip 
							FROM UsersInfoPropertiesCategoriesLanguages L, UsersInfoPropertiesCategories C 
							WHERE L.IDUserInfoPropertyCategory = C.IDUserInfoPropertyCategory AND C.Enabled = 1
									AND L.IDLanguage = 1 AND L.IDUserInfoPropertyCategory = @IDUserInfoPropertyCategory

			IF (@IDUserInfoPropertyCategoryLanguage IS NULL)	--IF NULL AGAIN, NOT EXISTS THAT CATEGORY
				BEGIN
					SET @USPReturnValue = 'Category Not Found! Check the ErrorCode sent to USP_GetCategoryInfo'
					SET @isError = 1					
				END
			ELSE
				BEGIN
					-- IF A RECORD IS NULL, RETURN NULL IF YOU USE CONCATENATION (+ '|' + ...)
					IF (@UserInfoPropertyCategoryToolTip IS NULL)
					BEGIN
						SET @UserInfoPropertyCategoryToolTip = ''
					END
					
					SET @USPReturnValue = @IDUserInfoPropertyCategoryLanguage + '|' + @UserInfoPropertyCategory + '|' + @UserInfoPropertyCategoryToolTip
					SET @isError = 0
				END			
		END
	ELSE	--CATEGORY FOUND CORRECTLY
		BEGIN
			
			-- IF A RECORD IS NULL, RETURN NULL IF YOU USE CONCATENATION (+ '|' + ...)
			IF (@UserInfoPropertyCategoryToolTip IS NULL)
			BEGIN
				SET @UserInfoPropertyCategoryToolTip = ''
			END

			SET @USPReturnValue = @IDUserInfoPropertyCategoryLanguage + '|' + @UserInfoPropertyCategory + '|' + @UserInfoPropertyCategoryToolTip
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
	 DECLARE @ErrorOrLogMessage nvarchar(MAX)	= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime			= GETUTCDATE();
	 DECLARE @ErrorMessageCode nvarchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorOrLogMessage
	
	 SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
	 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin nvarchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorOrLogMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)
	 
-- =======================================  
END CATCH
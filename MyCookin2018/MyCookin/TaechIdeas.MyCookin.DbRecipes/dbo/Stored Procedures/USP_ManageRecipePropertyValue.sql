





-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,27/04/2013,>
-- Description:	<Description,Manage Recipe Property Value,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageRecipePropertyValue]
	@IDRecipePropertyValue uniqueidentifier,
	@IDRecipe uniqueidentifier,
	@IDRecipeProperty int,
	@Value bit,
	@isDeleteOperation bit
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
	IF @isDeleteOperation = 1
		BEGIN
			DELETE FROM  dbo.RecipesPropertiesValues
			WHERE IDRecipePropertyValue = @IDRecipePropertyValue
		END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT IDRecipePropertyValue FROM RecipesPropertiesValues WHERE IDRecipePropertyValue = @IDRecipePropertyValue)
			BEGIN
				INSERT INTO [dbo].RecipesPropertiesValues 
					(IDRecipePropertyValue
					   ,IDRecipe
					   ,IDRecipeProperty
					   ,Value) 
				
				VALUES (@IDRecipePropertyValue
					   ,@IDRecipe
					   ,@IDRecipeProperty
					   ,@Value)
			END
		ELSE
			BEGIN
				UPDATE [DBRecipes].dbo.RecipesPropertiesValues
				   SET	IDRecipe=@IDRecipe,
						IDRecipeProperty=@IDRecipeProperty,
						Value=@Value
				 
				 WHERE IDRecipePropertyValue=@IDRecipePropertyValue
			END
	END
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='';
		SET @USPReturnValue =@IDRecipePropertyValue;
					                            
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

	 SET @ResultExecutionCode = 'RC-ER-0004' --Error in the StoredProcedure
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








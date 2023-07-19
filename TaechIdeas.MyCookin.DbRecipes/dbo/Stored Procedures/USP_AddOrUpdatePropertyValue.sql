-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <09/02/2016,>
-- Description:	<Add or Update Recipe Property Value>
-- =============================================
CREATE PROCEDURE [dbo].[USP_AddOrUpdatePropertyValue]
	@IDRecipe uniqueidentifier,
	@IDRecipeProperty int	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

DECLARE @IDRecipePropertyValue uniqueidentifier

BEGIN TRY
	
	IF NOT EXISTS (SELECT IDRecipePropertyValue FROM RecipesPropertiesValues WHERE IDRecipe = @IDRecipe AND IDRecipeProperty = @IDRecipeProperty)
		BEGIN

			SET @IDRecipePropertyValue = NEWID()

			INSERT INTO RecipesPropertiesValues 
				(IDRecipePropertyValue
					,IDRecipe
					,IDRecipeProperty
					,Value) 
				
			VALUES (@IDRecipePropertyValue
					,@IDRecipe
					,@IDRecipeProperty
					,1)
		END
	ELSE
		BEGIN

		SELECT @IDRecipePropertyValue = IDRecipePropertyValue FROM RecipesPropertiesValues WHERE  IDRecipe = @IDRecipe AND IDRecipeProperty = @IDRecipeProperty

			UPDATE RecipesPropertiesValues
			SET	IDRecipe = @IDRecipe,
				IDRecipeProperty = @IDRecipeProperty,
				Value = 1
			WHERE IDRecipePropertyValue = @IDRecipePropertyValue

		END
						                            
		
	SELECT @IDRecipePropertyValue AS RecipePropertyValueId
		
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
	
	 --SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin varchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
					
	 SELECT @IDRecipePropertyValue AS RecipePropertyValueId
	 									 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH








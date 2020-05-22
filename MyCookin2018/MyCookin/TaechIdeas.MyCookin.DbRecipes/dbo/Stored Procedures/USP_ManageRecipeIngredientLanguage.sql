




-- ================================================
-- =============================================
-- Author:		<Saverio Cammarata>
-- Create date: <10/03/2013,,>
-- Description:	<[USP_ManageRecipeIngredientLanguage],,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageRecipeIngredientLanguage]
	
	 @IDRecipeIngredientLanguage uniqueidentifier,
     @IDRecipeIngredient uniqueidentifier,
     @IDLanguage int,
     @RecipeIngredientNote nvarchar(250),
	 @RecipeIngredientGroupName nvarchar(150),
	 @isAutoTranslate bit,
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
			DELETE FROM  dbo.RecipesIngredientsLanguages
			WHERE IDRecipeIngredientLanguage = @IDRecipeIngredientLanguage
		END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT IDRecipeIngredientLanguage FROM RecipesIngredientsLanguages WHERE IDRecipeIngredient = @IDRecipeIngredient AND IDLanguage = @IDLanguage)
			BEGIN
				INSERT INTO [dbo].RecipesIngredientsLanguages 
					([IDRecipeIngredientLanguage],
						[IDRecipeIngredient],
						[IDLanguage],
						[RecipeIngredientNote],
						[RecipeIngredientGroupName],
						[isAutoTranslate]
					)
				
				VALUES (@IDRecipeIngredientLanguage,
						 @IDRecipeIngredient,
						 @IDLanguage,
						 @RecipeIngredientNote,
						 @RecipeIngredientGroupName,
						 @isAutoTranslate)
			END
		ELSE
			BEGIN
				UPDATE [dbo].RecipesIngredientsLanguages
				   SET [IDRecipeIngredient] = @IDRecipeIngredient,
					   [IDLanguage] = @IDLanguage,
					   [RecipeIngredientNote] = @RecipeIngredientNote,
					   [RecipeIngredientGroupName] = @RecipeIngredientGroupName,
					   [isAutoTranslate] = @isAutoTranslate
				 
				 WHERE IDRecipeIngredient = @IDRecipeIngredient AND IDLanguage = @IDLanguage
			END
	END
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='';
		SET @USPReturnValue =@IDRecipeIngredient;
					                            
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







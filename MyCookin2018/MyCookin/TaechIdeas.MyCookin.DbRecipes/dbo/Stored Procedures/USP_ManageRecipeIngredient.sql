



-- ================================================
-- =============================================
-- Author:		<Saverio Cammarata>
-- Create date: <17/02/2013,,>
-- Description:	<[USP_ManageRecipeIngredient],,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageRecipeIngredient]
	
	 @IDRecipeIngredient uniqueidentifier,
     @IDRecipe uniqueidentifier,
     @IDIngredient uniqueidentifier,
     @isPrincipalIngredient bit,
     @QuantityNotStd nvarchar(150),
     @IDQuantityNotStd int,
     @Quantity float,
     @IDQuantityType int,
     @QuantityNotSpecified bit,
     @RecipeIngredientGroupNumber tinyint,
     @IDRecipeIngredientAlternative uniqueidentifier,
	 @IngredientRelevance int,
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
			DELETE FROM  dbo.RecipesIngredients
			WHERE IDRecipeIngredient = @IDRecipeIngredient
			
			DELETE FROM  dbo.RecipesIngredients
			WHERE IDRecipeIngredientAlternative = @IDRecipeIngredient
			
		END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT IDRecipeIngredient FROM RecipesIngredients WHERE IDRecipe = @IDRecipe AND IDIngredient = @IDIngredient AND RecipeIngredientGroupNumber = @RecipeIngredientGroupNumber)
			BEGIN
				INSERT INTO [dbo].RecipesIngredients 
					([IDRecipeIngredient]
					   ,[IDRecipe]
					   ,[IDIngredient]
					   ,[IsPrincipalIngredient]
					   ,[QuantityNotStd]
					   ,[IDQuantityNotStd]
					   ,[Quantity]
					   ,[IDQuantityType]
					   ,[QuantityNotSpecified]
					   ,[RecipeIngredientGroupNumber]
					   ,[IDRecipeIngredientAlternative]
					   ,[IngredientRelevance]) 
				
				VALUES (@IDRecipeIngredient,
						 @IDRecipe,
						 @IDIngredient,
						 @isPrincipalIngredient,
						 @QuantityNotStd,
						 @IDQuantityNotStd,
						 @Quantity,
						 @IDQuantityType,
						 @QuantityNotSpecified,
						 @RecipeIngredientGroupNumber,
						 @IDRecipeIngredientAlternative,
						 @IngredientRelevance)
			END
		ELSE
			BEGIN
				
				SELECT @IDRecipeIngredient = IDRecipeIngredient FROM RecipesIngredients WHERE IDRecipe = @IDRecipe AND IDIngredient = @IDIngredient AND RecipeIngredientGroupNumber = @RecipeIngredientGroupNumber
				
				UPDATE [DBRecipes].[dbo].[RecipesIngredients]
				   SET [IDRecipe] = @IDRecipe,
					   [IDIngredient] = @IDIngredient,
					   [IsPrincipalIngredient] = @IsPrincipalIngredient,
					   [QuantityNotStd] = @QuantityNotStd,
					   [IDQuantityNotStd] = @IDQuantityNotStd,
					   [Quantity] = @Quantity,
					   [IDQuantityType] = @IDQuantityType,
					   [QuantityNotSpecified] = @QuantityNotSpecified,
					   [RecipeIngredientGroupNumber] = @RecipeIngredientGroupNumber,
					   [IDRecipeIngredientAlternative] = @IDRecipeIngredientAlternative,
					   [IngredientRelevance] = @IngredientRelevance
				 
				 WHERE IDRecipe = @IDRecipe AND IDIngredient = @IDIngredient AND RecipeIngredientGroupNumber = @RecipeIngredientGroupNumber
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






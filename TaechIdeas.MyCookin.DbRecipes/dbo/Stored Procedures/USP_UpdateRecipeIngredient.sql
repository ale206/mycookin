﻿-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <26/01/2016,,>
-- Last Edit:   <,,>
-- Description:	<Update RecipeLanguage,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UpdateRecipeIngredient]
	@IDRecipeIngredient uniqueidentifier,
	@IDRecipe uniqueidentifier,
      @IDIngredient uniqueidentifier,
      @IsPrincipalIngredient bit,
      @QuantityNotStd  nvarchar(150),
      @IDQuantityNotStd  int,
      @Quantity  float,
      @IDQuantityType  int,
      @QuantityNotSpecified bit,
      @RecipeIngredientGroupNumber  tinyint,
      @IDRecipeIngredientAlternative uniqueidentifier,
      @IngredientRelevance int
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
				

		UPDATE dbo.RecipesIngredients
   SET IDRecipe = @IDRecipe,IDIngredient = @IDIngredient,IsPrincipalIngredient = @IsPrincipalIngredient,QuantityNotStd = @QuantityNotStd,
      IDQuantityNotStd = @IDQuantityNotStd,
      Quantity = @Quantity, 
      IDQuantityType = @IDQuantityType,
      QuantityNotSpecified = @QuantityNotSpecified,
      RecipeIngredientGroupNumber = @RecipeIngredientGroupNumber, 
      IDRecipeIngredientAlternative = @IDRecipeIngredientAlternative,
      IngredientRelevance = @IngredientRelevance
 WHERE IDRecipeIngredient = @IDRecipeIngredient
	
	-- If we reach here, success!
	COMMIT

	SELECT 1 AS RecipeIngredientUpdated

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
	
	 --SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin nvarchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
					
	SELECT 0 AS RecipeIngredientUpdated													 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH
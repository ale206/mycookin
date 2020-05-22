-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <18/02/2016>
-- Description:	<[>
-- =============================================
CREATE PROCEDURE [dbo].[USP_AddNewIngredientTranslation]
	
	@IngredientSingular nvarchar(250),
	@IngredientPlural nvarchar(250),
	@IngredientDescription nvarchar(500),
	@isAutoTranslate bit,
	@IDIngredient uniqueidentifier,
	@IDLanguage int
	 
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
		DECLARE @NewIngredientLanguageGuid uniqueidentifier

		SET @NewIngredientLanguageGuid = NEWID()

   --insert
   IF NOT EXISTS (SELECT IDIngredientLanguage FROM dbo.IngredientsLanguages WHERE IDIngredient=@IDIngredient AND IDLanguage = @IDLanguage)
   BEGIN 

			INSERT INTO dbo.IngredientsLanguages (
			IDIngredientLanguage,
			IDIngredient,IDLanguage,IngredientSingular,IngredientPlural,IngredientDescription,isAutoTranslate)
						VALUES (
						@NewIngredientLanguageGuid,
			@IDIngredient,@IDLanguage, @IngredientSingular,@IngredientPlural,@IngredientDescription,@isAutoTranslate)
			

	
   END

   COMMIT
		
-- Create FriendlyId calling SP
	DECLARE @FriendlyId nvarchar(500)

	--Use of a Temp Table to avoid return of this Called-Stored Procedure in this one
	CREATE TABLE #Temp1 ( FriendlyId nvarchar( 500 ) )

	INSERT INTO #Temp1 EXECUTE @FriendlyId = USP_GenerateIngredientFriendlyId @NewIngredientLanguageGuid
	
	--Return objects
	--SELECT @NewRecipeGuid AS RecipeId, @NewRecipeLanguageGuid AS RecipeLanguageId, ( SELECT FriendlyId FROM #Temp1 ) AS FriendlyId
		

				
	SELECT 1 AS NewTranslationAdded

		
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

SELECT 0 AS NewTranslationAdded

END CATCH




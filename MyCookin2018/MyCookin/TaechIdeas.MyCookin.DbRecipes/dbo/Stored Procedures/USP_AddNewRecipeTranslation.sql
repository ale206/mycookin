-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <18/02/2016>
-- Description:	<>
-- =============================================
CREATE PROCEDURE [dbo].[USP_AddNewRecipeTranslation]
	@IDRecipe uniqueidentifier,
	@IDLanguage int,
	@RecipeName nvarchar(250),
	@RecipeLanguageAutoTranslate bit,
	@RecipeHistory nvarchar(max),
	@RecipeNote nvarchar(max),
	@RecipeSuggestion nvarchar(max),
	@RecipeLanguageTags nvarchar(max),
	@TranslatedBy uniqueidentifier
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
   DECLARE @NewRecipeLanguageGuid uniqueidentifier

   SET @NewRecipeLanguageGuid = NEWID()
	
		IF NOT EXISTS (SELECT IDRecipe FROM RecipesLanguages WHERE IDRecipe = @IDRecipe AND IDLanguage = @IDLanguage)
			BEGIN
				INSERT INTO dbo.RecipesLanguages
						(IDRecipeLanguage 
						,IDRecipe
						,IDLanguage 
						,RecipeName 
						,RecipeLanguageAutoTranslate 
						,RecipeHistory 
						,RecipeNote 
						,RecipeSuggestion 
						,RecipeDisabled 
						,GeoIDRegion 
						,RecipeLanguageTags
						,OriginalVersion
						,TranslatedBy)
				VALUES
					(@NewRecipeLanguageGuid
					,@IDRecipe 
					,@IDLanguage 
					,dbo.udfReplaceSpecialChar(@RecipeName,' ')
					,@RecipeLanguageAutoTranslate 
					,@RecipeHistory 
					,@RecipeNote 
					,@RecipeSuggestion 
					,0 
					,1 
					,dbo.udfReplaceSpecialChar(@RecipeLanguageTags,' ')
					,0
					,@TranslatedBy)
			END
							                            
   COMMIT

   -- Create FriendlyId calling SP
	DECLARE @FriendlyId nvarchar(500)

	--Use of a Temp Table to avoid return of this Called-Stored Procedure in this one
	CREATE TABLE #Temp1 ( FriendlyId nvarchar( 500 ) )

	INSERT INTO #Temp1 EXECUTE @FriendlyId = USP_GenerateFriendlyId @NewRecipeLanguageGuid
	
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

	 SET @ResultExecutionCode = 'RC-ER-0007' --Error in the StoredProcedure
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
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================   

SELECT 0 AS NewTranslationAdded

 
END CATCH











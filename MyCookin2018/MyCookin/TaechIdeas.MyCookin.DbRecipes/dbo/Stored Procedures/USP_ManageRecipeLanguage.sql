-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,19/10/2013,>
-- Description:	<Description,Manage Recipe language,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageRecipeLanguage]
	@IDRecipe uniqueidentifier,
	@IDRecipeLanguage uniqueidentifier,
	@IDLanguage int,
	@RecipeName nvarchar(250),
	@RecipeLanguageAutoTranslate bit,
	@RecipeHistory nvarchar(max),
	@RecipeHistoryDate datetime,
	@RecipeNote nvarchar(max),
	@RecipeSuggestion nvarchar(max),
	@RecipeDisabled bit,
	@GeoIDRegion int,
	@RecipeLanguageTags nvarchar(max),
	@OriginalVersion bit,
	@TranslatedBy uniqueidentifier,
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
			SET @isError = 0
			SET @ResultExecutionCode ='RC-IN-0003'
			SET @USPReturnValue = @IDRecipe
		END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT IDRecipe FROM RecipesLanguages WHERE IDRecipe = @IDRecipe AND IDLanguage = @IDLanguage)
			BEGIN
				INSERT INTO dbo.RecipesLanguages
						(IDRecipeLanguage 
						,IDRecipe
						,IDLanguage 
						,RecipeName 
						,RecipeLanguageAutoTranslate 
						,RecipeHistory 
						,RecipeHistoryDate 
						,RecipeNote 
						,RecipeSuggestion 
						,RecipeDisabled 
						,GeoIDRegion 
						,RecipeLanguageTags
						,OriginalVersion
						,TranslatedBy)
				VALUES
					(@IDRecipeLanguage
					,@IDRecipe 
					,@IDLanguage 
					,dbo.udfReplaceSpecialChar(@RecipeName,' ')
					,@RecipeLanguageAutoTranslate 
					,@RecipeHistory 
					,@RecipeHistoryDate 
					,@RecipeNote 
					,@RecipeSuggestion 
					,@RecipeDisabled 
					,@GeoIDRegion 
					,dbo.udfReplaceSpecialChar(@RecipeLanguageTags,' ')
					,@OriginalVersion
					,@TranslatedBy)
			END
		ELSE
			BEGIN
				UPDATE dbo.RecipesLanguages
				SET 	RecipeName =dbo.udfReplaceSpecialChar(@RecipeName,' ') ,
						RecipeLanguageAutoTranslate =@RecipeLanguageAutoTranslate ,
						RecipeHistory =@RecipeHistory ,
						RecipeHistoryDate =@RecipeHistoryDate ,
						RecipeNote =@RecipeNote ,
						RecipeSuggestion =@RecipeSuggestion ,
						RecipeDisabled =@RecipeDisabled ,
						GeoIDRegion =@GeoIDRegion ,
						RecipeLanguageTags =dbo.udfReplaceSpecialChar(@RecipeLanguageTags, ' '),
						OriginalVersion = @OriginalVersion,
						TranslatedBy = @TranslatedBy
				
				WHERE  IDRecipe=@IDRecipe
					AND IDLanguage=@IDLanguage

			END
	END
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='RC-IN-0003';
		SET @USPReturnValue =@IDRecipe;
					                            
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











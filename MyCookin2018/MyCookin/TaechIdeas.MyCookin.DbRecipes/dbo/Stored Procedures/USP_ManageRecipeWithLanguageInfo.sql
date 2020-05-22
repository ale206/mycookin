






-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,28/04/2013,>
-- Description:	<Description,Manage Recipe base and language,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageRecipeWithLanguageInfo]
	@IDRecipe uniqueidentifier,
	@IDRecipeFather uniqueidentifier,
	@IDOwner uniqueidentifier,
	@NumberOfPerson int,
	@PreparationTimeMinute int,
	@CookingTimeMinute int,
	@RecipeDifficulties int,
	@IDRecipeImage uniqueidentifier,
	@IDRecipeVideo uniqueidentifier,
	@IDCity int,
	@CreationDate smalldatetime,
	@LastUpdate smalldatetime,
	@UpdatedByUser uniqueidentifier,
	@RecipeConsulted int,
	@RecipeAvgRating float,
	@isStarterRecipe bit,
	@DeletedOn smalldatetime,
	@BaseRecipe bit,
	@RecipeEnabled bit,
	@Checked bit,
	@RecipeCompletePerc int,
	@RecipePortionKcal float,
	@RecipePortionProteins float,
	@RecipePortionFats float,
	@RecipePortionCarbohydrates float,
	@RecipePortionAlcohol float,
	@RecipePortionQta float,
	@Vegetarian bit,
	@Vegan bit,
	@GlutenFree bit,
	@HotSpicy bit,
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
	@isDeleteOperation bit,
	@Draft bit,
	@RecipeRated int
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

SET @RecipeName = dbo.udfReplaceSpecialChar(@RecipeName, ' ')
SET @RecipeLanguageTags = dbo.udfReplaceSpecialChar(@RecipeLanguageTags, ' ')

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
	IF @isDeleteOperation = 1
		BEGIN
			UPDATE  dbo.Recipes
			SET DeletedOn = @DeletedOn
			WHERE IDRecipe = @IDRecipe
		END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT IDRecipe FROM Recipes WHERE IDRecipe = @IDRecipe)
			BEGIN
				INSERT INTO [dbo].Recipes 
					(IDRecipe 
					,IDRecipeFather 
					,IDOwner 
					,NumberOfPerson 
					,PreparationTimeMinute 
					,CookingTimeMinute 
					,RecipeDifficulties 
					,IDRecipeImage 
					,IDRecipeVideo 
					,IDCity 
					,CreationDate 
					,LastUpdate 
					,UpdatedByUser 
					,RecipeConsulted 
					,RecipeAvgRating 
					,isStarterRecipe 
					,DeletedOn 
					,BaseRecipe 
					,RecipeEnabled 
					,Checked 
					,RecipeCompletePerc 
					,RecipePortionKcal 
					,RecipePortionProteins 
					,RecipePortionFats 
					,RecipePortionCarbohydrates
					,RecipePortionQta
					,Vegetarian
					,Vegan
					,GlutenFree
					,HotSpicy
					,RecipePortionAlcohol
					,Draft
					,RecipeRated) 
				
				VALUES (@IDRecipe 
						,@IDRecipeFather 
						,@IDOwner 
						,@NumberOfPerson 
						,@PreparationTimeMinute 
						,@CookingTimeMinute 
						,@RecipeDifficulties 
						,@IDRecipeImage 
						,@IDRecipeVideo 
						,@IDCity 
						,@CreationDate 
						,@LastUpdate 
						,@UpdatedByUser 
						,@RecipeConsulted 
						,@RecipeAvgRating 
						,@isStarterRecipe 
						,@DeletedOn 
						,@BaseRecipe 
						,@RecipeEnabled 
						,@Checked 
						,@RecipeCompletePerc 
						,@RecipePortionKcal 
						,@RecipePortionProteins 
						,@RecipePortionFats 
						,@RecipePortionCarbohydrates
						,@RecipePortionQta
						,@Vegetarian
						,@Vegan
						,@GlutenFree
						,@HotSpicy
						,@RecipePortionAlcohol
						,@Draft
						,@RecipeRated)

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
					,@RecipeName 
					,@RecipeLanguageAutoTranslate 
					,@RecipeHistory 
					,@RecipeHistoryDate 
					,@RecipeNote 
					,@RecipeSuggestion 
					,@RecipeDisabled 
					,@GeoIDRegion 
					,@RecipeLanguageTags
					,@OriginalVersion
					,@TranslatedBy)
			END
		ELSE
			BEGIN
				UPDATE [DBRecipes].dbo.Recipes
				   SET	IDRecipeFather =@IDRecipeFather ,
						IDOwner =@IDOwner ,
						NumberOfPerson =@NumberOfPerson ,
						PreparationTimeMinute =@PreparationTimeMinute ,
						CookingTimeMinute =@CookingTimeMinute ,
						RecipeDifficulties =@RecipeDifficulties ,
						IDRecipeImage =@IDRecipeImage ,
						IDRecipeVideo =@IDRecipeVideo ,
						IDCity =@IDCity ,
						CreationDate =@CreationDate ,
						LastUpdate =@LastUpdate ,
						UpdatedByUser =@UpdatedByUser ,
						RecipeConsulted =@RecipeConsulted ,
						RecipeAvgRating =@RecipeAvgRating ,
						isStarterRecipe =@isStarterRecipe ,
						DeletedOn =@DeletedOn ,
						BaseRecipe =@BaseRecipe ,
						RecipeEnabled =@RecipeEnabled ,
						Checked =@Checked ,
						RecipeCompletePerc =@RecipeCompletePerc ,
						RecipePortionKcal =@RecipePortionKcal ,
						RecipePortionProteins =@RecipePortionProteins ,
						RecipePortionFats =@RecipePortionFats ,
						RecipePortionCarbohydrates =@RecipePortionCarbohydrates,
						RecipePortionQta=@RecipePortionQta,
						Vegetarian=@Vegetarian,
						Vegan=@Vegan,
						GlutenFree=@GlutenFree,
						HotSpicy=@HotSpicy,
						RecipePortionAlcohol=@RecipePortionAlcohol,
						Draft=@Draft,
						RecipeRated=@RecipeRated
				 
				 WHERE IDRecipe=@IDRecipe
				 
				UPDATE dbo.RecipesLanguages
				SET 	RecipeName =@RecipeName ,
						RecipeLanguageAutoTranslate =@RecipeLanguageAutoTranslate ,
						RecipeHistory =@RecipeHistory ,
						RecipeHistoryDate =@RecipeHistoryDate ,
						RecipeNote =@RecipeNote ,
						RecipeSuggestion =@RecipeSuggestion ,
						RecipeDisabled =@RecipeDisabled ,
						GeoIDRegion =@GeoIDRegion ,
						RecipeLanguageTags =@RecipeLanguageTags,
						OriginalVersion = @OriginalVersion,
						TranslatedBy = @TranslatedBy
				
				WHERE IDRecipeLanguage=@IDRecipeLanguage
						AND IDRecipe=@IDRecipe
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









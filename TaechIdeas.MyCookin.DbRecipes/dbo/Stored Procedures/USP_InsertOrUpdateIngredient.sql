
-- ================================================
-- =============================================
-- Author:		<Saverio Cammarata>
-- Create date: <20/07/2012,,>
-- Description:	<[USP_InsertOrUpdateIngredient],,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_InsertOrUpdateIngredient]
	
	@IDIngredientPreparationRecipe uniqueidentifier,
	@IDIngredientImage uniqueidentifier,
	@AverageWeightOfOnePiece float,
	@Kcal100gr float,
	@grProteins float,
	@grFats float,
	@grCarbohydrates float,
	@grAlcohol float,
	@mgCalcium float,
	@mgSodium float,
	@mgPhosphorus float,
	@mgPotassium float,
	@mgIron float,
	@mgMagnesium float,
	@mcgVitaminA float,
	@mgVitaminB1 float,
	@mgVitaminB2 float,
	@mcgVitaminB9 float,
	@mcgVitaminB12 float,
	@mgVitaminC float,
	@grSaturatedFat float,
	@grMonounsaturredFat float,
	@grPolyunsaturredFat float,
	@mgCholesterol float,
	@mgPhytosterols float,
	@mgOmega3 float,
	@IsForBaby bit,
	@IsVegetarian bit,
	@IsGlutenFree bit,
	@IsVegan bit,
	@IsHotSpicy bit,
	@Checked bit,
	@IngredientCreatedBy uniqueidentifier,
	@IngredientCreationDate smalldatetime,
	@IngredientModifiedByUser uniqueidentifier,
	@IngredientLastMod smalldatetime,
	@IngredientEnabled bit,
	@IDIngredient uniqueidentifier,
	@January bit,
	@February bit,
	@March bit,
	@April bit,
	@May bit,
	@June bit,
	@July bit,
	@August bit,
	@September bit,
	@October bit,
	@November bit,
	@December bit,
	@grDietaryFiber float,
	@grStarch float,
	@grSugar float 
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
   --insert
   IF EXISTS (SELECT IDIngredient FROM dbo.Ingredients WHERE IDIngredient=@IDIngredient)
   BEGIN 
			-- Update Ingredient
	UPDATE dbo.Ingredients
		SET IDIngredientPreparationRecipe = @IDIngredientPreparationRecipe,
			IDIngredientImage = @IDIngredientImage,
			AverageWeightOfOnePiece = @AverageWeightOfOnePiece,
			Kcal100gr = @Kcal100gr,
			grProteins = @grProteins,
			grFats = @grFats,
			grCarbohydrates = @grCarbohydrates,
			grAlcohol = @grAlcohol,
			mgCalcium = @mgCalcium,
			mgSodium = @mgSodium,
			mgPhosphorus = @mgPhosphorus,
			mgPotassium = @mgPotassium,
			mgIron = @mgIron,
			mgMagnesium = @mgMagnesium,
			mcgVitaminA = @mcgVitaminA,
			mgVitaminB1 = @mgVitaminB1,
			mgVitaminB2 = @mgVitaminB2,
			mcgVitaminB9 = @mcgVitaminB9,
			mcgVitaminB12 = @mcgVitaminB12,
			mgVitaminC = @mgVitaminC,
			grSaturatedFat = @grSaturatedFat,
			grMonounsaturredFat = @grMonounsaturredFat,
			grPolyunsaturredFat = @grPolyunsaturredFat,
			mgCholesterol = @mgCholesterol,
			mgPhytosterols = @mgPhytosterols,
			mgOmega3 = @mgOmega3,
			IsForBaby = @IsForBaby,
			IsVegetarian = @IsVegetarian,
			IsGlutenFree = @IsGlutenFree,
			IsHotSpicy = @IsHotSpicy,
			IsVegan = @IsVegan,
			Checked = @Checked,
			IngredientCreatedBy=@IngredientCreatedBy,
			IngredientCreationDate=@IngredientCreationDate,
			IngredientModifiedByUser = @IngredientModifiedByUser,
			IngredientLastMod = @IngredientLastMod,
			IngredientEnabled=@IngredientEnabled,
			January=@January,
			February=@February,
			March=@March,
			April=@April,
			May=@May,
			June=@June,
			July=@July,
			August=@August,
			September=@September,
			October=@October,
			November=@November,
			December=@December,
			grDietaryFiber=@grDietaryFiber, 
			grStarch=@grStarch, 
			grSugar=@grSugar 

		WHERE IDIngredient = @IDIngredient
		
	
		-- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='RC-IN-0001';
		SET @USPReturnValue ='';
   END
ELSE
	BEGIN
	
	INSERT INTO dbo.Ingredients(IDIngredient, IDIngredientPreparationRecipe,IDIngredientImage,AverageWeightOfOnePiece, 
								Kcal100gr, grProteins, grFats, grCarbohydrates, grAlcohol, mgCalcium, mgSodium, mgPhosphorus, 
								mgPotassium, mgIron, mgMagnesium, mcgVitaminA, mgVitaminB1, mgVitaminB2, mcgVitaminB9, mcgVitaminB12, 
								mgVitaminC, grSaturatedFat, grMonounsaturredFat, grPolyunsaturredFat, mgCholesterol, mgPhytosterols, mgOmega3, 
								IsForBaby, IsVegetarian,IsVegan,IsGlutenFree,IsHotSpicy,Checked,IngredientCreatedBy,IngredientCreationDate,
								IngredientModifiedByUser,IngredientLastMod,IngredientEnabled,January,February,March,April,May,June,July,
								August,September,October,November,December,grDietaryFiber, grStarch, grSugar)
	                    VALUES (@IDIngredient, @IDIngredientPreparationRecipe,@IDIngredientImage,@AverageWeightOfOnePiece, 
								@Kcal100gr, @grProteins, @grFats, @grCarbohydrates, @grAlcohol, @mgCalcium, @mgSodium, 
								@mgPhosphorus, @mgPotassium, @mgIron, @mgMagnesium, @mcgVitaminA, @mgVitaminB1, @mgVitaminB2, 
								@mcgVitaminB9, @mcgVitaminB12, @mgVitaminC, @grSaturatedFat, @grMonounsaturredFat, @grPolyunsaturredFat, 
								@mgCholesterol, @mgPhytosterols, @mgOmega3, @IsForBaby, @IsVegetarian,@IsVegan,@IsGlutenFree,
								@IsHotSpicy,@Checked,@IngredientCreatedBy,@IngredientCreationDate,@IngredientModifiedByUser,@IngredientLastMod,@IngredientEnabled,
								@January,@February,@March,@April,@May,@June,@July,@August,@September,@October,@November,@December,@grDietaryFiber, @grStarch, @grSugar)
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='RC-IN-0002';
		SET @USPReturnValue ='';
					                            
	END
   


	
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



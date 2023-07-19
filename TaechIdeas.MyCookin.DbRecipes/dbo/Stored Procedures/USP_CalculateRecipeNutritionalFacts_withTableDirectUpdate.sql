-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,29/04/2013,>
-- Description:	<Description,Calculate Recipe nutritional facts,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CalculateRecipeNutritionalFacts_withTableDirectUpdate]
	@IDRecipe uniqueidentifier
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction


DECLARE @NutrictionalInfo TABLE
(
  PropKey NVARCHAR(50),
  PropValue NVARCHAR(50)
)

DECLARE @IngrKcal FLOAT;
DECLARE @IngrProteins FLOAT;
DECLARE @IngrFats FLOAT;
DECLARE @IngrCarbohydrates FLOAT;
DECLARE @IngrAlcohol FLOAT;

DECLARE @TotRecipeKcal FLOAT;
DECLARE @TotRecipeProteins FLOAT;
DECLARE @TotRecipeFats FLOAT;
DECLARE @TotRecipeCarbohydrates FLOAT;
DECLARE @TotRecipeAlcohol FLOAT;
DECLARE @TotPercKalFromFat FLOAT;

DECLARE @CountVegetarian INT;
DECLARE @CountVegan INT;
DECLARE @CountGlutenFree INT;

DECLARE @Vegetarian BIT;
DECLARE @Vegan BIT;
DECLARE @GlutenFree BIT;

DECLARE @CountIngredient INT;

    SET @TotRecipeKcal = 0
	SET @TotRecipeProteins = 0
	SET @TotRecipeFats = 0
	SET @TotRecipeCarbohydrates = 0
	SET @TotRecipeAlcohol = 0

	DECLARE IngredientCursor CURSOR LOCAL
	FOR 
		SELECT  DISTINCT  dbo.Recipes.NumberOfPerson, Ingredients.IDIngredientPreparationRecipe, Ingredients.AverageWeightOfOnePiece,
						  dbo.Ingredients.Kcal100gr, dbo.Ingredients.grProteins, dbo.Ingredients.grFats, dbo.Ingredients.grCarbohydrates, 
						  dbo.Ingredients.grAlcohol, dbo.QuantityNotStd.PercentageFactor, 
						  dbo.RecipesIngredients.Quantity, isStandardQuantityType, dbo.IngredientsQuantityTypes.NoStdAvgWeight,
						  dbo.IngredientsQuantityTypes.IDIngredientQuantityType,dbo.Ingredients.[IsVegetarian],dbo.Ingredients.[IsVegan],
						  dbo.Ingredients.[IsGlutenFree]
		FROM         dbo.RecipesIngredients INNER JOIN
							  dbo.Ingredients ON dbo.Ingredients.IDIngredient = dbo.RecipesIngredients.IDIngredient INNER JOIN
							  dbo.Recipes ON dbo.RecipesIngredients.IDRecipe = dbo.Recipes.IDRecipe INNER JOIN
							  dbo.RecipesLanguages ON dbo.Recipes.IDRecipe = dbo.RecipesLanguages.IDRecipe INNER JOIN
							  dbo.IngredientsLanguages ON dbo.Ingredients.IDIngredient = dbo.IngredientsLanguages.IDIngredient LEFT OUTER JOIN
							  dbo.QuantityNotStdLanguage INNER JOIN
							  dbo.QuantityNotStd ON dbo.QuantityNotStdLanguage.IDQuantityNotStd = dbo.QuantityNotStd.IDQuantityNotStd ON 
							  dbo.RecipesIngredients.IDQuantityNotStd = dbo.QuantityNotStd.IDQuantityNotStd LEFT OUTER JOIN
							  dbo.IngredientsQuantityTypesLanguages INNER JOIN
							  dbo.IngredientsQuantityTypes ON dbo.IngredientsQuantityTypesLanguages.IDIngredientQuantityType = dbo.IngredientsQuantityTypes.IDIngredientQuantityType ON 
							  dbo.RecipesIngredients.IDQuantityType = dbo.IngredientsQuantityTypes.IDIngredientQuantityType
		WHERE     (dbo.Recipes.IDRecipe = @IDRecipe) 
		--AND (dbo.QuantityNotStdLanguage.IDLanguage = 2 OR dbo.QuantityNotStdLanguage.IDLanguage IS NULL) 
		--AND (dbo.IngredientsQuantityTypesLanguages.IDLanguage = 2 OR dbo.IngredientsQuantityTypesLanguages.IDLanguage IS NULL) 
		--AND (dbo.RecipesLanguages.IDLanguage = 2 OR dbo.RecipesLanguages.IDLanguage IS NULL) 
		--AND (dbo.IngredientsLanguages.IDLanguage = 2 OR dbo.IngredientsLanguages.IDLanguage IS NULL)
		AND(
		PercentageFactor is not null
		OR Quantity is not null
		OR NoStdAvgWeight IS NOT NULL
		)
		ORDER BY isStandardQuantityType DESC, PercentageFactor

	OPEN IngredientCursor 

	DECLARE @NumberOfPerson INT;
	DECLARE @IDIngredientPreparationRecipe UNIQUEIDENTIFIER;
	DECLARE @AverageWeightOfOnePiece FLOAT;
	DECLARE @Kcal100gr FLOAT;
	DECLARE @grProteins FLOAT;
	DECLARE @grFats FLOAT;
	DECLARE @grCarbohydrates FLOAT;
	DECLARE @grAlcohol FLOAT;
	DECLARE @PercentageFactor FLOAT;
	DECLARE @Quantity FLOAT;
	DECLARE @isStandardQuantityType BIT;
	DECLARE @NoStdAvgWeight FLOAT;
	DECLARE @IDIngredientQuantityType INT;
	DECLARE @IngredientComputed BIT;
	DECLARE @Qta FLOAT;
	DECLARE @TotalQta FLOAT;

	SET @CountIngredient=0;
	SET @CountVegetarian=0;
	SET @CountVegan=0;
	SET @CountGlutenFree=0;
	SET @TotalQta = 0;

	FETCH NEXT FROM IngredientCursor INTO @NumberOfPerson,@IDIngredientPreparationRecipe,@AverageWeightOfOnePiece,@Kcal100gr,@grProteins,@grFats,
											@grCarbohydrates,@grAlcohol,@PercentageFactor,@Quantity,@isStandardQuantityType,@NoStdAvgWeight,@IDIngredientQuantityType,
											@Vegetarian,@Vegan,@GlutenFree

	WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			BEGIN TRY
				--BEGIN INGREDIENT LOGIC
				SET @IngrKcal = 0
				SET @IngrProteins = 0
				SET @IngrFats = 0
				SET @IngrCarbohydrates = 0
				SET @IngrAlcohol = 0
				SET @IngredientComputed = 0
				SET @Qta = 0
				SET @CountIngredient = @CountIngredient + 1

				IF @Vegetarian = 1
					SET @CountVegetarian = @CountVegetarian + 1
				IF @Vegan = 1
					SET @CountVegan = @CountVegan + 1
				IF @GlutenFree = 1
					SET @CountGlutenFree = @CountGlutenFree + 1


				IF(@IDIngredientPreparationRecipe IS NOT NULL AND @IDIngredientPreparationRecipe<>'00000000-0000-0000-0000-000000000000')
					BEGIN
						DECLARE @IngrWithReicpePrepInfo TABLE
							(
							  PropKey NVARCHAR(50),
							  PropValue NVARCHAR(50)
							)
						
						INSERT INTO @IngrWithReicpePrepInfo
							EXEC USP_CalculateRecipeNutritionalFacts @IDRecipe = @IDIngredientPreparationRecipe
						
						SELECT	@IngrKcal = PropValue FROM @IngrWithReicpePrepInfo
							WHERE PropKey='TotRecipeKcal'
						SELECT	@IngrProteins = PropValue FROM @IngrWithReicpePrepInfo
							WHERE PropKey='TotRecipeProteins'
						SELECT	@IngrFats = PropValue FROM @IngrWithReicpePrepInfo
							WHERE PropKey='TotRecipeFats'
						SELECT	@IngrCarbohydrates = PropValue FROM @IngrWithReicpePrepInfo
							WHERE PropKey='TotRecipeCarbohydrates'
						SELECT	@IngrAlcohol = PropValue FROM @IngrWithReicpePrepInfo
							WHERE PropKey='TotRecipeAlcohol'
						SELECT	@TotalQta = PropValue FROM @IngrWithReicpePrepInfo
							WHERE PropKey='TotQta'

						SELECT	@Vegetarian = CONVERT(bit, PropValue) FROM @IngrWithReicpePrepInfo
							WHERE PropKey='Vegetarian'
						SELECT	@Vegan = CONVERT(bit, PropValue) FROM @IngrWithReicpePrepInfo
							WHERE PropKey='Vegan'
						SELECT	@GlutenFree = CONVERT(bit, PropValue) FROM @IngrWithReicpePrepInfo
							WHERE PropKey='GlutenFree'


						SET @Kcal100gr = (@IngrKcal/@TotalQta)*100
						SET @grProteins = (@IngrProteins/@TotalQta)*100
						SET @grFats = (@IngrFats/@TotalQta)*100
						SET @grCarbohydrates = (@IngrCarbohydrates/@TotalQta)*100
						SET @grAlcohol = (@IngrAlcohol/@TotalQta)*100
						--SET @TotalQta = 0
					END

				IF(@isStandardQuantityType=1 AND (@IngredientComputed=0))
					BEGIN
						--Ingredient with standard quantity type
						SET @IngrKcal = (@Kcal100gr/100)*@Quantity
						SET @IngrProteins = (@grProteins/100)*@Quantity
						SET @IngrFats = (@grFats/100)*@Quantity
						SET @IngrCarbohydrates = (@grCarbohydrates/100)*@Quantity
						SET @IngrAlcohol = (@grAlcohol/100)*@Quantity
						SET @IngredientComputed = 1
						SET @Qta = @Quantity
					END
				--Ingredient with NO standard quantity type
				IF(@Quantity IS NOT NULL AND @Quantity>0 AND @IDIngredientQuantityType=129 AND (@IngredientComputed=0))
					BEGIN
						SET @IngrKcal = (@Kcal100gr/100)*(@Quantity*@AverageWeightOfOnePiece)
						SET @IngrProteins = (@grProteins/100)*(@Quantity*@AverageWeightOfOnePiece)
						SET @IngrFats = (@grFats/100)*(@Quantity*@AverageWeightOfOnePiece)
						SET @IngrCarbohydrates = (@grCarbohydrates/100)*(@Quantity*@AverageWeightOfOnePiece)
						SET @IngrAlcohol = (@grAlcohol/100)*(@Quantity*@AverageWeightOfOnePiece)
						SET @Qta = (@Quantity*@AverageWeightOfOnePiece)
						SET @IngredientComputed = 1
					END
				ELSE IF((@PercentageFactor IS NOT NULL AND @PercentageFactor>0) AND (@AverageWeightOfOnePiece > 0) AND (@IngredientComputed=0))
					BEGIN
						SET @IngrKcal = (@Kcal100gr/100)*((@AverageWeightOfOnePiece/100)*@PercentageFactor)
						SET @IngrProteins = (@grProteins/100)*((@AverageWeightOfOnePiece/100)*@PercentageFactor)
						SET @IngrFats = (@grFats/100)*((@AverageWeightOfOnePiece/100)*@PercentageFactor)
						SET @IngrCarbohydrates = (@grCarbohydrates/100)*((@AverageWeightOfOnePiece/100)*@PercentageFactor)
						SET @IngrAlcohol = (@grAlcohol/100)*((@AverageWeightOfOnePiece/100)*@PercentageFactor)
						SET @Qta = ((@AverageWeightOfOnePiece/100)*@PercentageFactor)
						SET @IngredientComputed = 1
					END
				ELSE IF((@PercentageFactor IS NULL) AND (@NoStdAvgWeight > 0) AND (@IngredientComputed=0))
					BEGIN
						SET @IngrKcal = (@Kcal100gr/100)*(@NoStdAvgWeight * @Quantity)
						SET @IngrProteins = (@grProteins/100)*(@NoStdAvgWeight * @Quantity)
						SET @IngrFats = (@grFats/100)*(@NoStdAvgWeight * @Quantity)
						SET @IngrCarbohydrates = (@grCarbohydrates/100)*(@NoStdAvgWeight * @Quantity)
						SET @IngrAlcohol = (@grAlcohol/100)*(@NoStdAvgWeight * @Quantity)
						SET @Qta = (@NoStdAvgWeight * @Quantity)
						SET @IngredientComputed = 1
					END
				ELSE IF(@NoStdAvgWeight IS NOT NULL AND @NoStdAvgWeight>0 AND (@IngredientComputed=0) AND (@Quantity IS NOT NULL AND @Quantity >0))
					BEGIN
						SET @IngrKcal = (@Kcal100gr/100)*(@NoStdAvgWeight * @Quantity)
						SET @IngrProteins = (@grProteins/100)*(@NoStdAvgWeight * @Quantity)
						SET @IngrFats = (@grFats/100)*(@NoStdAvgWeight * @Quantity)
						SET @IngrCarbohydrates = (@grCarbohydrates/100)*(@NoStdAvgWeight * @Quantity)
						SET @IngrAlcohol = (@grAlcohol/100)*(@NoStdAvgWeight * @Quantity)
						SET @Qta = (@NoStdAvgWeight * @Quantity)
						SET @IngredientComputed = 1
					END
				ELSE IF(@NoStdAvgWeight IS NOT NULL AND @NoStdAvgWeight>0 AND (@IngredientComputed=0) AND (@Quantity IS NULL) AND (@PercentageFactor IS NOT NULL))
					BEGIN
						SET @IngrKcal = (@Kcal100gr/100)*(@NoStdAvgWeight/100 * @PercentageFactor)
						SET @IngrProteins = (@grProteins/100)*(@NoStdAvgWeight/100 * @PercentageFactor)
						SET @IngrFats = (@grFats/100)*(@NoStdAvgWeight/100 * @PercentageFactor)
						SET @IngrCarbohydrates = (@grCarbohydrates/100)*(@NoStdAvgWeight/100 * @PercentageFactor)
						SET @IngrAlcohol = (@grAlcohol/100)*(@NoStdAvgWeight/100 * @PercentageFactor)
						SET @Qta = (@NoStdAvgWeight/100 * @PercentageFactor)
						SET @IngredientComputed = 1
					END

				--END LOGIC

				SET @TotRecipeKcal =COALESCE(@TotRecipeKcal + @IngrKcal,@TotRecipeKcal)
				SET @TotRecipeProteins = COALESCE(@TotRecipeProteins + @IngrProteins,@TotRecipeProteins)
				SET @TotRecipeFats = COALESCE(@TotRecipeFats + @IngrFats,@TotRecipeFats)
				SET @TotRecipeCarbohydrates = COALESCE(@TotRecipeCarbohydrates + @IngrCarbohydrates,@TotRecipeCarbohydrates)
				SET @TotRecipeAlcohol = COALESCE(@TotRecipeAlcohol + @IngrAlcohol,@TotRecipeAlcohol)
				SET @TotalQta = COALESCE(@TotalQta + @Qta,@TotalQta)
		
			END TRY

			BEGIN CATCH
				
				SET @TotRecipeKcal =COALESCE(@TotRecipeKcal + @IngrKcal,@TotRecipeKcal)
				SET @TotRecipeProteins = COALESCE(@TotRecipeProteins + @IngrProteins,@TotRecipeProteins)
				SET @TotRecipeFats = COALESCE(@TotRecipeFats + @IngrFats,@TotRecipeFats)
				SET @TotRecipeCarbohydrates = COALESCE(@TotRecipeCarbohydrates + @IngrCarbohydrates,@TotRecipeCarbohydrates)
				SET @TotRecipeAlcohol = COALESCE(@TotRecipeAlcohol + @IngrAlcohol,@TotRecipeAlcohol)
				SET @TotalQta = COALESCE(@TotalQta + @Qta,@TotalQta)

			END CATCH

			FETCH NEXT FROM IngredientCursor INTO @NumberOfPerson,@IDIngredientPreparationRecipe,@AverageWeightOfOnePiece,@Kcal100gr,@grProteins,@grFats,
												@grCarbohydrates,@grAlcohol,@PercentageFactor,@Quantity,@isStandardQuantityType,@NoStdAvgWeight,@IDIngredientQuantityType,
												@Vegetarian,@Vegan,@GlutenFree
		END
		CLOSE IngredientCursor
		DEALLOCATE IngredientCursor		

		IF(@NumberOfPerson IS NULL OR @NumberOfPerson=0)
			BEGIN
				SET @NumberOfPerson=4
			END

		SET @TotRecipeKcal = @TotRecipeKcal / @NumberOfPerson
		SET @TotRecipeProteins = @TotRecipeProteins / @NumberOfPerson
		SET @TotRecipeFats = @TotRecipeFats / @NumberOfPerson
		SET @TotRecipeCarbohydrates = @TotRecipeCarbohydrates / @NumberOfPerson
		SET @TotRecipeAlcohol = @TotRecipeAlcohol / @NumberOfPerson
		SET @TotalQta = @TotalQta / @NumberOfPerson

		IF(@TotRecipeKcal>0)
			BEGIN
				SET @TotPercKalFromFat = (@TotRecipeFats * 9)/@TotRecipeKcal*100
			END
		
		--Fried Kcal correction
		BEGIN TRY
			DECLARE @isFriedRecipe BIT
			DECLARE @numCookType INT
			SET @isFriedRecipe = 0
			SELECT @isFriedRecipe = Value FROM RecipesPropertiesValues
				WHERE IDRecipe = @IDRecipe AND IDRecipeProperty = 25
			SELECT @numCookType = COUNT(IDRecipeProperty) FROM RecipesPropertiesValues
				WHERE IDRecipe = @IDRecipe
			IF @isFriedRecipe = 1
				BEGIN
					IF @TotalQta > 0
						BEGIN
							IF @numCookType = 1
								BEGIN
									SET @TotRecipeKcal = @TotRecipeKcal + (@TotalQta * 1.5)
								END
							ELSE
								BEGIN
									SET @TotRecipeKcal = @TotRecipeKcal + (@TotalQta * 0.5)
								END
						END
					ELSE
						BEGIN
							
							IF @numCookType = 1
								BEGIN
									SET @TotRecipeKcal = @TotRecipeKcal + 200
								END
							ELSE
								BEGIN
									SET @TotRecipeKcal = @TotRecipeKcal + 50
								END
						END
				END
			END TRY
			BEGIN CATCH
				SET @TotRecipeKcal = @TotRecipeKcal
			END CATCH
		--

		DECLARE @FinalVegan BIT
		DECLARE @FinalVegetarian BIT
		DECLARE @FinalGlutenFree BIT

		SET @FinalVegan = 0
		SET @FinalVegetarian = 0
		SET @FinalGlutenFree = 0

		IF @CountVegetarian = @CountIngredient
			SET @FinalVegetarian = 1
		IF @CountVegan = @CountIngredient
			SET @FinalVegan = 1
		IF @CountGlutenFree = @CountIngredient
			SET @FinalGlutenFree = 1

		UPDATE Recipes
			SET [RecipePortionKcal] = @TotRecipeKcal,
				[RecipePortionProteins]=@TotRecipeProteins,
				[RecipePortionFats]=@TotRecipeFats,
				[RecipePortionCarbohydrates]=@TotRecipeCarbohydrates,
				[RecipePortionQta]=@TotalQta,
				[RecipePortionAlcohol]=@TotRecipeAlcohol,
				[Vegetarian]=@FinalVegetarian,
				[Vegan]=@FinalVegan,
				[GlutenFree]=@FinalGlutenFree
			WHERE IDRecipe = @IDRecipe

		SET @TotRecipeKcal = ROUND((@TotRecipeKcal / @TotalQta)*100,2)
		SET @TotRecipeProteins = ROUND((@TotRecipeProteins / @TotalQta)*100,2)
		SET @TotRecipeFats = ROUND((@TotRecipeFats / @TotalQta)*100,2)
		SET @TotRecipeCarbohydrates = ROUND((@TotRecipeCarbohydrates / @TotalQta)*100,2)
		SET @TotRecipeAlcohol = ROUND((@TotRecipeAlcohol / @TotalQta)*100,2)

		UPDATE Ingredients
			SET Kcal100gr=@TotRecipeKcal, 
				grProteins=@TotRecipeProteins,
				grFats=@TotRecipeFats,
				grCarbohydrates=@TotRecipeCarbohydrates,
				grAlcohol=@TotRecipeAlcohol,
				IsVegan=@FinalVegan,
				IsVegetarian=@FinalVegetarian,
				IsGlutenFree=@FinalGlutenFree
			WHERE [IDIngredientPreparationRecipe]=@IDRecipe
		
		COMMIT
			
		Select 1 AS NutritionaFactsCalculated

END TRY

BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

  -- There was an error
  IF @@TRANCOUNT > 0
     ROLLBACK

	 SET @ResultExecutionCode = 'RC-ER-9999' --Error in the StoredProcedure
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
																		 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

	 		Select 0 AS NutritionaFactsCalculated


-- =======================================    
END CATCH
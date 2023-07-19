-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <30/01/2016>
-- Description:	<Get a list of all Ingredients with filter>
-- =============================================
CREATE PROCEDURE [dbo].[USP_IngredientLanguageList]
	@IDLanguage int,
	@offset int,
	@count int,
	@orderBy nvarchar(100),
	@isAscendent bit,			
	@search nvarchar(250)	

AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY

	SET NOCOUNT ON;

	WITH Results AS 
	(
		SELECT	
		--Ingredient
		IDIngredient, IDIngredientPreparationRecipe, IDIngredientImage, AverageWeightOfOnePiece, Kcal100gr, grProteins, grFats,
		grCarbohydrates, grAlcohol, mgCalcium, mgSodium, mgPhosphorus, mgPotassium, mgIron, mgMagnesium, mcgVitaminA, mgVitaminB1,
		mgVitaminB2, mcgVitaminB9, mcgVitaminB12, mgVitaminC, grSaturatedFat, grMonounsaturredFat, grPolyunsaturredFat, mgCholesterol,
		mgPhytosterols, mgOmega3, IsForBaby, IsMeat, IsFish, IsVegetarian, IsVegan, IsGlutenFree, IsHotSpicy, Checked, IngredientCreatedBy,
		IngredientCreationDate, IngredientModifiedByUser, IngredientLastMod, IngredientEnabled, January, February, March, April,
		May, June, July, August, September, October, November, December, grDietaryFiber, grStarch, grSugar,

		-- Ingredient Language
		IDIngredientLanguage, IDLanguage, IngredientSingular, IngredientPlural, IngredientDescription,
		isAutoTranslate, GeoIDRegion, FriendlyId,

			ROW_NUMBER() OVER 
				(	
					ORDER BY
						CASE WHEN @orderby='IngredientSingular' THEN IngredientSingular END,				
						CASE WHEN @orderby='IngredientPlural' THEN IngredientPlural END,
						CASE WHEN @orderby='IngredientCreationDate' THEN IngredientCreationDate END
				) as RowNumber,

		    COUNT(*) OVER () as TotalCount
		FROM	
		(	
			SELECT

		--Ingredient
		I.IDIngredient, I.IDIngredientPreparationRecipe, I.IDIngredientImage, I.AverageWeightOfOnePiece, I.Kcal100gr, I.grProteins, I.grFats,
		grCarbohydrates, I.grAlcohol, I.mgCalcium, I.mgSodium, I.mgPhosphorus, I.mgPotassium, I.mgIron, I.mgMagnesium, I.mcgVitaminA, I.mgVitaminB1,
		mgVitaminB2, I.mcgVitaminB9, I.mcgVitaminB12, I.mgVitaminC, I.grSaturatedFat, I.grMonounsaturredFat, I.grPolyunsaturredFat, I.mgCholesterol,
		mgPhytosterols, I.mgOmega3, I.IsForBaby, I.IsMeat, I.IsFish, I.IsVegetarian, I.IsVegan, I.IsGlutenFree, I.IsHotSpicy, I.Checked, I.IngredientCreatedBy,
		IngredientCreationDate, I.IngredientModifiedByUser, I.IngredientLastMod, I.IngredientEnabled, I.January, I.February, I.March, I.April,
		May, I.June, I.July, I.August, I.September, I.October, I.November, I.December, I.grDietaryFiber, I.grStarch, I.grSugar,

		L.IDIngredientLanguage, L.IDLanguage, L.IngredientSingular, L.IngredientPlural, L.IngredientDescription, 
		L.isAutoTranslate, L.GeoIDRegion, L.FriendlyId
			FROM Ingredients I	INNER JOIN IngredientsLanguages L
				ON I.IDIngredient = L.IDIngredient
				WHERE 
				IDLanguage = @IDLanguage 
				and ( (IngredientSingular LIKE '%' + @search + '%') 
					   OR (IngredientPlural LIKE '%' + @search + '%') 
					   
					)
		) as cpo
	)
	SELECT *, COUNT(*) OVER () AS RowsReturned FROM Results
	WHERE
		RowNumber between @offset and (@offset+@count)	
	ORDER BY 
		CASE WHEN @isAscendent = 1 THEN RowNumber END ASC,
		CASE WHEN @isAscendent = 0 THEN RowNumber END DESC
	

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

-- =======================================
END CATCH




	




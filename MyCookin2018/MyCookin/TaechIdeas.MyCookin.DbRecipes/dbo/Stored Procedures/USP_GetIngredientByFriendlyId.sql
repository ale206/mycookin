-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/02/2016>
-- Description:	<Get Ingredient detail by its friendly id>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetIngredientByFriendlyId]
	@FriendlyId nvarchar(250)

AS

BEGIN
    SELECT I.IDIngredient, I.IDIngredientPreparationRecipe, I.IDIngredientImage, I.AverageWeightOfOnePiece, I.Kcal100gr, I.grProteins, I.grFats, I.grCarbohydrates, I.grAlcohol, I.mgCalcium, I.mgSodium, I.mgPhosphorus, 
         I.mgPotassium, I.mgIron, I.mcgVitaminA, I.mgMagnesium, I.mgVitaminB1, I.mgVitaminB2, I.mcgVitaminB9, I.mgVitaminC, I.mcgVitaminB12, I.grSaturatedFat, I.grMonounsaturredFat, I.mgCholesterol, I.mgPhytosterols, 
         I.mgOmega3, I.IsForBaby, I.IsMeat, I.IsFish, I.IsVegan, I.IsVegetarian, I.IsGlutenFree, I.IsHotSpicy, I.Checked, I.IngredientCreationDate, I.IngredientEnabled, I.February, I.January, I.April, I.March, 
         I.May, I.June, I.July, I.August, I.September, I.October, I.November, I.December, I.grDietaryFiber, I.grStarch, I.grSugar, I.grPolyunsaturredFat, IL.IDIngredientLanguage, IL.IDLanguage, 
         IL.IngredientSingular, IL.IngredientPlural, IL.IngredientDescription, IL.isAutoTranslate, IL.FriendlyId, IL.GeoIDRegion, I.IngredientModifiedByUser, I.IngredientLastMod, I.IngredientCreatedBy
	FROM  Ingredients I INNER JOIN
			 IngredientsLanguages IL ON I.IDIngredient = IL.IDIngredient
	WHERE FriendlyId = @FriendlyId AND IngredientEnabled = 1

END



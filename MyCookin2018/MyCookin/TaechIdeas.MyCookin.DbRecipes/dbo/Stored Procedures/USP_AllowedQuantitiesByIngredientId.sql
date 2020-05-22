-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/02/2016>
-- Description:	<Get Ingredient allowed quantities by its id>
-- =============================================
CREATE PROCEDURE [dbo].[USP_AllowedQuantitiesByIngredientId]
	@IDIngredient uniqueidentifier,
	@IDLanguage int

AS

BEGIN
    SELECT IAQT.IDIngredientAllowedQuantityType, IAQT.IDingredient, IAQT.IDIngredientQuantityType, IQT.isWeight, IQT.isLiquid, IQT.isPiece, IQT.isStandardQuantityType, 
         IQT.EnabledToUser, IQT.ShowInIngredientList, IQTL.IDLanguage, IQTL.IngredientQuantityTypeSingular, IQTL.IngredientQuantityTypePlural, 
         IQTL.IngredientQuantityTypeX1000Singular, IQTL.ConvertionRatio, IQTL.IngredientQuantityTypeX1000Plural, IQTL.IngredientQuantityTypeWordsShowBefore, 
         IQTL.IngredientQuantityTypeWordsShowAfter
FROM  IngredientsAllowedQuantityTypes IAQT INNER JOIN
         IngredientsQuantityTypes IQT ON IAQT.IDIngredientQuantityType = IQT.IDIngredientQuantityType INNER JOIN
         IngredientsQuantityTypesLanguages IQTL ON IQT.IDIngredientQuantityType = IQTL.IDIngredientQuantityType
WHERE IAQT.IDIngredient = @IDIngredient AND IQTL.IDLanguage = @IDLanguage

END



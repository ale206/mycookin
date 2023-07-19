-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/02/2016>
-- Description:	<Get Ingredient categories by its id>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CategoriesByIngredientId]
	@IDIngredient uniqueidentifier,
	@IDLanguage int

AS

BEGIN
    SELECT IC.IDIngredientCategory, IC.IDIngredientCategoryFather, IC.Enabled, ICL.IDIngredientCategoryLanguage, ICL.IDLanguage, ICL.IngredientCategoryLanguage, 
         ICL.IngredientCategoryLanguageDesc, IIC.isPrincipalCategory
FROM  IngredientsCategories IC INNER JOIN
      IngredientsCategoriesLanguages ICL ON IC.IDIngredientCategory = ICL.IDIngredientCategory 
	  INNER JOIN
      IngredientsIngredientsCategories IIC ON IC.IDIngredientCategory = IIC.IDIngredientCategory
WHERE IIC.IDIngredient = @IDIngredient AND IDLanguage = @IDLanguage

END



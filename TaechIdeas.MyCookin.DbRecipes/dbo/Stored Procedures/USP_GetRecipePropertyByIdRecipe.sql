-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/11/2015>
-- Description:	<GetRecipePropertyByIdRecipe>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipePropertyByIdRecipe]
	@IDRecipe uniqueidentifier

AS
BEGIN
     
SELECT        RecipesPropertiesValues.Value, RecipesPropertiesValues.IDRecipePropertyValue, RecipesPropertiesValues.IDRecipe, RecipeProperties.IDRecipeProperty, RecipePropertiesTypes.IDRecipePropertyType, 
                         RecipePropertiesTypes.isDishType, RecipePropertiesTypes.isCookingType, RecipePropertiesTypes.isColorType, RecipePropertiesTypes.isEatType, RecipePropertiesTypes.isUseType, 
                         RecipePropertiesTypes.isPeriodType
FROM            RecipeProperties INNER JOIN
                         RecipePropertiesTypes ON RecipeProperties.IDRecipePropertyType = RecipePropertiesTypes.IDRecipePropertyType INNER JOIN
                         RecipesPropertiesValues ON RecipeProperties.IDRecipeProperty = RecipesPropertiesValues.IDRecipeProperty
WHERE        (RecipesPropertiesValues.IDRecipe = @IDRecipe)


END



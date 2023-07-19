-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/11/2015>
-- Description:	<GetRecipePropertyByIdRecipeAndLanguage>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipePropertyByIdRecipeAndLanguage]
	@IDRecipe uniqueidentifier,
	@IDLanguage int
AS
BEGIN
     
	 SELECT        RecipesPropertiesValues.Value, RecipesPropertiesValues.IDRecipePropertyValue, RecipesPropertiesValues.IDRecipe, RecipeProperties.IDRecipeProperty, RecipePropertiesTypes.IDRecipePropertyType, 
                         RecipePropertiesTypes.isDishType, RecipePropertiesTypes.isCookingType, RecipePropertiesTypes.isColorType, RecipePropertiesTypes.isEatType, RecipePropertiesTypes.isUseType, 
                         RecipePropertiesTypes.isPeriodType, RecipePropertiesLanguages.IDRecipePropertyLanguage, RecipePropertiesLanguages.RecipeProperty, RecipePropertiesTypesLanguages.RecipePropertyType, 
                         RecipePropertiesTypesLanguages.IDLanguage, RecipePropertiesTypesLanguages.IDRecipePropertyTypeLanguage
FROM            RecipeProperties INNER JOIN
                         RecipePropertiesLanguages ON RecipeProperties.IDRecipeProperty = RecipePropertiesLanguages.IDRecipeProperty INNER JOIN
                         RecipePropertiesTypes ON RecipeProperties.IDRecipePropertyType = RecipePropertiesTypes.IDRecipePropertyType INNER JOIN
                         RecipePropertiesTypesLanguages ON RecipePropertiesTypes.IDRecipePropertyType = RecipePropertiesTypesLanguages.IDRecipePropertyType INNER JOIN
                         RecipesPropertiesValues ON RecipeProperties.IDRecipeProperty = RecipesPropertiesValues.IDRecipeProperty
WHERE        (RecipesPropertiesValues.IDRecipe = @IDRecipe) AND (RecipePropertiesTypesLanguages.IDLanguage = @IDLanguage)


END



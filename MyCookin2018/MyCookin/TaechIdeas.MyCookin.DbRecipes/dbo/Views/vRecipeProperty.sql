
CREATE VIEW [dbo].[vRecipeProperty]
WITH SCHEMABINDING
AS
SELECT   dbo.RecipePropertiesLanguages.IDRecipePropertyLanguage, dbo.RecipePropertiesLanguages.IDRecipeProperty, 
                      dbo.RecipeProperties.IDRecipePropertyType, dbo.RecipeProperties.OrderPosition, dbo.RecipeProperties.Enabled, dbo.RecipePropertiesLanguages.IDLanguage, 
                      dbo.RecipePropertiesLanguages.RecipeProperty, dbo.RecipePropertiesLanguages.RecipePropertyToolTip
FROM         dbo.RecipeProperties INNER JOIN
                      dbo.RecipePropertiesLanguages ON dbo.RecipeProperties.IDRecipeProperty = dbo.RecipePropertiesLanguages.IDRecipeProperty
WHERE     (dbo.RecipeProperties.Enabled = 1)
--ORDER BY dbo.RecipeProperties.IDRecipePropertyType, dbo.RecipeProperties.OrderPosition, dbo.RecipePropertiesLanguages.IDLanguage


GO
CREATE UNIQUE CLUSTERED INDEX [IX_IDRecipePropertyLanguage]
    ON [dbo].[vRecipeProperty]([IDRecipePropertyLanguage] ASC);


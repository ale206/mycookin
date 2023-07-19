CREATE VIEW [dbo].[vIndexedRecipeLang_It]
WITH SCHEMABINDING
AS
SELECT dbo.RecipesLanguages.IDRecipeLanguage,dbo.Recipes.IDRecipe,dbo.RecipesLanguages.RecipeName
FROM dbo.Recipes INNER JOIN dbo.RecipesLanguages ON dbo.Recipes.IDRecipe = dbo.RecipesLanguages.IDRecipe
WHERE dbo.RecipesLanguages.IDLanguage = 2


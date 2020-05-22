
CREATE VIEW [dbo].[vAllRecipesNames_Es]
WITH SCHEMABINDING 
AS
SELECT IDRecipeLanguage, RecipeName
FROM  dbo.RecipesLanguages
WHERE (IDLanguage = 3)
GO
CREATE UNIQUE CLUSTERED INDEX [IX_vAllRecipesNames_Es]
    ON [dbo].[vAllRecipesNames_Es]([IDRecipeLanguage] ASC) WITH (FILLFACTOR = 80);


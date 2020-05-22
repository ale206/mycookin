
CREATE VIEW [dbo].[vAllRecipesNames_En]
WITH SCHEMABINDING 
AS
SELECT IDRecipeLanguage, RecipeName
FROM  dbo.RecipesLanguages
WHERE (IDLanguage = 1)
GO
CREATE UNIQUE CLUSTERED INDEX [IX_vAllRecipesNames_En]
    ON [dbo].[vAllRecipesNames_En]([IDRecipeLanguage] ASC) WITH (FILLFACTOR = 80);


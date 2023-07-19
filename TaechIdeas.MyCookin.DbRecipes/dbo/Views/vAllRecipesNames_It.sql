
CREATE VIEW [dbo].[vAllRecipesNames_It]
WITH SCHEMABINDING 
AS
SELECT IDRecipeLanguage, RecipeName
FROM  dbo.RecipesLanguages
WHERE (IDLanguage = 2)
GO
CREATE UNIQUE CLUSTERED INDEX [IX_vAllRecipesNames_It]
    ON [dbo].[vAllRecipesNames_It]([IDRecipeLanguage] ASC) WITH (FILLFACTOR = 80);


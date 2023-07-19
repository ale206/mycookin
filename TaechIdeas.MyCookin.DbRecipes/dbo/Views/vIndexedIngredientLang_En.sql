
CREATE VIEW [dbo].[vIndexedIngredientLang_En]
WITH SCHEMABINDING 
AS
SELECT        dbo.Ingredients.IDIngredient, dbo.IngredientsLanguages.IngredientSingular, dbo.IngredientsLanguages.IngredientPlural, 
                         dbo.IngredientsLanguages.IngredientDescription, dbo.Ingredients.Kcal100gr, dbo.Ingredients.IsVegetarian, dbo.Ingredients.IsVegan, 
                         dbo.Ingredients.IsGlutenFree, dbo.Languages.IDLanguage, dbo.Languages.Language, dbo.Ingredients.IsHotSpicy, 
                         dbo.Ingredients.AverageWeightOfOnePiece, dbo.IngredientsLanguages.IDIngredientLanguage, FriendlyId
FROM            dbo.Ingredients INNER JOIN
                         dbo.IngredientsLanguages ON dbo.Ingredients.IDIngredient = dbo.IngredientsLanguages.IDIngredient INNER JOIN
                         dbo.Languages ON dbo.IngredientsLanguages.IDLanguage = dbo.Languages.IDLanguage
WHERE        (dbo.Ingredients.IngredientEnabled = 1) AND (dbo.Languages.IDLanguage = 1)


GO
CREATE UNIQUE CLUSTERED INDEX [IX_IDIngredientLanguage]
    ON [dbo].[vIndexedIngredientLang_En]([IDIngredientLanguage] ASC);


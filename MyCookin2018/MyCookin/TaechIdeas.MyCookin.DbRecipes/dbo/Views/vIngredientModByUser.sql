CREATE VIEW [dbo].[vIngredientModByUser]
AS
SELECT     COUNT(dbo.IngredientsLanguages.IDIngredient) AS NumIngredient, dbo.Ingredients.IngredientModifiedByUser
FROM         dbo.Ingredients INNER JOIN
                      dbo.IngredientsLanguages ON dbo.Ingredients.IDIngredient = dbo.IngredientsLanguages.IDIngredient
WHERE     (dbo.IngredientsLanguages.IDLanguage = 2) AND (dbo.Ingredients.Checked = 1)
GROUP BY dbo.Ingredients.IngredientModifiedByUser
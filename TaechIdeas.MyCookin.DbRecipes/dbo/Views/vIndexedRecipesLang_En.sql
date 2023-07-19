
CREATE VIEW [dbo].[vIndexedRecipesLang_En] WITH SCHEMABINDING
AS
SELECT IDRecipeIndex, IDRecipeLanguage, IDRecipe, RecipeName, IDLanguage, SearchPreference, PreparationTimeMinute, CookingTimeMinute, RecipeDifficulties, 
               IDRecipeImage, CreationDate, LastUpdate, RecipeConsulted, RecipeAvgRating, isStarterRecipe, DeletedOn, RecipeEnabled, Checked, RecipePortionKcal, 
               Vegetarian, Vegan, GlutenFree, Draft, IndexInserted
FROM  dbo.RecipesIndex
WHERE (IDLanguage = 1)


GO
CREATE UNIQUE CLUSTERED INDEX [IX_vIndexedRecipesLang_En]
    ON [dbo].[vIndexedRecipesLang_En]([IDRecipeIndex] ASC) WITH (FILLFACTOR = 80);


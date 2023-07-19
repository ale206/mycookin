
CREATE VIEW [dbo].[vIndexedRecipesLang_Es] WITH SCHEMABINDING
AS
SELECT IDRecipeIndex, IDRecipeLanguage, IDRecipe, RecipeName, IDLanguage, SearchPreference, PreparationTimeMinute, CookingTimeMinute, RecipeDifficulties, 
               IDRecipeImage, CreationDate, LastUpdate, RecipeConsulted, RecipeAvgRating, isStarterRecipe, DeletedOn, RecipeEnabled, Checked, RecipePortionKcal, 
               Vegetarian, Vegan, GlutenFree, Draft, IndexInserted
FROM  dbo.RecipesIndex
WHERE (IDLanguage = 3)


GO
CREATE UNIQUE CLUSTERED INDEX [IX_vIndexedRecipesLang_Es]
    ON [dbo].[vIndexedRecipesLang_Es]([IDRecipeIndex] ASC) WITH (FILLFACTOR = 80);


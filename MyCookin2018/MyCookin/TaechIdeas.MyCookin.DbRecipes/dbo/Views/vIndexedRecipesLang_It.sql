
CREATE VIEW [dbo].[vIndexedRecipesLang_It] WITH SCHEMABINDING
AS
SELECT IDRecipeIndex, IDRecipeLanguage, IDRecipe, RecipeName, IDLanguage, SearchPreference, PreparationTimeMinute, CookingTimeMinute, RecipeDifficulties, 
               IDRecipeImage, CreationDate, LastUpdate, RecipeConsulted, RecipeAvgRating, isStarterRecipe, DeletedOn, RecipeEnabled, Checked, RecipePortionKcal, 
               Vegetarian, Vegan, GlutenFree, Draft, IndexInserted
FROM  dbo.RecipesIndex
WHERE (IDLanguage = 2)


GO
CREATE UNIQUE CLUSTERED INDEX [IX_vIndexedRecipesLang_It]
    ON [dbo].[vIndexedRecipesLang_It]([IDRecipeIndex] ASC) WITH (FILLFACTOR = 80);


CREATE TABLE [dbo].[BestRecipes]
(
	[BestRecipeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IDRecipe] UNIQUEIDENTIFIER NOT NULL, 
    [Enabled] BIT NOT NULL, 
    [InsertedOn] DATETIME NOT NULL
)

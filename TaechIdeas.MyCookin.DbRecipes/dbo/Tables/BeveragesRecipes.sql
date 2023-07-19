CREATE TABLE [dbo].[BeveragesRecipes] (
    [IDBeverageRecipe]        UNIQUEIDENTIFIER CONSTRAINT [DF_BeveragesRecipes_IDBeverageRecipe] DEFAULT (newid()) NOT NULL,
    [IDRecipe]                UNIQUEIDENTIFIER NOT NULL,
    [IDBeverage]              UNIQUEIDENTIFIER NOT NULL,
    [IDUserSuggestedBy]       UNIQUEIDENTIFIER NOT NULL,
    [DateSuggestion]          SMALLDATETIME    NOT NULL,
    [BeverageRecipeAvgRating] FLOAT (53)       NULL,
    CONSTRAINT [PK_BeveragesRecipes] PRIMARY KEY CLUSTERED ([IDBeverageRecipe] ASC),
    CONSTRAINT [FK_BeveragesRecipes_Beverages] FOREIGN KEY ([IDBeverage]) REFERENCES [dbo].[Beverages] ([IDBeverage]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BeveragesRecipes_Recipes] FOREIGN KEY ([IDRecipe]) REFERENCES [dbo].[Recipes] ([IDRecipe]) ON DELETE CASCADE ON UPDATE CASCADE
);


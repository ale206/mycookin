CREATE TABLE [dbo].[BeveragesRecipesVotes] (
    [IDBeverageRecipeVote] UNIQUEIDENTIFIER CONSTRAINT [DF_BeveragesVotes_IDRecipeVote] DEFAULT (newid()) NOT NULL,
    [IDBeverageRecipe]     UNIQUEIDENTIFIER NOT NULL,
    [IDUser]               UNIQUEIDENTIFIER NOT NULL,
    [RecipeVoteDate]       SMALLDATETIME    NOT NULL,
    [RecipeVote]           INT              NOT NULL,
    CONSTRAINT [PK_BeveragesRecipesVotes] PRIMARY KEY CLUSTERED ([IDBeverageRecipeVote] ASC),
    CONSTRAINT [FK_BeveragesRecipesVotes_BeveragesRecipes] FOREIGN KEY ([IDBeverageRecipe]) REFERENCES [dbo].[BeveragesRecipes] ([IDBeverageRecipe]) ON DELETE CASCADE ON UPDATE CASCADE
);


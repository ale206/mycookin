CREATE TABLE [dbo].[RecipesVotes] (
    [IDRecipeVote]   UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesVotes_IDRecipeVote] DEFAULT (newid()) NOT NULL,
    [IDRecipe]       UNIQUEIDENTIFIER NOT NULL,
    [IDUser]         UNIQUEIDENTIFIER NOT NULL,
    [RecipeVoteDate] SMALLDATETIME    NOT NULL,
    [RecipeVote]     FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_RecipesVotes] PRIMARY KEY CLUSTERED ([IDRecipeVote] ASC),
    CONSTRAINT [FK_RecipesVotes_Recipes] FOREIGN KEY ([IDRecipe]) REFERENCES [dbo].[Recipes] ([IDRecipe]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IDRecipe-IDUser]
    ON [dbo].[RecipesVotes]([IDRecipe] ASC, [IDUser] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IDRecipe]
    ON [dbo].[RecipesVotes]([IDRecipe] ASC);


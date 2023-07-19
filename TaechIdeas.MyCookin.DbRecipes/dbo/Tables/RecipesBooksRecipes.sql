CREATE TABLE [dbo].[RecipesBooksRecipes] (
    [IDRecipeBookRecipe] UNIQUEIDENTIFIER CONSTRAINT [DF_CookBooksRecipes_IDCookBookRecipe] DEFAULT (newid()) NOT NULL,
    [IDUser]             UNIQUEIDENTIFIER NOT NULL,
    [IDRecipe]           UNIQUEIDENTIFIER NOT NULL,
    [RecipeAddedOn]      SMALLDATETIME    NOT NULL,
    [RecipeOrder]        INT              NULL,
    CONSTRAINT [PK_CookBooksRecipes] PRIMARY KEY CLUSTERED ([IDRecipeBookRecipe] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IDRecipe-IDUser_Unique]
    ON [dbo].[RecipesBooksRecipes]([IDUser] ASC, [IDRecipe] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IDUser]
    ON [dbo].[RecipesBooksRecipes]([IDUser] ASC)
    INCLUDE([IDRecipe], [RecipeAddedOn]);


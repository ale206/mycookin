CREATE TABLE [dbo].[RecipesPresentations] (
    [IDRecipePresentation]         UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesPresentations_IDRecipePresentation] DEFAULT (newid()) NOT NULL,
    [IDRecipe]                     UNIQUEIDENTIFIER NOT NULL,
    [IDUser]                       UNIQUEIDENTIFIER NOT NULL,
    [IDRecipePresentationPhoto]    UNIQUEIDENTIFIER NOT NULL,
    [RecipePresentationAddedOn]    SMALLDATETIME    NOT NULL,
    [ReciapePresentationDeletedOn] SMALLDATETIME    NULL,
    CONSTRAINT [PK_RecipesPresentations] PRIMARY KEY CLUSTERED ([IDRecipePresentation] ASC),
    CONSTRAINT [FK_RecipesPresentations_Recipes] FOREIGN KEY ([IDRecipe]) REFERENCES [dbo].[Recipes] ([IDRecipe]) ON DELETE CASCADE ON UPDATE CASCADE
);


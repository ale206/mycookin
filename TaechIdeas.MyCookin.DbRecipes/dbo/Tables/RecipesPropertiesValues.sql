CREATE TABLE [dbo].[RecipesPropertiesValues] (
    [IDRecipePropertyValue] UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesPropertiesValues_IDRecipePropertyValue] DEFAULT (newid()) NOT NULL,
    [IDRecipe]              UNIQUEIDENTIFIER NOT NULL,
    [IDRecipeProperty]      INT              NOT NULL,
    [Value]                 BIT              CONSTRAINT [DF_RecipesPropertiesValues_Value] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RecipesPropertiesValues] PRIMARY KEY CLUSTERED ([IDRecipePropertyValue] ASC),
    CONSTRAINT [FK_RecipesPropertiesValues_RecipeProperties] FOREIGN KEY ([IDRecipeProperty]) REFERENCES [dbo].[RecipeProperties] ([IDRecipeProperty]) ON UPDATE CASCADE,
    CONSTRAINT [FK_RecipesPropertiesValues_Recipes1] FOREIGN KEY ([IDRecipe]) REFERENCES [dbo].[Recipes] ([IDRecipe]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IDRecipeProperty_Value]
    ON [dbo].[RecipesPropertiesValues]([IDRecipeProperty] ASC, [Value] ASC)
    INCLUDE([IDRecipe]);


GO
CREATE NONCLUSTERED INDEX [IX_IDRecipe]
    ON [dbo].[RecipesPropertiesValues]([IDRecipe] ASC);


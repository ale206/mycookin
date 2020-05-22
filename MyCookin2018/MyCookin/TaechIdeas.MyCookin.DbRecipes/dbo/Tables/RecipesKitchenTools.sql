CREATE TABLE [dbo].[RecipesKitchenTools] (
    [IDRecipeKitchenTool] UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesKitchenTools_IDRecipeKitchenTool] DEFAULT (newid()) NOT NULL,
    [IDRecipe]            UNIQUEIDENTIFIER NOT NULL,
    [IDKitchenTool]       UNIQUEIDENTIFIER NOT NULL,
    [isNeedful]           BIT              NOT NULL,
    CONSTRAINT [PK_RecipesKitchenTools] PRIMARY KEY CLUSTERED ([IDRecipeKitchenTool] ASC),
    CONSTRAINT [FK_RecipesKitchenTools_KitchenTools] FOREIGN KEY ([IDKitchenTool]) REFERENCES [dbo].[KitchenTools] ([IDKitchenTool]) ON UPDATE CASCADE,
    CONSTRAINT [FK_RecipesKitchenTools_Recipes] FOREIGN KEY ([IDRecipe]) REFERENCES [dbo].[Recipes] ([IDRecipe]) ON UPDATE CASCADE
);


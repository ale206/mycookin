CREATE TABLE [dbo].[RecipesPresentationsKitchenTools] (
    [IDRecipePresentationKitchenTool] UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesPresentationsKitchenTools_IDRecipePresentationKitchenTool] DEFAULT (newid()) NOT NULL,
    [IDRecipePresentation]            UNIQUEIDENTIFIER NOT NULL,
    [IDKitchenTool]                   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RecipesPresentationsKitchenTools] PRIMARY KEY CLUSTERED ([IDRecipePresentationKitchenTool] ASC),
    CONSTRAINT [FK_RecipesPresentationsKitchenTools_KitchenTools] FOREIGN KEY ([IDKitchenTool]) REFERENCES [dbo].[KitchenTools] ([IDKitchenTool]),
    CONSTRAINT [FK_RecipesPresentationsKitchenTools_RecipesPresentations] FOREIGN KEY ([IDRecipePresentation]) REFERENCES [dbo].[RecipesPresentations] ([IDRecipePresentation]) ON DELETE CASCADE ON UPDATE CASCADE
);


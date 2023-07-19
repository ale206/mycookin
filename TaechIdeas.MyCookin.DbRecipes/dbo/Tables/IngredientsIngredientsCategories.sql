CREATE TABLE [dbo].[IngredientsIngredientsCategories] (
    [IDIngredientIngredientCategory] UNIQUEIDENTIFIER CONSTRAINT [DF_IngredientsIngredientsCategories_IDIngredientIngredientCategory] DEFAULT (newid()) NOT NULL,
    [IDIngredient]                   UNIQUEIDENTIFIER NOT NULL,
    [IDIngredientCategory]           INT              NOT NULL,
    [isPrincipalCategory]            BIT              NOT NULL,
    CONSTRAINT [PK_IngredientsIngredientsCategories] PRIMARY KEY CLUSTERED ([IDIngredientIngredientCategory] ASC),
    CONSTRAINT [FK_IngredientsIngredientsCategories_Ingredients] FOREIGN KEY ([IDIngredient]) REFERENCES [dbo].[Ingredients] ([IDIngredient]) ON UPDATE CASCADE,
    CONSTRAINT [FK_IngredientsIngredientsCategories_IngredientsCategories] FOREIGN KEY ([IDIngredientCategory]) REFERENCES [dbo].[IngredientsCategories] ([IDIngredientCategory]) ON DELETE CASCADE ON UPDATE CASCADE
);


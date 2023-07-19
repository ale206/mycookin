CREATE TABLE [dbo].[IngredientsAllowedQuantityTypes] (
    [IDIngredientAllowedQuantityType] UNIQUEIDENTIFIER CONSTRAINT [DF_IngredientsAllowedQuantityTypes_IDIngredientAllowedQuantityType] DEFAULT (newid()) NOT NULL,
    [IDingredient]                    UNIQUEIDENTIFIER NULL,
    [IDIngredientQuantityType]        INT              NULL,
    CONSTRAINT [PK_IngredientsAllowedQuantityTypes] PRIMARY KEY CLUSTERED ([IDIngredientAllowedQuantityType] ASC),
    CONSTRAINT [FK_IngredientsAllowedQuantityTypes_Ingredients] FOREIGN KEY ([IDingredient]) REFERENCES [dbo].[Ingredients] ([IDIngredient]),
    CONSTRAINT [FK_IngredientsAllowedQuantityTypes_IngredientsQuantityTypes] FOREIGN KEY ([IDIngredientQuantityType]) REFERENCES [dbo].[IngredientsQuantityTypes] ([IDIngredientQuantityType])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IDIngredient_IDQuantityType]
    ON [dbo].[IngredientsAllowedQuantityTypes]([IDingredient] ASC, [IDIngredientQuantityType] ASC);


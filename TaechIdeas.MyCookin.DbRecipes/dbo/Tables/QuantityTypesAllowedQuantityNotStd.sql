CREATE TABLE [dbo].[QuantityTypesAllowedQuantityNotStd] (
    [IDQuantityTypeAllowedQuantityNotStd] INT IDENTITY (1, 1) NOT NULL,
    [IDIngredientQuantityType]            INT NOT NULL,
    [IDQuantityNotStd]                    INT NOT NULL,
    CONSTRAINT [PK_QuantityTypesAllowedQuantityNotStd] PRIMARY KEY CLUSTERED ([IDQuantityTypeAllowedQuantityNotStd] ASC),
    CONSTRAINT [FK_QuantityTypesAllowedQuantityNotStd_IngredientsQuantityTypes] FOREIGN KEY ([IDIngredientQuantityType]) REFERENCES [dbo].[IngredientsQuantityTypes] ([IDIngredientQuantityType]),
    CONSTRAINT [FK_QuantityTypesAllowedQuantityNotStd_QuantityNotStd] FOREIGN KEY ([IDQuantityNotStd]) REFERENCES [dbo].[QuantityNotStd] ([IDQuantityNotStd])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UniqueIDIngrQtaType_IDQtaNotStd]
    ON [dbo].[QuantityTypesAllowedQuantityNotStd]([IDIngredientQuantityType] ASC, [IDQuantityNotStd] ASC);


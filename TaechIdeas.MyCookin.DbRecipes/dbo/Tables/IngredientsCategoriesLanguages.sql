CREATE TABLE [dbo].[IngredientsCategoriesLanguages] (
    [IDIngredientCategoryLanguage]   INT            IDENTITY (1, 1) NOT NULL,
    [IDIngredientCategory]           INT            NOT NULL,
    [IDLanguage]                     INT            NOT NULL,
    [IngredientCategoryLanguage]     NVARCHAR (100) NOT NULL,
    [IngredientCategoryLanguageDesc] NVARCHAR (250) NULL,
    CONSTRAINT [PK_IngredientsCategoriesLanguages] PRIMARY KEY CLUSTERED ([IDIngredientCategoryLanguage] ASC),
    CONSTRAINT [FK_IngredientsCategoriesLanguages_IngredientsCategories] FOREIGN KEY ([IDIngredientCategory]) REFERENCES [dbo].[IngredientsCategories] ([IDIngredientCategory]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UNIQ_IDIngredientCategory_IDLanguage]
    ON [dbo].[IngredientsCategoriesLanguages]([IDIngredientCategory] ASC, [IDLanguage] ASC);


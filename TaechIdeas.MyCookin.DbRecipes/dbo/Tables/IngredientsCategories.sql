CREATE TABLE [dbo].[IngredientsCategories] (
    [IDIngredientCategory]       INT IDENTITY (1, 1) NOT NULL,
    [IDIngredientCategoryFather] INT NULL,
    [Enabled]                    BIT CONSTRAINT [DF_IngredientsCategories_Enabled] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_IngredientsCategories] PRIMARY KEY CLUSTERED ([IDIngredientCategory] ASC)
);


CREATE TABLE [dbo].[RecipesIngredientsLanguages] (
    [IDRecipeIngredientLanguage] UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesIngredientsLanguages_IDRecipeIngredientLanguage] DEFAULT (newid()) NOT NULL,
    [IDRecipeIngredient]         UNIQUEIDENTIFIER NOT NULL,
    [IDLanguage]                 INT              NOT NULL,
    [RecipeIngredientNote]       NVARCHAR (250)   NULL,
    [RecipeIngredientGroupName]  NVARCHAR (150)   NULL,
    [isAutoTranslate]            BIT              NOT NULL,
    CONSTRAINT [PK_RecipesIngredientsLanguages] PRIMARY KEY CLUSTERED ([IDRecipeIngredientLanguage] ASC),
    CONSTRAINT [FK_RecipesIngredientsLanguages_RecipesIngredients] FOREIGN KEY ([IDRecipeIngredient]) REFERENCES [dbo].[RecipesIngredients] ([IDRecipeIngredient]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IDRecipeIngredient_IDLang_Unique]
    ON [dbo].[RecipesIngredientsLanguages]([IDRecipeIngredient] ASC, [IDLanguage] ASC);


GO
CREATE STATISTICS [_dta_stat_237243900_1_2]
    ON [dbo].[RecipesIngredientsLanguages]([IDRecipeIngredientLanguage], [IDRecipeIngredient]);


GO
CREATE STATISTICS [_dta_stat_237243900_2_3_1]
    ON [dbo].[RecipesIngredientsLanguages]([IDRecipeIngredient], [IDLanguage], [IDRecipeIngredientLanguage]);


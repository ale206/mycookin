CREATE TABLE [dbo].[RecipesIngredients] (
    [IDRecipeIngredient]            UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesIngredients_IDRecipeIngredient] DEFAULT (newid()) NOT NULL,
    [IDRecipe]                      UNIQUEIDENTIFIER NOT NULL,
    [IDIngredient]                  UNIQUEIDENTIFIER NOT NULL,
    [IsPrincipalIngredient]         BIT              CONSTRAINT [DF_RecipesIngredients_IsPrincipalIngredient] DEFAULT ((0)) NOT NULL,
    [QuantityNotStd]                NVARCHAR (150)   NULL,
    [IDQuantityNotStd]              INT              NULL,
    [Quantity]                      FLOAT (53)       NULL,
    [IDQuantityType]                INT              NOT NULL,
    [QuantityNotSpecified]          BIT              CONSTRAINT [DF_RecipesIngredients_QuantityNotSpecified] DEFAULT ((0)) NULL,
    [RecipeIngredientGroupNumber]   TINYINT          NOT NULL,
    [IDRecipeIngredientAlternative] UNIQUEIDENTIFIER NULL,
    [IngredientRelevance]           INT              NULL,
    CONSTRAINT [PK_RecipesIngredients] PRIMARY KEY CLUSTERED ([IDRecipeIngredient] ASC),
    CONSTRAINT [FK_RecipesIngredients_Ingredients] FOREIGN KEY ([IDIngredient]) REFERENCES [dbo].[Ingredients] ([IDIngredient]) ON UPDATE CASCADE,
    CONSTRAINT [FK_RecipesIngredients_IngredientsQuantityTypes] FOREIGN KEY ([IDQuantityType]) REFERENCES [dbo].[IngredientsQuantityTypes] ([IDIngredientQuantityType]) ON UPDATE CASCADE,
    CONSTRAINT [FK_RecipesIngredients_Recipes1] FOREIGN KEY ([IDRecipe]) REFERENCES [dbo].[Recipes] ([IDRecipe]) ON DELETE CASCADE ON UPDATE CASCADE
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [RecipeIngredientUnique]
    ON [dbo].[RecipesIngredients]([IDRecipe] ASC, [IDIngredient] ASC, [RecipeIngredientGroupNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_RecipesIngredients_9_269244014__K11_1_2_3_4_5_6_7_8_9_10_12]
    ON [dbo].[RecipesIngredients]([IDRecipeIngredientAlternative] ASC)
    INCLUDE([IDRecipeIngredient], [IDRecipe], [IDIngredient], [IsPrincipalIngredient], [QuantityNotStd], [IDQuantityNotStd], [Quantity], [IDQuantityType], [QuantityNotSpecified], [RecipeIngredientGroupNumber], [IngredientRelevance]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_RecipesIngredients_9_269244014__K3_K4_K2_K1]
    ON [dbo].[RecipesIngredients]([IDIngredient] ASC, [IsPrincipalIngredient] ASC, [IDRecipe] ASC, [IDRecipeIngredient] ASC);


GO
CREATE STATISTICS [_dta_stat_269244014_3_1]
    ON [dbo].[RecipesIngredients]([IDIngredient], [IDRecipeIngredient]);


GO
CREATE STATISTICS [_dta_stat_269244014_7_11]
    ON [dbo].[RecipesIngredients]([Quantity], [IDRecipeIngredientAlternative]);


GO
CREATE STATISTICS [_dta_stat_269244014_2_11]
    ON [dbo].[RecipesIngredients]([IDRecipe], [IDRecipeIngredientAlternative]);


GO
CREATE STATISTICS [_dta_stat_269244014_1_4_2]
    ON [dbo].[RecipesIngredients]([IDRecipeIngredient], [IsPrincipalIngredient], [IDRecipe]);


GO
CREATE STATISTICS [_dta_stat_269244014_1_2_3]
    ON [dbo].[RecipesIngredients]([IDRecipeIngredient], [IDRecipe], [IDIngredient]);


GO
CREATE STATISTICS [_dta_stat_269244014_11_1_7]
    ON [dbo].[RecipesIngredients]([IDRecipeIngredientAlternative], [IDRecipeIngredient], [Quantity]);


GO
CREATE STATISTICS [_dta_stat_269244014_10_2_11]
    ON [dbo].[RecipesIngredients]([RecipeIngredientGroupNumber], [IDRecipe], [IDRecipeIngredientAlternative]);


GO
CREATE STATISTICS [_dta_stat_269244014_1_2_10]
    ON [dbo].[RecipesIngredients]([IDRecipeIngredient], [IDRecipe], [RecipeIngredientGroupNumber]);


GO
CREATE STATISTICS [_dta_stat_269244014_10_7_11_2]
    ON [dbo].[RecipesIngredients]([RecipeIngredientGroupNumber], [Quantity], [IDRecipeIngredientAlternative], [IDRecipe]);


GO
CREATE STATISTICS [_dta_stat_269244014_11_1_2_10_7]
    ON [dbo].[RecipesIngredients]([IDRecipeIngredientAlternative], [IDRecipeIngredient], [IDRecipe], [RecipeIngredientGroupNumber], [Quantity]);


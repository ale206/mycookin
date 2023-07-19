CREATE TABLE [dbo].[RecipesLanguages] (
    [IDRecipeLanguage]            UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesLanguages_IDRecipeLanguages] DEFAULT (newid()) NOT NULL,
    [IDRecipe]                    UNIQUEIDENTIFIER NOT NULL,
    [IDLanguage]                  INT              NOT NULL,
    [RecipeName]                  NVARCHAR (250)   NOT NULL,
    [RecipeLanguageAutoTranslate] BIT              NOT NULL,
    [RecipeHistory]               NVARCHAR (MAX)   NULL,
    [RecipeHistoryDate]           DATETIME         NULL,
    [RecipeNote]                  NVARCHAR (MAX)   NULL,
    [RecipeSuggestion]            NVARCHAR (MAX)   NULL,
    [RecipeDisabled]              BIT              CONSTRAINT [DF_RecipesLanguages_RecipeDisabled] DEFAULT ((0)) NOT NULL,
    [GeoIDRegion]                 INT              NULL,
    [RecipeLanguageTags]          NVARCHAR (MAX)   NULL,
    [OriginalVersion]             BIT              NULL,
    [TranslatedBy]                UNIQUEIDENTIFIER NULL,
    [FriendlyId]                  NVARCHAR (500)   NULL,
    CONSTRAINT [PK_RecipesLanguages] PRIMARY KEY CLUSTERED ([IDRecipeLanguage] ASC),
    CONSTRAINT [FK_RecipesLanguages_Languages] FOREIGN KEY ([IDLanguage]) REFERENCES [dbo].[Languages] ([IDLanguage]) ON UPDATE CASCADE,
    CONSTRAINT [FK_RecipesLanguages_Recipes] FOREIGN KEY ([IDRecipe]) REFERENCES [dbo].[Recipes] ([IDRecipe]) ON DELETE CASCADE ON UPDATE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_IDRecipe_IDLanguage]
    ON [dbo].[RecipesLanguages]([IDRecipe] ASC, [IDLanguage] ASC, [GeoIDRegion] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RecipeName]
    ON [dbo].[RecipesLanguages]([RecipeName] ASC);


GO
CREATE STATISTICS [_dta_stat_486292792_4_1]
    ON [dbo].[RecipesLanguages]([RecipeName], [IDRecipeLanguage]);


GO
CREATE STATISTICS [_dta_stat_486292792_1_10]
    ON [dbo].[RecipesLanguages]([IDRecipeLanguage], [RecipeDisabled]);


GO
CREATE STATISTICS [_dta_stat_486292792_3_1]
    ON [dbo].[RecipesLanguages]([IDLanguage], [IDRecipeLanguage]);


GO
CREATE STATISTICS [_dta_stat_486292792_1_2_4]
    ON [dbo].[RecipesLanguages]([IDRecipeLanguage], [IDRecipe], [RecipeName]);


GO
CREATE STATISTICS [_dta_stat_486292792_2_1_3]
    ON [dbo].[RecipesLanguages]([IDRecipe], [IDRecipeLanguage], [IDLanguage]);


GO
CREATE STATISTICS [_dta_stat_486292792_2_3_10]
    ON [dbo].[RecipesLanguages]([IDRecipe], [IDLanguage], [RecipeDisabled]);


GO
CREATE STATISTICS [_dta_stat_486292792_3_2_1_10]
    ON [dbo].[RecipesLanguages]([IDLanguage], [IDRecipe], [IDRecipeLanguage], [RecipeDisabled]);


GO
CREATE NONCLUSTERED INDEX [IX_FriendlyIdUnique]
    ON [dbo].[RecipesLanguages]([FriendlyId] ASC);


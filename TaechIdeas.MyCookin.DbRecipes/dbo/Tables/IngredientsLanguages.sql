CREATE TABLE [dbo].[IngredientsLanguages] (
    [IDIngredientLanguage]  UNIQUEIDENTIFIER CONSTRAINT [DF_IngredientsLanguages_IDIngredientLanguage] DEFAULT (newid()) NOT NULL,
    [IDIngredient]          UNIQUEIDENTIFIER NOT NULL,
    [IDLanguage]            INT              NOT NULL,
    [IngredientSingular]    NVARCHAR (250)   NOT NULL,
    [IngredientPlural]      NVARCHAR (250)   NULL,
    [IngredientDescription] NVARCHAR (MAX)   NULL,
    [isAutoTranslate]       BIT              NOT NULL,
    [GeoIDRegion]           INT              NULL,
    [FriendlyId] NVARCHAR(500) NULL, 
    CONSTRAINT [PK_IngredientsLanguages] PRIMARY KEY CLUSTERED ([IDIngredientLanguage] ASC),
    CONSTRAINT [FK_IngredientsLanguages_Ingredients] FOREIGN KEY ([IDIngredient]) REFERENCES [dbo].[Ingredients] ([IDIngredient]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_IngredientsLanguages_Languages] FOREIGN KEY ([IDLanguage]) REFERENCES [dbo].[Languages] ([IDLanguage]) ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IDIngredient-IDLanguage]
    ON [dbo].[IngredientsLanguages]([IDIngredient] ASC, [IDLanguage] ASC, [GeoIDRegion] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IngredientSingular]
    ON [dbo].[IngredientsLanguages]([IngredientSingular] ASC)
    INCLUDE([IngredientPlural]);


GO
CREATE STATISTICS [_dta_stat_621245268_3_1]
    ON [dbo].[IngredientsLanguages]([IDLanguage], [IDIngredientLanguage]);


GO
CREATE STATISTICS [_dta_stat_621245268_1_2_4]
    ON [dbo].[IngredientsLanguages]([IDIngredientLanguage], [IDIngredient], [IngredientSingular]);


GO
CREATE STATISTICS [_dta_stat_621245268_7_2_3]
    ON [dbo].[IngredientsLanguages]([isAutoTranslate], [IDIngredient], [IDLanguage]);


GO
CREATE STATISTICS [_dta_stat_621245268_1_2_3]
    ON [dbo].[IngredientsLanguages]([IDIngredientLanguage], [IDIngredient], [IDLanguage]);


GO
CREATE STATISTICS [_dta_stat_621245268_1_4_3_2]
    ON [dbo].[IngredientsLanguages]([IDIngredientLanguage], [IngredientSingular], [IDLanguage], [IDIngredient]);


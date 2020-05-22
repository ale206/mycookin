CREATE TABLE [dbo].[IngredientsQuantityTypesLanguages] (
    [IDIngredientQuantityTypeLanguage]      INT            IDENTITY (1, 1) NOT NULL,
    [IDIngredientQuantityType]              INT            NOT NULL,
    [IDLanguage]                            INT            NOT NULL,
    [IngredientQuantityTypeSingular]        NVARCHAR (250) NOT NULL,
    [IngredientQuantityTypePlural]          NVARCHAR (250) NULL,
    [ConvertionRatio]                       FLOAT (53)     CONSTRAINT [DF_IngredientsQuantityTypesLanguages_ConvertionRatio] DEFAULT ((1)) NOT NULL,
    [IngredientQuantityTypeX1000Singular]   NVARCHAR (50)  NULL,
    [IngredientQuantityTypeX1000Plural]     NVARCHAR (50)  NULL,
    [IngredientQuantityTypeWordsShowBefore] NVARCHAR (50)  NULL,
    [IngredientQuantityTypeWordsShowAfter]  NVARCHAR (50)  NULL,
    CONSTRAINT [PK_IngredientsQuantityTypesLanguages] PRIMARY KEY CLUSTERED ([IDIngredientQuantityTypeLanguage] ASC),
    CONSTRAINT [FK_IngredientsQuantityTypesLanguages_IngredientsQuantityTypes] FOREIGN KEY ([IDIngredientQuantityType]) REFERENCES [dbo].[IngredientsQuantityTypes] ([IDIngredientQuantityType]) ON UPDATE CASCADE
);


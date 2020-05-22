CREATE TABLE [dbo].[Ingredients] (
    [IDIngredient]                  UNIQUEIDENTIFIER CONSTRAINT [DF_Ingredients_IDIndredient] DEFAULT (newid()) NOT NULL,
    [IDIngredientPreparationRecipe] UNIQUEIDENTIFIER NULL,
    [IDIngredientImage]             UNIQUEIDENTIFIER NULL,
    [AverageWeightOfOnePiece]       FLOAT (53)       NULL,
    [Kcal100gr]                     FLOAT (53)       CONSTRAINT [DF_Ingredients_Kcal100gr] DEFAULT ((0)) NULL,
    [grProteins]                    FLOAT (53)       NULL,
    [grFats]                        FLOAT (53)       NULL,
    [grCarbohydrates]               FLOAT (53)       NULL,
    [grAlcohol]                     FLOAT (53)       NULL,
    [mgCalcium]                     FLOAT (53)       NULL,
    [mgSodium]                      FLOAT (53)       NULL,
    [mgPhosphorus]                  FLOAT (53)       NULL,
    [mgPotassium]                   FLOAT (53)       NULL,
    [mgIron]                        FLOAT (53)       NULL,
    [mgMagnesium]                   FLOAT (53)       NULL,
    [mcgVitaminA]                   FLOAT (53)       NULL,
    [mgVitaminB1]                   FLOAT (53)       NULL,
    [mgVitaminB2]                   FLOAT (53)       NULL,
    [mcgVitaminB9]                  FLOAT (53)       NULL,
    [mcgVitaminB12]                 FLOAT (53)       NULL,
    [mgVitaminC]                    FLOAT (53)       NULL,
    [grSaturatedFat]                FLOAT (53)       NULL,
    [grMonounsaturredFat]           FLOAT (53)       NULL,
    [grPolyunsaturredFat]           FLOAT (53)       NULL,
    [mgCholesterol]                 FLOAT (53)       NULL,
    [mgPhytosterols]                FLOAT (53)       NULL,
    [mgOmega3]                      FLOAT (53)       NULL,
    [IsForBaby]                     BIT              NULL,
    [IsMeat]                        BIT              NULL,
    [IsFish]                        BIT              NULL,
    [IsVegetarian]                  BIT              CONSTRAINT [DF_Ingredients_IsVegetarian] DEFAULT ((0)) NOT NULL,
    [IsVegan]                       BIT              CONSTRAINT [DF_Ingredients_IsVegan] DEFAULT ((0)) NOT NULL,
    [IsGlutenFree]                  BIT              CONSTRAINT [DF_Ingredients_IsGlutenFree] DEFAULT ((0)) NOT NULL,
    [IsHotSpicy]                    BIT              NOT NULL,
    [Checked]                       BIT              NOT NULL,
    [IngredientCreatedBy]           UNIQUEIDENTIFIER NULL,
    [IngredientCreationDate]        SMALLDATETIME    NULL,
    [IngredientModifiedByUser]      UNIQUEIDENTIFIER NULL,
    [IngredientLastMod]             SMALLDATETIME    NULL,
    [IngredientEnabled]             BIT              NULL,
    [January]                       BIT              NOT NULL,
    [February]                      BIT              NOT NULL,
    [March]                         BIT              NOT NULL,
    [April]                         BIT              NOT NULL,
    [May]                           BIT              NOT NULL,
    [June]                          BIT              NOT NULL,
    [July]                          BIT              NOT NULL,
    [August]                        BIT              NOT NULL,
    [September]                     BIT              NOT NULL,
    [October]                       BIT              NOT NULL,
    [November]                      BIT              NOT NULL,
    [December]                      BIT              NOT NULL,
    [grDietaryFiber]                FLOAT (53)       NULL,
    [grStarch]                      FLOAT (53)       NULL,
    [grSugar]                       FLOAT (53)       NULL,
    CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED ([IDIngredient] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'http://www.valori-alimenti.com', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Ingredients', @level2type = N'COLUMN', @level2name = N'grProteins';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'http://www.valori-alimenti.com', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Ingredients', @level2type = N'COLUMN', @level2name = N'grFats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'http://www.valori-alimenti.com', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Ingredients', @level2type = N'COLUMN', @level2name = N'grCarbohydrates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'http://www.valori-alimenti.com', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Ingredients', @level2type = N'COLUMN', @level2name = N'grAlcohol';


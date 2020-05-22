CREATE TABLE [dbo].[IngredientsQuantityTypes] (
    [IDIngredientQuantityType] INT        IDENTITY (1, 1) NOT NULL,
    [isWeight]                 BIT        CONSTRAINT [DF_IngredientsQuantityTypes_isWeight] DEFAULT ((0)) NOT NULL,
    [isLiquid]                 BIT        CONSTRAINT [DF_IngredientsQuantityTypes_isLiquid] DEFAULT ((0)) NOT NULL,
    [isPiece]                  BIT        CONSTRAINT [DF_IngredientsQuantityTypes_isPiece] DEFAULT ((0)) NOT NULL,
    [isStandardQuantityType]   BIT        CONSTRAINT [DF_IngredientsQuantityTypes_isStandardQuantityType] DEFAULT ((0)) NOT NULL,
    [NoStdAvgWeight]           FLOAT (53) NULL,
    [EnabledToUser]            BIT        NOT NULL,
    [ShowInIngredientList]     BIT        NOT NULL,
    CONSTRAINT [PK_IngredientsQuantityTypes] PRIMARY KEY CLUSTERED ([IDIngredientQuantityType] ASC)
);


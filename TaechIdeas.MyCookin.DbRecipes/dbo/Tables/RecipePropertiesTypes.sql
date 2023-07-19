CREATE TABLE [dbo].[RecipePropertiesTypes] (
    [IDRecipePropertyType] INT IDENTITY (1, 1) NOT NULL,
    [isDishType]           BIT NOT NULL,
    [isCookingType]        BIT NOT NULL,
    [isColorType]          BIT NOT NULL,
    [isEatType]            BIT NOT NULL,
    [isUseType]            BIT NOT NULL,
    [isPeriodType]         BIT NOT NULL,
    [OrderPosition]        INT NOT NULL,
    [Enabled]              BIT NOT NULL,
    CONSTRAINT [PK_RecipePropertiesTypes] PRIMARY KEY CLUSTERED ([IDRecipePropertyType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primo, Secondo, Antipasto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecipePropertiesTypes', @level2type = N'COLUMN', @level2name = N'isDishType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cotto al Forno, Bollito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecipePropertiesTypes', @level2type = N'COLUMN', @level2name = N'isCookingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'General color of the dish presentation', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecipePropertiesTypes', @level2type = N'COLUMN', @level2name = N'isColorType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es: Finger Food, glass food', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecipePropertiesTypes', @level2type = N'COLUMN', @level2name = N'isEatType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es. For PicNic, for barbecue, for special event', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecipePropertiesTypes', @level2type = N'COLUMN', @level2name = N'isUseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cook in winter, cook for chrismas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecipePropertiesTypes', @level2type = N'COLUMN', @level2name = N'isPeriodType';


CREATE TABLE [dbo].[Recipes] (
    [IDRecipe]                   UNIQUEIDENTIFIER CONSTRAINT [DF_Recipes_IDRecipe] DEFAULT (newid()) NOT NULL,
    [IDRecipeFather]             UNIQUEIDENTIFIER NULL,
    [IDOwner]                    UNIQUEIDENTIFIER NULL,
    [NumberOfPerson]             INT              NULL,
    [PreparationTimeMinute]      INT              NULL,
    [CookingTimeMinute]          INT              NULL,
    [RecipeDifficulties]         INT              NULL,
    [IDRecipeImage]              UNIQUEIDENTIFIER NULL,
    [IDRecipeVideo]              UNIQUEIDENTIFIER NULL,
    [IDCity]                     INT              NULL,
    [CreationDate]               SMALLDATETIME    NULL,
    [LastUpdate]                 SMALLDATETIME    NULL,
    [UpdatedByUser]              UNIQUEIDENTIFIER NULL,
    [RecipeConsulted]            INT              CONSTRAINT [DF_Recipes_RecipeConsulted] DEFAULT ((0)) NOT NULL,
    [RecipeAvgRating]            FLOAT (53)       CONSTRAINT [DF_Recipes_RecipeRating] DEFAULT ((0)) NOT NULL,
    [isStarterRecipe]            BIT              NOT NULL,
    [DeletedOn]                  SMALLDATETIME    NULL,
    [BaseRecipe]                 BIT              NOT NULL,
    [RecipeEnabled]              BIT              NOT NULL,
    [Checked]                    BIT              NULL,
    [RecipeCompletePerc]         INT              NULL,
    [RecipePortionKcal]          FLOAT (53)       NULL,
    [RecipePortionProteins]      FLOAT (53)       NULL,
    [RecipePortionFats]          FLOAT (53)       NULL,
    [RecipePortionCarbohydrates] FLOAT (53)       NULL,
    [RecipePortionQta]           FLOAT (53)       NULL,
    [Vegetarian]                 BIT              NULL,
    [Vegan]                      BIT              NULL,
    [GlutenFree]                 BIT              NULL,
    [HotSpicy]                   BIT              NULL,
    [RecipePortionAlcohol]       FLOAT (53)       NULL,
    [Draft]                      BIT              NULL,
    [RecipeRated]                INT              NULL,
    CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED ([IDRecipe] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_RecipeFileds_ForFilter]
    ON [dbo].[Recipes]([RecipeEnabled] ASC, [IDRecipeFather] ASC, [RecipePortionKcal] ASC)
    INCLUDE([IDRecipe], [PreparationTimeMinute], [CookingTimeMinute], [RecipeAvgRating], [Vegetarian], [Vegan], [GlutenFree]);


GO
CREATE NONCLUSTERED INDEX [IX_RecipeOwner]
    ON [dbo].[Recipes]([IDOwner] ASC)
    INCLUDE([IDRecipe], [CreationDate]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_Recipes_9_1140199112__K19_K27_K8_K22_K1_K12_K15]
    ON [dbo].[Recipes]([RecipeEnabled] ASC, [Vegetarian] ASC, [IDRecipeImage] ASC, [RecipePortionKcal] ASC, [IDRecipe] ASC, [LastUpdate] ASC, [RecipeAvgRating] ASC);


GO
CREATE STATISTICS [_dta_stat_1140199112_11_1]
    ON [dbo].[Recipes]([CreationDate], [IDRecipe]);


GO
CREATE STATISTICS [_dta_stat_1140199112_3_19]
    ON [dbo].[Recipes]([IDOwner], [RecipeEnabled]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_17]
    ON [dbo].[Recipes]([IDRecipe], [DeletedOn]);


GO
CREATE STATISTICS [_dta_stat_1140199112_2_1_19]
    ON [dbo].[Recipes]([IDRecipeFather], [IDRecipe], [RecipeEnabled]);


GO
CREATE STATISTICS [_dta_stat_1140199112_22_27_19]
    ON [dbo].[Recipes]([RecipePortionKcal], [Vegetarian], [RecipeEnabled]);


GO
CREATE STATISTICS [_dta_stat_1140199112_20_2_22]
    ON [dbo].[Recipes]([Checked], [IDRecipeFather], [RecipePortionKcal]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_3_19]
    ON [dbo].[Recipes]([IDRecipe], [IDOwner], [RecipeEnabled]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_19_22_28]
    ON [dbo].[Recipes]([IDRecipe], [RecipeEnabled], [RecipePortionKcal], [Vegan]);


GO
CREATE STATISTICS [_dta_stat_1140199112_22_1_19_20]
    ON [dbo].[Recipes]([RecipePortionKcal], [IDRecipe], [RecipeEnabled], [Checked]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_19_27_8]
    ON [dbo].[Recipes]([IDRecipe], [RecipeEnabled], [Vegetarian], [IDRecipeImage]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_19_2_27]
    ON [dbo].[Recipes]([IDRecipe], [RecipeEnabled], [IDRecipeFather], [Vegetarian]);


GO
CREATE STATISTICS [_dta_stat_1140199112_19_20_2_22]
    ON [dbo].[Recipes]([RecipeEnabled], [Checked], [IDRecipeFather], [RecipePortionKcal]);


GO
CREATE STATISTICS [_dta_stat_1140199112_19_1_20_2]
    ON [dbo].[Recipes]([RecipeEnabled], [IDRecipe], [Checked], [IDRecipeFather]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_5_6_22]
    ON [dbo].[Recipes]([IDRecipe], [PreparationTimeMinute], [CookingTimeMinute], [RecipePortionKcal]);


GO
CREATE STATISTICS [_dta_stat_1140199112_15_20_2_22_19]
    ON [dbo].[Recipes]([RecipeAvgRating], [Checked], [IDRecipeFather], [RecipePortionKcal], [RecipeEnabled]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_15_20_2_22]
    ON [dbo].[Recipes]([IDRecipe], [RecipeAvgRating], [Checked], [IDRecipeFather], [RecipePortionKcal]);


GO
CREATE STATISTICS [_dta_stat_1140199112_22_1_19_5_6]
    ON [dbo].[Recipes]([RecipePortionKcal], [IDRecipe], [RecipeEnabled], [PreparationTimeMinute], [CookingTimeMinute]);


GO
CREATE STATISTICS [_dta_stat_1140199112_27_1_22_19_5_6]
    ON [dbo].[Recipes]([Vegetarian], [IDRecipe], [RecipePortionKcal], [RecipeEnabled], [PreparationTimeMinute], [CookingTimeMinute]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_20_2_22_19_15]
    ON [dbo].[Recipes]([IDRecipe], [Checked], [IDRecipeFather], [RecipePortionKcal], [RecipeEnabled], [RecipeAvgRating]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_15_22_28_27_29]
    ON [dbo].[Recipes]([IDRecipe], [RecipeAvgRating], [RecipePortionKcal], [Vegan], [Vegetarian], [GlutenFree]);


GO
CREATE STATISTICS [_dta_stat_1140199112_12_15_22_27_19_8]
    ON [dbo].[Recipes]([LastUpdate], [RecipeAvgRating], [RecipePortionKcal], [Vegetarian], [RecipeEnabled], [IDRecipeImage]);


GO
CREATE STATISTICS [_dta_stat_1140199112_27_19_8_22_1_12_15]
    ON [dbo].[Recipes]([Vegetarian], [RecipeEnabled], [IDRecipeImage], [RecipePortionKcal], [IDRecipe], [LastUpdate], [RecipeAvgRating]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_22_27_19_28_29_15]
    ON [dbo].[Recipes]([IDRecipe], [RecipePortionKcal], [Vegetarian], [RecipeEnabled], [Vegan], [GlutenFree], [RecipeAvgRating]);


GO
CREATE STATISTICS [_dta_stat_1140199112_19_2_27_22_1_5_6]
    ON [dbo].[Recipes]([RecipeEnabled], [IDRecipeFather], [Vegetarian], [RecipePortionKcal], [IDRecipe], [PreparationTimeMinute], [CookingTimeMinute]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_15_5_19_2_28_27_29]
    ON [dbo].[Recipes]([IDRecipe], [RecipeAvgRating], [PreparationTimeMinute], [RecipeEnabled], [IDRecipeFather], [Vegan], [Vegetarian], [GlutenFree]);


GO
CREATE STATISTICS [_dta_stat_1140199112_22_19_5_2_28_27_29_1]
    ON [dbo].[Recipes]([RecipePortionKcal], [RecipeEnabled], [PreparationTimeMinute], [IDRecipeFather], [Vegan], [Vegetarian], [GlutenFree], [IDRecipe]);


GO
CREATE STATISTICS [_dta_stat_1140199112_1_5_22_19_2_28_27_29_15]
    ON [dbo].[Recipes]([IDRecipe], [PreparationTimeMinute], [RecipePortionKcal], [RecipeEnabled], [IDRecipeFather], [Vegan], [Vegetarian], [GlutenFree], [RecipeAvgRating]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A recipe like pizza base, cream, maionese, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Recipes', @level2type = N'COLUMN', @level2name = N'BaseRecipe';


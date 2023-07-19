CREATE TABLE [dbo].[RecipesSteps] (
    [IDRecipeStep]      UNIQUEIDENTIFIER CONSTRAINT [DF_Table_1_RecipeStep] DEFAULT (newid()) NOT NULL,
    [IDRecipeLanguage]  UNIQUEIDENTIFIER NOT NULL,
    [StepGroup]         NVARCHAR (150)   NULL,
    [StepNumber]        INT              NOT NULL,
    [RecipeStep]        NVARCHAR (MAX)   NOT NULL,
    [StepTimeMinute]    INT              NULL,
    [IDRecipeStepImage] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_RecipesSteps] PRIMARY KEY CLUSTERED ([IDRecipeStep] ASC),
    CONSTRAINT [FK_RecipesSteps_RecipesLanguages] FOREIGN KEY ([IDRecipeLanguage]) REFERENCES [dbo].[RecipesLanguages] ([IDRecipeLanguage]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IDRecipeLanguage]
    ON [dbo].[RecipesSteps]([IDRecipeLanguage] ASC);


GO
CREATE STATISTICS [_dta_stat_1988202133_4_2]
    ON [dbo].[RecipesSteps]([StepNumber], [IDRecipeLanguage]);


GO
CREATE STATISTICS [_dta_stat_1988202133_1_4]
    ON [dbo].[RecipesSteps]([IDRecipeStep], [StepNumber]);


GO
CREATE STATISTICS [_dta_stat_1988202133_2_1_4]
    ON [dbo].[RecipesSteps]([IDRecipeLanguage], [IDRecipeStep], [StepNumber]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Group logicaly the step of recipe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RecipesSteps', @level2type = N'COLUMN', @level2name = N'StepGroup';


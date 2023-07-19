CREATE TABLE [dbo].[RecipePropertiesTypesLanguages] (
    [IDRecipePropertyTypeLanguage] INT            IDENTITY (1, 1) NOT NULL,
    [IDRecipePropertyType]         INT            NOT NULL,
    [IDLanguage]                   INT            NOT NULL,
    [RecipePropertyType]           NVARCHAR (100) NOT NULL,
    [RecipePropertyTypeToolTip]    NVARCHAR (150) NULL,
    CONSTRAINT [PK_RecipePropertiesTypesLanguages] PRIMARY KEY CLUSTERED ([IDRecipePropertyTypeLanguage] ASC),
    CONSTRAINT [FK_RecipePropertiesTypesLanguages_RecipePropertiesTypes] FOREIGN KEY ([IDRecipePropertyType]) REFERENCES [dbo].[RecipePropertiesTypes] ([IDRecipePropertyType]) ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Unique_IDRecipePropertyType_IDLanguage]
    ON [dbo].[RecipePropertiesTypesLanguages]([IDRecipePropertyType] ASC, [IDLanguage] ASC);


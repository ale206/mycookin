CREATE TABLE [dbo].[RecipePropertiesLanguages] (
    [IDRecipePropertyLanguage] INT            IDENTITY (1, 1) NOT NULL,
    [IDRecipeProperty]         INT            NOT NULL,
    [IDLanguage]               INT            NOT NULL,
    [RecipeProperty]           NVARCHAR (100) NOT NULL,
    [RecipePropertyToolTip]    NVARCHAR (150) NULL,
    CONSTRAINT [PK_RecipePropertiesLanguages] PRIMARY KEY CLUSTERED ([IDRecipePropertyLanguage] ASC),
    CONSTRAINT [FK_RecipePropertiesLanguages_RecipeProperties] FOREIGN KEY ([IDRecipeProperty]) REFERENCES [dbo].[RecipeProperties] ([IDRecipeProperty]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Unique_IDRecipeProperty_IDLanguage]
    ON [dbo].[RecipePropertiesLanguages]([IDRecipeProperty] ASC, [IDLanguage] ASC);


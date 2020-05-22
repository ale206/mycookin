CREATE TABLE [dbo].[RecipesPresentationsLanguages] (
    [IDRecipePresentationLanguage] UNIQUEIDENTIFIER CONSTRAINT [DF_RecipesPresentationsLanguages_IDRecipePresentationLanguage] DEFAULT (newid()) NOT NULL,
    [IDRecipePresentation]         UNIQUEIDENTIFIER NOT NULL,
    [IDLanguage]                   INT              NOT NULL,
    [RecipePresentationNote]       NVARCHAR (MAX)   NOT NULL,
    [isAutoTranslate]              BIT              NOT NULL,
    CONSTRAINT [PK_RecipesPresentationsLanguages] PRIMARY KEY CLUSTERED ([IDRecipePresentationLanguage] ASC),
    CONSTRAINT [FK_RecipesPresentationsLanguages_RecipesPresentations] FOREIGN KEY ([IDRecipePresentation]) REFERENCES [dbo].[RecipesPresentations] ([IDRecipePresentation]) ON DELETE CASCADE ON UPDATE CASCADE
);


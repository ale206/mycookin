CREATE TABLE [dbo].[RecipesLanguagesTags] (
    [IDRecipeLanguageTag]       INT            IDENTITY (1, 1) NOT NULL,
    [IDRecipeTag]               INT            NOT NULL,
    [IDLanguage]                INT            NOT NULL,
    [RecipeLanguageTag]         NVARCHAR (50)  NOT NULL,
    [RecipeLanguageSimilarTags] NVARCHAR (500) NULL,
    [RecipeLanguageReletadTags] NVARCHAR (500) NULL,
    CONSTRAINT [PK_RecipesLanguagesTags] PRIMARY KEY CLUSTERED ([IDRecipeLanguageTag] ASC),
    CONSTRAINT [FK_RecipesLanguagesTags_RecipesTags] FOREIGN KEY ([IDRecipeTag]) REFERENCES [dbo].[RecipesTags] ([IDRecipeTag]) ON DELETE CASCADE ON UPDATE CASCADE
);


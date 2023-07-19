CREATE TABLE [dbo].[RecipesTags] (
    [IDRecipeTag]  INT IDENTITY (1, 1) NOT NULL,
    [Enabled]      BIT NOT NULL,
    [MinThreshold] INT NULL,
    [MaxThreshold] INT NULL,
    CONSTRAINT [PK_RecipesTags] PRIMARY KEY CLUSTERED ([IDRecipeTag] ASC)
);


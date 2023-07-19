CREATE TABLE [dbo].[RecipeProperties] (
    [IDRecipeProperty]     INT IDENTITY (1, 1) NOT NULL,
    [IDRecipePropertyType] INT NOT NULL,
    [OrderPosition]        INT NOT NULL,
    [Enabled]              BIT NOT NULL,
    CONSTRAINT [PK_RecipeProperties] PRIMARY KEY CLUSTERED ([IDRecipeProperty] ASC),
    CONSTRAINT [FK_RecipeProperties_RecipePropertiesTypes] FOREIGN KEY ([IDRecipePropertyType]) REFERENCES [dbo].[RecipePropertiesTypes] ([IDRecipePropertyType]) ON UPDATE CASCADE
);


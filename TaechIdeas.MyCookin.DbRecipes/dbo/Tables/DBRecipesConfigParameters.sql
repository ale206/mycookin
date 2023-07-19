CREATE TABLE [dbo].[DBRecipesConfigParameters] (
    [IDDBRecipeConfigParameter]    INT            IDENTITY (1, 1) NOT NULL,
    [DBRecipeConfigParameterName]  NVARCHAR (50)  NOT NULL,
    [DBRecipeConfigParameterValue] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_DBRecipesConfigParameters] PRIMARY KEY CLUSTERED ([IDDBRecipeConfigParameter] ASC)
);


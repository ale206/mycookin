CREATE TABLE [dbo].[RecipesFeedbacks] (
    [IDRecipeFeedback] UNIQUEIDENTIFIER NOT NULL,
    [IDRecipe]         UNIQUEIDENTIFIER NOT NULL,
    [IDUser]           UNIQUEIDENTIFIER NOT NULL,
    [IDFeedbackType]   INT              NOT NULL,
    [FeedbackText]     NVARCHAR (550)   NULL,
    [FeedbackDate]     DATETIME         NOT NULL,
    CONSTRAINT [PK_RecipesFeedbacks] PRIMARY KEY CLUSTERED ([IDRecipeFeedback] ASC) WITH (FILLFACTOR = 80),
    CONSTRAINT [FK_RecipesFeedbacks_Recipes] FOREIGN KEY ([IDRecipe]) REFERENCES [dbo].[Recipes] ([IDRecipe]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IDRecipeIDUser]
    ON [dbo].[RecipesFeedbacks]([IDRecipe] ASC, [IDUser] ASC, [IDFeedbackType] ASC) WITH (FILLFACTOR = 80);


GO
CREATE NONCLUSTERED INDEX [IX_IDRecipe_IDFeedbackType]
    ON [dbo].[RecipesFeedbacks]([IDRecipe] ASC, [IDFeedbackType] ASC) WITH (FILLFACTOR = 80);


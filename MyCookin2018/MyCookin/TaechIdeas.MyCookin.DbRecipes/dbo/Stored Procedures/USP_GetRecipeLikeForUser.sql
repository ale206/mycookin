
-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,20/05/2014,>
-- Description:	<Description,Get Recipe Like for user,>
-- =============================================
create PROCEDURE [dbo].[USP_GetRecipeLikeForUser]
	@IDRecipe uniqueidentifier,
	@IDUser uniqueidentifier
	
AS

SELECT TOP 1 [IDRecipeFeedback]
      ,[IDRecipe]
      ,[IDUser]
      ,[IDFeedbackType]
      ,[FeedbackText]
      ,[FeedbackDate]
  FROM [DBRecipes].[dbo].[RecipesFeedbacks]
  WHERE IDRecipe = @IDRecipe 
	AND IDUser = @IDUser
	AND IDFeedbackType = 1











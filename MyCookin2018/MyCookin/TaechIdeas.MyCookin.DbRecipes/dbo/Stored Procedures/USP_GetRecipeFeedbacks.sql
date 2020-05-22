
-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,20/05/2014,>
-- Description:	<Description,Get Recipe Comments,>
-- =============================================
Create PROCEDURE [dbo].[USP_GetRecipeFeedbacks]
	@IDRecipe uniqueidentifier,
	@IDFeedbackType INT,
	@OffsetRows INT, 
	@FetchRows INT
	
AS

SELECT [IDRecipeFeedback]
		  ,[IDRecipe]
		  ,[IDUser]
		  ,[IDFeedbackType]
		  ,[FeedbackText]
		  ,[FeedbackDate]
	FROM [DBRecipes].[dbo].[RecipesFeedbacks]
	WHERE IDRecipe = @IDRecipe 
		AND IDFeedbackType = @IDFeedbackType
	ORDER BY FeedbackDate
	OFFSET @OffsetRows ROWS
	FETCH NEXT @FetchRows ROWS ONLY	











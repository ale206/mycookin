-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <03/11/2013,,>
-- Last Edit:   <,,>
-- Description:	<Most Shared Recipes,,>

-- Allowed Types
-- RC_RecipeSharedOnFacebookFromShowRecipe = 404,
-- RC_RecipeSharedOnTwitterFromShowRecipe = 405,
-- RC_RecipeSharedOnOwnWallFromShowRecipe = 406,
-- RC_RecipeSharedOnFacebookFromWall = 407,
-- RC_RecipeSharedOnTwitterFromWall = 408,
-- RC_RecipeSharedOnOwnWallFromWall = 409
-- =============================================
CREATE PROCEDURE [dbo].[USP_Stats_MostSharedRecipes]
	@StartDate datetime,
	@EndDate datetime,
	@ActionType int		
		
AS
   
   --AGGIUNGERE JOIN PER PRENDERE IL NOME DELLA RICETTA

  SELECT TOP 10 S.IDRelatedObject, COUNT(S.IDRelatedObject) AS RecipeSharedNumber
  FROM [DBStatistics].[dbo].[UsersActionsStatistics] S
	--INNER JOIN [DBUsersProfile].[dbo].[Users] U ON U.IDUser = S.IDRelatedObject
  WHERE ActionType = @ActionType
  AND DateAction BETWEEN @StartDate AND @EndDate
  GROUP BY IDRelatedObject --, U.Name, U.Surname, U.eMail
  ORDER BY RecipeSharedNumber DESC
	
		

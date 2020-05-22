-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <03/11/2013,,>
-- Last Edit:   <,,>
-- Description:	<Social Stats By Type,,>

--USER TYPES
--US_Login = 100,
--US_Logout = 101,

-- SOCIAL TYPES
-- SC_LoginThroughFacebook = 201,
-- SC_LoginThroughGoogle = 202,
-- SC_LoginThroughTwitter = 203,
-- SC_NewRegistrationThroughGoogle = 204,
-- SC_NewRegistrationThroughFacebook = 205,
-- SC_NewRegistrationThroughTwitter = 206,
-- SC_ActionSharedOnFacebook = 207,
-- SC_ActionSharedOnTwitter = 208,
-- SC_ActionSharedOnGoogle = 209,
-- SC_Like = 210,
-- SC_DontLikeMore = 211,
-- SC_SocialFriendsRetrieved = 212,
-- SC_ContactFriendsMemorized = 231,
-- SC_NewActionOnUserBoard = 232,
-- SC_NewComment = 233,
-- SC_NewPersonalMessage = 234,
-- SC_NewPostOnFriendUserBoard = 235,
-- SC_LikeForComment = 236,
-- SC_LikeForNewFollower = 237,
-- SC_LikeForNewIngredient = 238,
-- SC_LikeForNewRecipe = 239,
-- SC_LikeForPersonalMessage = 240,
-- SC_LikeForPostOnFriendUserBoard = 241,
-- SC_LikeForProfileUpdate = 242,
-- =============================================
CREATE PROCEDURE [dbo].[USP_Stats_SocialStatsByType]
	@StartDate datetime,
	@EndDate datetime,
	@ActionType int			--ONE OF: 202 (Google), 201 (Facebook), ... - See MyStatistics.aspx.cs
		
AS
   
  SELECT TOP 10 S.IDUser, U.Name, U.Surname, U.eMail, COUNT(S.IDUser) AS NumberOfResults
  FROM [DBStatistics].[dbo].[UsersActionsStatistics] S
	INNER JOIN [DBUsersProfile].[dbo].[Users] U ON U.IDUser = S.IDUser
  WHERE ActionType = @ActionType
  AND DateAction BETWEEN @StartDate AND @EndDate
  GROUP BY S.IDUser, U.Name, U.Surname, U.eMail
  ORDER BY NumberOfResults DESC
	
		

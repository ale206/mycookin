-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <03/11/2013,,>
-- Last Edit:   <,,>
-- Description:	<Most Followed Users,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_Stats_MostFollowedUsers]
		
AS
   
	SELECT [IDUserFollowed], count ([IDUserFollowed]) AS NumberOfFollower   
	FROM [DBUsersProfile].[dbo].[UsersFollowers]
	GROUP BY [IDUserFollowed]
	ORDER BY NumberOfFollower DESC
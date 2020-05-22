-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <03/11/2013,,>
-- Last Edit:   <,,>
-- Description:	<Most Following Users,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_Stats_MostFollowingUsers]
		
AS
   
  SELECT [IDUser], count ([IDUser]) AS NumberOfFollowing  
  FROM [DBUsersProfile].[dbo].[UsersFollowers]
  GROUP BY [IDUser]
  ORDER BY NumberOfFollowing DESC
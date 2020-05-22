-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <03/11/2013,,>
-- Last Edit:   <,,>
-- Description:	<Most Viewed Profile,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_Stats_MostViewedProfile]
	@StartDate datetime,
	@EndDate datetime		
		
AS
   
  SELECT TOP 10 S.IDRelatedObject, U.Name, U.Surname, U.eMail, COUNT(S.IDRelatedObject) AS ProfileViewedNumber
  FROM [DBStatistics].[dbo].[UsersActionsStatistics] S
	INNER JOIN [DBUsersProfile].[dbo].[Users] U ON U.IDUser = S.IDRelatedObject
  WHERE ActionType = 102
  AND DateAction BETWEEN @StartDate AND @EndDate
  GROUP BY IDRelatedObject, U.Name, U.Surname, U.eMail
  ORDER BY ProfileViewedNumber DESC
	
		

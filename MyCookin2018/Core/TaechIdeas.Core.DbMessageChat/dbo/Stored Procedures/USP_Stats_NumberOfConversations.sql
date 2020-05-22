-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <03/11/2013,,>
-- Last Edit:   <,,>
-- Description:	<Get Number of conversations,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_Stats_NumberOfConversations]
	@StartDate datetime,
	@EndDate datetime
		
AS
   
	SELECT COUNT(DISTINCT IDConversation) 
	FROM [DBMessageChat].[dbo].[UsersConversations]
	WHERE CreatedOn BETWEEN @StartDate AND @EndDate
	
		

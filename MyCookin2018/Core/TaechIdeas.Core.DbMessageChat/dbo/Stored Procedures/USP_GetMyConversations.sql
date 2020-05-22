-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <01/06/2013,,>
-- Last Edit:	<15/09/2013,,>
-- Description:	<Get all my conversations>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMyConversations]	
	@me uniqueidentifier				
		
AS

BEGIN
SELECT IDUserConversation,IDConversation, IDUser, Friend, max(SentOn) AS LastDateMessage FROM
(
	SELECT TOP (100) PERCENT U.IDUserConversation, U.IDConversation, U.IDUser, MR.SentOn,
           MR.IDUserSender AS Friend
	FROM   Messages M INNER JOIN
				MessagesRecipients MR ON M.IDMessage = MR.IDMessage INNER JOIN
                UsersConversations U  ON MR.IDUserConversation = U.IDUserConversation
	WHERE  (MR.DeletedOn IS NULL) 
				AND (U.IDUser = @me)
				AND (U.ArchivedOn IS NULL)
  UNION

	SELECT TOP (100) PERCENT U.IDUserConversation, U.IDConversation, U.IDUser, MR.SentOn,
           MR.IDUserRecipient  AS Friend
	FROM   Messages M INNER JOIN
                MessagesRecipients MR ON M.IDMessage = MR.IDMessage INNER JOIN
                UsersConversations U ON MR.IDUserConversation = U.IDUserConversation
	WHERE  (MR.DeletedOn IS NULL) 
				AND (U.IDUser = @me)
				AND (U.ArchivedOn IS NULL)
	
) AS T

WHERE Friend<> @me
group by  IDUserConversation,IDConversation, IDUser, Friend
order by max(SentOn) desc

END

-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <15/09/2013,,>
-- Last Edit:   <>
-- Description:	<Count number of messages to read,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CountMessagesNotRead]
	@IDUserConversationOwner uniqueidentifier
			
AS

	SELECT  COUNT(MR.IDMessageRecipient) AS MessagesNumber				
	FROM	Messages M 
		INNER JOIN MessagesRecipients MR
			ON MR.IDMessage = M.IDMessage
		INNER JOIN UsersConversations U
			ON MR.IDUserConversation = U.IDUserConversation
	WHERE	
			(MR.IDUserSender = @IDUserConversationOwner OR MR.IDUserRecipient = @IDUserConversationOwner)
		AND MR.DeletedOn IS NULL
		AND MR.ViewedOn IS NULL
		AND U.IDUser = @IDUserConversationOwner			--My Messages
		AND U.ArchivedOn IS NULL
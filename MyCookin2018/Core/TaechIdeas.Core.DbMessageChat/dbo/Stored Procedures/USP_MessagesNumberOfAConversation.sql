-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <22/08/2013,,>
-- Description:	<Get the Number of Messages Of A Conversation between two people>
-- =============================================
CREATE PROCEDURE [dbo].[USP_MessagesNumberOfAConversation]	
	@IDUserConversationOwner uniqueidentifier,
	@IDConversation uniqueidentifier				
		
AS

BEGIN

	SELECT	Count(M.IDMessage) AS MessagesNumber
	FROM	Messages M 
				INNER JOIN MessagesRecipients MR
					ON MR.IDMessage = M.IDMessage
				INNER JOIN UsersConversations U
					ON MR.IDUserConversation = U.IDUserConversation

	WHERE	M.IDMessageType = 1													
				AND (MR.IDUserSender = @IDUserConversationOwner OR MR.IDUserRecipient = @IDUserConversationOwner)
				AND MR.DeletedOn IS NULL
				AND U.IDUser = @IDUserConversationOwner												
				AND U.IDConversation = @IDConversation							
				AND U.ArchivedOn IS NULL
		
END
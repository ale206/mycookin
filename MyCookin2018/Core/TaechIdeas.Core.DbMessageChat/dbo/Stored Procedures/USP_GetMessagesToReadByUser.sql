-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <25/08/2013>
-- Last Edit: <,,>
-- Description:	<Get Messages To Read>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMessagesToReadByUser]	
	@IDUserConversationOwner uniqueidentifier,
	@IDUserSender uniqueidentifier	
		
AS

BEGIN

	SELECT  DISTINCT M.IDMessage, M.[Message],				--messaggio
					 MR.IDUserSender, MR.SentOn
	FROM	Messages M 
				INNER JOIN MessagesRecipients MR
					ON MR.IDMessage = M.IDMessage
				INNER JOIN UsersConversations U
					ON MR.IDUserConversation = U.IDUserConversation
	WHERE	--M.IDMessageType = 1	AND				--tipo del messaggio, messaggio o chat
				 (MR.IDUserSender = @IDUserConversationOwner OR MR.IDUserRecipient = @IDUserConversationOwner)
				AND MR.DeletedOn IS NULL
				AND MR.ViewedOn IS NULL
				AND U.IDUser = @IDUserConversationOwner			--i messaggi sono i miei
				AND U.ArchivedOn IS NULL
				AND MR.IDUserSender = @IDUserSender				--solo di questo utente
		
END
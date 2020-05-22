-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/08/2013,,>
-- Description:	<Get Messages Of A Conversation between two people, with PAGING>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMessagesOfAConversationPaged]	
	@IDUserConversationOwner uniqueidentifier,
	@IDConversation uniqueidentifier,	--se passiamo questa id possiamo eliminare la prima qry
	@Offset int,						--numero della pagina da visualizzare
	@PageSize int						--dimensione della pagina
		
AS

BEGIN
	--QRY PER ESTRARRE TUTTE LE INFORMAZIONI
	SELECT	M.IDMessage,				--superfluo, per eventuali azioni sul singolo messaggio
			M.[Message],				--messaggio
			MR.IDMessageRecipient,		--questo servirà per cancellare o segnare come visto il messaggio
			MR.IDUserConversation,		--questo servirà per continuare la conversazione o per archiviare la conversazione. cambia in base al "proprietario"
			MR.IDUserSender,			--questo per estrarre le informazioni di chi ha inviato il messaggio
			MR.SentOn,					--data di invio del messaggio
			U.IDConversation,			--id della conversazione unico per i due partecipanti
			U.CreatedOn					--info aggiuntiva, superflua
	FROM	Messages M 
				INNER JOIN MessagesRecipients MR
					ON MR.IDMessage = M.IDMessage
				INNER JOIN UsersConversations U
					ON MR.IDUserConversation = U.IDUserConversation
	WHERE	M.IDMessageType = 1													--tipo del messaggio, messaggio o chat
				AND (MR.IDUserSender = @IDUserConversationOwner OR MR.IDUserRecipient = @IDUserConversationOwner)
				AND MR.DeletedOn IS NULL
				AND U.IDUser = @IDUserConversationOwner												--i messaggi sono i miei
				AND U.IDConversation = @IDConversation							--conversazione tra me e sav
				AND U.ArchivedOn IS NULL

	ORDER BY MR.SentOn DESC

	OFFSET		@Offset		ROWS
	FETCH NEXT	@PageSize	ROWS ONLY
		
END
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <19/05/2013,,>
-- Description:	<Get Messages Of A Conversation between two people>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMessagesOfAConversation]	
	@IDUserConversationOwner uniqueidentifier,
	--@friend uniqueidentifier
	@IDConversation uniqueidentifier	--se passiamo questa id possiamo eliminare la prima qry						
		
AS

BEGIN

	--QRY PER ESTRARRE L'ID DELLA CONVERSAZIONE TRA DUE UTENTI
	--DECLARE @IDConversation uniqueidentifier

	--SELECT	DISTINCT  @IDConversation = U.IDConversation
	--FROM	UsersConversations U INNER JOIN MessagesRecipients MR
	--			ON MR.IDUserConversation = U.IDUserConversation
	--WHERE		(IDUserRecipient = 'E78330F7-DB37-4A22-8ED1-DBB80AB9E944' AND IDUserSender = '8AA259A3-B131-4C48-B065-B10851C0765B')
	--		OR	(IDUserRecipient = '8AA259A3-B131-4C48-B065-B10851C0765B' AND IDUserSender = 'E78330F7-DB37-4A22-8ED1-DBB80AB9E944')

	--QRY PER ESTRARRE TUTTE LE INFORMAZIONI
	SELECT	M.IDMessage,				--superfluo, per eventuali azioni sul singolo messaggio
			--M.IDMessageType,			--superfluo, serve solo per filtrare
			M.[Message],				--messaggio
		
			MR.IDMessageRecipient,		--questo servirà per cancellare o segnare come visto il messaggio
			MR.IDUserConversation,		--questo servirà per continuare la conversazione o per archiviare la conversazione. cambia in base al "proprietario"
			MR.IDUserSender,			--questo per estrarre le informazioni di chi ha inviato il messaggio
			--MR.IDUserRecipient,		--superfluo (saremo noi che visualizziamo il messaggio)
			MR.SentOn,					--data di invio del messaggio
			--MR.ViewedOn,				-- superfluo, ottenere questa info con un'altra qry
			--MR.DeletedOn,				-- superfluo, serve solo per filtrare

			--U.IDUserConversation,		--superfluo, lo abbiamo preso prima 
			U.IDConversation,			--id della conversazione unico per i due partecipanti
			U.CreatedOn					--info aggiuntiva, superflua
			--U.ArchivedOn				--superfluo, serve solo per filtrare

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

	ORDER BY MR.SentOn ASC
		
END
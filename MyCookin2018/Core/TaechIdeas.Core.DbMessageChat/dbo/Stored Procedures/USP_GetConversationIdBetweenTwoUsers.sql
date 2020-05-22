-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <21/05/2013,,>
-- Description:	<Get IDConversation between two people>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetConversationIdBetweenTwoUsers]	
	@user1 uniqueidentifier,
	@user2 uniqueidentifier					
		
AS

BEGIN

	SELECT DISTINCT U.IDConversation
	FROM MessagesRecipients MR INNER JOIN UsersConversations U
		  ON (MR.IDUserConversation = U.IDUserConversation)
	WHERE    (MR.IDUserSender = @user1 AND MR.IDUserRecipient = @user2)
		  OR (MR.IDUserSender = @user2 AND MR.IDUserRecipient = @user1)
		
END
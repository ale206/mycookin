-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <19/05/2013,,>
-- Description:	<Get Users Of A Conversation>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetUsersOfAConversation]	
	@IDConversation uniqueidentifier
		
AS

BEGIN	
	SELECT    IDUserConversation, IDConversation, IDUser, CreatedOn, ArchivedOn
		FROM  UsersConversations
		WHERE IDConversation = @IDConversation			
END
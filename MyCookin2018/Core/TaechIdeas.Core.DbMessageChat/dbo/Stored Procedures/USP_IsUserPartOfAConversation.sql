-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <19/05/2013,,>
-- Description:	<Is a User part of a Conversation?>
-- =============================================
CREATE PROCEDURE [dbo].[USP_IsUserPartOfAConversation]	
	@IDUser uniqueidentifier,
	@IDConversation uniqueidentifier
		
AS

BEGIN

	SELECT    IDUserConversation, IDConversation, IDUser, CreatedOn, ArchivedOn
		FROM  UsersConversations
		WHERE IDUser = @IDUser AND IDConversation = @IDConversation AND ArchivedOn IS NULL

END
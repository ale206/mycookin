-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <19/05/2013,,>
-- Description:	<Get Message Info By ID>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetMessageInfoByID]	
	@IDMessage uniqueidentifier
		
AS

BEGIN

	SELECT    IDMessage, IDMessageType, [Message]
		FROM  Messages
		WHERE IDMessage = @IDMessage
	
END
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <11/05/2013,,>
-- Description:	<Get TypeOfMessage Informations according to ID>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetTypeOfMessageInfoByID]	
	@IDMessageType int
		
AS

BEGIN	
	SELECT IDMessageType, MessageType, MessageMaxLength, MessageTypeEnabled
		FROM MessagesTypes
		WHERE IDMessageType = @IDMessageType
END

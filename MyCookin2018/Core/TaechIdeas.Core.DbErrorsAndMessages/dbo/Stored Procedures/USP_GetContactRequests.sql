-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <19/05/2013,,>
-- Description:	<Get Contact Requests Messages>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetContactRequests]	
	@IDContactRequestType int,									--DA IMPLEMENTARE !
	@JustNotClosedRequests bit
		
AS

BEGIN	

	IF @JustNotClosedRequests = 1
		BEGIN

			SELECT  CR.IDContactRequest, CR.IDLanguage, CR.FirstName, CR.LastName, CR.Email, 
					CR.RequestText, CR.RequestDate, CR.IpAddress, CR.IDContactRequestType   
				FROM  ContactRequests CR
					INNER JOIN ContactRequestsType CRT
							ON (CR.IDContactRequestType = CRT.IDContactRequestType)
				WHERE CR.IsRequestClosed = 0
				ORDER BY CR.RequestDate DESC
		
		END
	ELSE
		BEGIN
			SELECT  CR.IDContactRequest, CR.IDLanguage, CR.IDContactRequestType, CR.FirstName, CR.LastName, CR.Email, 
					CR.RequestText, CR.PrivacyAccept, CR.RequestDate, CR.IpAddress, CRT.RequestType   
				FROM  ContactRequests CR
					INNER JOIN ContactRequestsType CRT
							ON (CR.IDContactRequestType = CRT.IDContactRequestType)
				WHERE CR.IsRequestClosed = 0
				ORDER BY CR.RequestDate DESC
		END		
END
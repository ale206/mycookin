
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <27/10/2013,,>
-- Description:	<,,Get AuditEvent By ObjectType>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetAuditEventByObjectType]
	@NumberOfResults int,
	@ObjectType nvarchar(100)
			
AS

IF(@ObjectType = 'Photo')
	BEGIN
		--SELECT TOP (@NumberOfResults)	
		--	A.IDAuditEvent, A.AuditEventMessage, A.ObjectID, A.ObjectType, A.ObjectTxtInfo, A.AuditEventLevel, 
		--	A.EventInsertedOn, A.EventUpdatedOn, A.IDEventUpdatedBy, A.AuditEventIsOpen 
		--FROM [DBAudit].[dbo].[AuditEvent] A
		--	INNER JOIN [DBMedia].[dbo].[Media] M ON (A.ObjectID = M.IDMedia)
		--WHERE A.ObjectType = @ObjectType AND A.AuditEventIsOpen = 1 AND M.MediaDeletedOn IS NULL
		--ORDER BY EventInsertedOn ASC

		SELECT TOP (@NumberOfResults)	
			A.IDAuditEvent, A.AuditEventMessage, A.ObjectID, A.ObjectType, A.ObjectTxtInfo, A.AuditEventLevel, 
			A.EventInsertedOn, A.EventUpdatedOn, A.IDEventUpdatedBy, A.AuditEventIsOpen
			,M.IDMediaOwner
			,U.UserName
		FROM [DBAudit].[dbo].[AuditEvent] A
			INNER JOIN [DBMedia].[dbo].[Media] M ON (A.ObjectID = M.IDMedia)
			INNER JOIN [DBUsersProfile].[dbo].[Users] U ON U.IDUser = M.IDMediaOwner
		WHERE A.ObjectType = @ObjectType AND A.AuditEventIsOpen = 1 AND M.MediaDeletedOn IS NULL
		ORDER BY EventInsertedOn ASC

	END
ELSE
	BEGIN
		SELECT TOP (@NumberOfResults) IDAuditEvent, AuditEventMessage, ObjectID, ObjectType, ObjectTxtInfo, AuditEventLevel, EventInsertedOn, EventUpdatedOn, IDEventUpdatedBy, AuditEventIsOpen FROM dbo.AuditEvent
			WHERE ObjectType = @ObjectType AND AuditEventIsOpen = 1
			ORDER BY EventInsertedOn ASC
	END


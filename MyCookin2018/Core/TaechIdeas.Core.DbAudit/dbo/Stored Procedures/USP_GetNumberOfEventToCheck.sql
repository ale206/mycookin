
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <27/10/2013,,>
-- Description:	<,,Get Number of Event To Check>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetNumberOfEventToCheck]
	@ObjectType nvarchar(100)
			
AS

IF(@ObjectType = 'Photo')
	BEGIN
		SELECT COUNT(*) AS EventsToCheck 
		FROM [DBAudit].[dbo].[AuditEvent] A 
		INNER JOIN [DBMedia].[dbo].[Media] M ON (A.ObjectID = M.IDMedia)
		WHERE A.ObjectType = @ObjectType AND AuditEventIsOpen = 1 AND M.MediaDeletedOn IS NULL
	END
ELSE
	BEGIN
		SELECT COUNT(*) AS EventsToCheck FROM AuditEvent WHERE AuditEventIsOpen = 1 AND ObjectType = @ObjectType
	END


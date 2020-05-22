-- =============================================
-- Author:		<Author, Alessio Di Salvo>
-- Create date: <Create Date, 25/01/2016>
-- Description:	<GetAutoAuditConfigInfoByObjectType>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetAutoAuditConfigInfoByObjectType] 
@ObjectType nvarchar(150) 

AS
BEGIN
	
        SELECT IDAutoAuditConfig, ObjectType, AuditEventLevel, EnableAutoAudit
                     FROM AutoAuditConfig
                     WHERE(ObjectType = @ObjectType)
                   
END



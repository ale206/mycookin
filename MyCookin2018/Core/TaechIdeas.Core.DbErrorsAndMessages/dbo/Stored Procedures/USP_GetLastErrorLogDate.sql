-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <01/02/2016,,>
-- Description:	<Get last error log date>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetLastErrorLogDate]
	 
	@FileOrigin nvarchar(500),
	@ErrorMessageCode nvarchar(100)
		
AS

BEGIN
	SELECT     MAX(DateError) AS LastErrorLogDate
	FROM         ErrorsLogs
	WHERE     (FileOrigin = @FileOrigin) AND (ErrorMessageCode = @ErrorMessageCode)
END

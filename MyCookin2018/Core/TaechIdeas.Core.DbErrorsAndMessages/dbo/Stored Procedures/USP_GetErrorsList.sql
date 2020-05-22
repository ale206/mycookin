-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <01/02/2016,,>
-- Description:	<Geta list of errors>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetErrorsList]
		
AS

BEGIN
	SELECT DISTINCT ErrorMessage, Count(ErrorMessage) AS NumberOfEveniences, ErrorSeverity, FileOrigin, ErrorLine, 
					ErrorMessageCode, isStoredProcedureError, isTriggerError, isApplicationError
	FROM ErrorsLogs
	GROUP BY ErrorMessage, ErrorSeverity, FileOrigin, ErrorLine, ErrorMessageCode, isStoredProcedureError, 
	isTriggerError, isApplicationError
	ORDER BY NumberOfEveniences DESC
END

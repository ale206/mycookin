-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <26/04/2013,,>
-- Description:	<Count number of events according to IDUser and ActionType>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CountByIDUserAndActionType]	
	@IDUser uniqueidentifier,
	@ActionType int
		
AS

BEGIN	
	SELECT COUNT(1) AS NumberOfEntities FROM UsersActionsStatistics
	WHERE IDUser = @IDUser AND ActionType = @ActionType
END

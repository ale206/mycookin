-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <26/04/2013,,>
-- Description:	<Count number of events according to IDRelatedObject and ActionType 
--				(Ex.: number of userprofile visited)>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CountByIDRelatedObjectAndActionType]	
	@IDRelatedObject uniqueidentifier,
	@ActionType int
		
AS

BEGIN	
	SELECT COUNT(1) AS NumberOfEntities FROM UsersActionsStatistics
	WHERE IDRelatedObject = @IDRelatedObject AND ActionType = @ActionType
END

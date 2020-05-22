-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <04/05/2013,,>
-- Last Edit:   <,,>
-- Description:	<Check if a Notification is Enabled for a user,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_IsNotificationEnabled]
	@IDUser uniqueidentifier,
	@IDUserNotificationType int,
	@IDLanguage int
		
AS

SELECT * FROM vGetUsersNotificationsByIDUserAndIDLanguage
			WHERE     IDUser = @IDUser 
			      AND IDUserNotificationType = @IDUserNotificationType	
			      AND IDLanguage = @IDLanguage
	

		
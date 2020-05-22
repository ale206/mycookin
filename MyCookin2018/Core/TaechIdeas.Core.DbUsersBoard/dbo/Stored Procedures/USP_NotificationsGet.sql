
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <28/04/2013,,>
-- Last Edit:   <05/09/2013,,>
-- Description:	<Get Notifications, If IDUserActionType is not selected get all notifications to read (if there are)
--				 else, get notification already read according to @NotificationsRead number.
--				 If we want All notifications, set AllNotification=true>
-- =============================================
CREATE PROCEDURE [dbo].[USP_NotificationsGet]
	@IDUserOwnerRelatedObject uniqueidentifier,
	@IDUserActionType int,
	@NotificationsRead int,
	@AllNotification bit,
	@MaxNotificationsNumber int
			
AS
	
	DECLARE @NotificationsToRead int

	
	IF(@IDUserActionType IS NOT NULL)
		BEGIN
		--Da implementare come sotto...
			SELECT IDUserNotification, IDUser, IDUserActionType, URLNotification,
				   IDRelatedObject, NotificationImage, UserNotification, CreatedOn, ViewedOn, NotifiedOn
			FROM   UsersNotifications 
			WHERE  IDUserOwnerRelatedObject = @IDUserOwnerRelatedObject AND IDUserActionType = @IDUserActionType AND ViewedOn IS NULL
			ORDER BY CreatedOn DESC
		END 
	ELSE
		BEGIN
			--Check if we want all notifications
			IF(@AllNotification = 1)
				BEGIN
					SELECT TOP (@MaxNotificationsNumber) IDUserNotification, IDUser, IDUserActionType, URLNotification,
							IDRelatedObject, NotificationImage, UserNotification, CreatedOn, ViewedOn, NotifiedOn, IDUserOwnerRelatedObject
						FROM   UsersNotifications 
						WHERE  IDUserOwnerRelatedObject = @IDUserOwnerRelatedObject
						ORDER BY CreatedOn DESC
				END
			ELSE
				BEGIN

				--Count number of NOT READ Notifications
				SELECT @NotificationsToRead = COUNT(IDUserNotification) FROM UsersNotifications WHERE  IDUserOwnerRelatedObject = @IDUserOwnerRelatedObject AND ViewedOn IS NULL

				--If we have notifications to read, get it
				IF(@NotificationsToRead > 0)
					BEGIN
						SELECT TOP (@MaxNotificationsNumber) IDUserNotification, IDUser, IDUserActionType, URLNotification,
							   IDRelatedObject, NotificationImage, UserNotification, CreatedOn, ViewedOn, NotifiedOn, IDUserOwnerRelatedObject
						FROM   UsersNotifications 
						WHERE  IDUserOwnerRelatedObject = @IDUserOwnerRelatedObject AND ViewedOn IS NULL
						ORDER BY CreatedOn DESC
					END
				ELSE
					-- else select notifications already read
					BEGIN
						SELECT TOP (@NotificationsRead) IDUserNotification, IDUser, IDUserActionType, URLNotification,
									IDRelatedObject, NotificationImage, UserNotification, CreatedOn, ViewedOn, NotifiedOn, IDUserOwnerRelatedObject
								FROM   UsersNotifications 
								WHERE  IDUserOwnerRelatedObject = @IDUserOwnerRelatedObject AND ViewedOn IS NOT NULL
								ORDER BY CreatedOn DESC
					END
				END
		END


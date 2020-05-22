CREATE TABLE [dbo].[UsersNotifications] (
    [IDUserNotification]       UNIQUEIDENTIFIER NOT NULL,
    [IDUser]                   UNIQUEIDENTIFIER NOT NULL,
    [IDUserActionType]         INT              NOT NULL,
    [URLNotification]          NVARCHAR (MAX)   NULL,
    [IDRelatedObject]          UNIQUEIDENTIFIER NULL,
    [NotificationImage]        UNIQUEIDENTIFIER NULL,
    [UserNotification]         NVARCHAR (MAX)   NOT NULL,
    [CreatedOn]                DATETIME         NOT NULL,
    [ViewedOn]                 DATETIME         NULL,
    [NotifiedOn]               DATETIME         NULL,
    [IDUserOwnerRelatedObject] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_UsersNotifications] PRIMARY KEY CLUSTERED ([IDUserNotification] ASC)
);


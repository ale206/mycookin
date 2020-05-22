CREATE TABLE [dbo].[UsersNotifications] (
    [IDUserNotification]     UNIQUEIDENTIFIER NOT NULL,
    [IDUserNotificationType] INT              NOT NULL,
    [IDUser]                 UNIQUEIDENTIFIER NOT NULL,
    [IsEnabled]              BIT              NOT NULL,
    CONSTRAINT [PK_UsersNotifications] PRIMARY KEY CLUSTERED ([IDUserNotification] ASC),
    CONSTRAINT [FK_UsersNotifications_UsersNotificationsTypes] FOREIGN KEY ([IDUserNotificationType]) REFERENCES [dbo].[UsersNotificationsTypes] ([IDUserNotificationType]) ON DELETE CASCADE ON UPDATE CASCADE
);


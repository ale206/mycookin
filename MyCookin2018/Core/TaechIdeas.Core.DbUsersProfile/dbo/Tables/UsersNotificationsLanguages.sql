CREATE TABLE [dbo].[UsersNotificationsLanguages] (
    [IDUserNotificationLanguage] INT            IDENTITY (1, 1) NOT NULL,
    [IDUserNotificationType]     INT            NOT NULL,
    [IDLanguage]                 INT            NOT NULL,
    [NotificationQuestion]       NVARCHAR (500) NOT NULL,
    [NotificationComment]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UsersNotificationsLanguages_1] PRIMARY KEY CLUSTERED ([IDUserNotificationLanguage] ASC),
    CONSTRAINT [FK_UsersNotificationsLanguages_UsersNotificationsTypes] FOREIGN KEY ([IDUserNotificationType]) REFERENCES [dbo].[UsersNotificationsTypes] ([IDUserNotificationType]) ON DELETE CASCADE ON UPDATE CASCADE
);


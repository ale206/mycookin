CREATE TABLE [dbo].[UsersNotificationsTypes] (
    [IDUserNotificationType]  INT            IDENTITY (1, 1) NOT NULL,
    [NotificationType]        NVARCHAR (150) NOT NULL,
    [NotificationTypeEnabled] BIT            NOT NULL,
    [NotificationTypeOrder]   INT            NOT NULL,
    [IsVisible]               BIT            NOT NULL,
    CONSTRAINT [PK_UsersNotificationsTypes_1] PRIMARY KEY CLUSTERED ([IDUserNotificationType] ASC)
);


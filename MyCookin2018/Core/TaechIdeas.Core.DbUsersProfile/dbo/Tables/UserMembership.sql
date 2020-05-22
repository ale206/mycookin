CREATE TABLE [dbo].[UserMembership] (
    [Id]               UNIQUEIDENTIFIER CONSTRAINT [DF_UserMembership_Id] DEFAULT (newid()) NOT NULL,
    [UserId]           UNIQUEIDENTIFIER NOT NULL,
    [WebsiteId]          INT              NOT NULL,
    [UserEnabled]      BIT              NOT NULL,
    [UserLocked]       BIT              NOT NULL,
    [DateMembership]   DATETIME         NOT NULL,
    [AccountExpireOn]  DATETIME         NOT NULL,
    [LastLogon]        DATETIME         NULL,
    [LastLogout]       DATETIME         NULL,
    [Offset]           INT              NOT NULL,
    [UserIsOnline]     BIT              NULL,
    [LastIpAddress]    NVARCHAR (50)    NULL,
    [AccountDeletedOn] DATETIME         NULL,
    CONSTRAINT [PK_UserMembership] PRIMARY KEY CLUSTERED ([Id] ASC)
);


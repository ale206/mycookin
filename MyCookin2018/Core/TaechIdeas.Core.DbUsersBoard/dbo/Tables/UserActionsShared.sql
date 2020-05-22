CREATE TABLE [dbo].[UserActionsShared] (
    [IDUserActionsShared] UNIQUEIDENTIFIER NOT NULL,
    [IDUserAction]        UNIQUEIDENTIFIER NOT NULL,
    [IDUser]              UNIQUEIDENTIFIER NOT NULL,
    [IDSocialNetwork]     INT              NOT NULL,
    [ShareDate]           DATETIME         NOT NULL,
    [IDShareOnSocial]     NVARCHAR (500)   NULL,
    CONSTRAINT [PK_UserActionsShared] PRIMARY KEY CLUSTERED ([IDUserActionsShared] ASC)
);


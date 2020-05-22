CREATE TABLE [dbo].[UsersConversations] (
    [IDUserConversation] UNIQUEIDENTIFIER NOT NULL,
    [IDConversation]     UNIQUEIDENTIFIER NOT NULL,
    [IDUser]             UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]          DATETIME         NOT NULL,
    [ArchivedOn]         DATETIME         NULL,
    CONSTRAINT [PK_UsersConversations] PRIMARY KEY CLUSTERED ([IDUserConversation] ASC)
);


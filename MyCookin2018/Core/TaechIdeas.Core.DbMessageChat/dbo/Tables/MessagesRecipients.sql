CREATE TABLE [dbo].[MessagesRecipients] (
    [IDMessageRecipient] UNIQUEIDENTIFIER CONSTRAINT [DF_MessagesRecipients_IDMessageRecipient] DEFAULT (newid()) NOT NULL,
    [IDMessage]          UNIQUEIDENTIFIER NOT NULL,
    [IDUserConversation] UNIQUEIDENTIFIER NOT NULL,
    [IDUserSender]       UNIQUEIDENTIFIER NOT NULL,
    [IDUserRecipient]    UNIQUEIDENTIFIER NOT NULL,
    [SentOn]             DATETIME         NOT NULL,
    [ViewedOn]           DATETIME         NULL,
    [DeletedOn]          DATETIME         NULL,
    CONSTRAINT [PK_MessagesRecipients] PRIMARY KEY CLUSTERED ([IDMessageRecipient] ASC),
    CONSTRAINT [FK_MessagesRecipients_Messages] FOREIGN KEY ([IDMessage]) REFERENCES [dbo].[Messages] ([IDMessage]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_MessagesRecipients_UsersConversations] FOREIGN KEY ([IDUserConversation]) REFERENCES [dbo].[UsersConversations] ([IDUserConversation])
);


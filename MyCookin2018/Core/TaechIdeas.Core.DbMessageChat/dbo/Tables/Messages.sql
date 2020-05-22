CREATE TABLE [dbo].[Messages] (
    [IDMessage]     UNIQUEIDENTIFIER CONSTRAINT [DF_Messages_IDMessage] DEFAULT (newid()) NOT NULL,
    [IDMessageType] INT              NOT NULL,
    [Message]       NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([IDMessage] ASC),
    CONSTRAINT [FK_Messages_MessagesTypes] FOREIGN KEY ([IDMessageType]) REFERENCES [dbo].[MessagesTypes] ([IDMessageType]) ON DELETE CASCADE ON UPDATE CASCADE
);


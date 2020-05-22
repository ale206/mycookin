CREATE TABLE [dbo].[MessagesTypes] (
    [IDMessageType]      INT           IDENTITY (1, 1) NOT NULL,
    [MessageType]        NVARCHAR (50) NOT NULL,
    [MessageMaxLength]   INT           NOT NULL,
    [MessageTypeEnabled] BIT           NOT NULL,
    CONSTRAINT [PK_MessagesTypes] PRIMARY KEY CLUSTERED ([IDMessageType] ASC)
);


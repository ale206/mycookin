CREATE TABLE [dbo].[LastSeenObjects] (
    [IDLastSeenObject] UNIQUEIDENTIFIER CONSTRAINT [DF_LastSeenObjects_IDLastSeenObject] DEFAULT (newid()) NOT NULL,
    [IDUser]           UNIQUEIDENTIFIER NOT NULL,
    [IDObjectType]     INT              NOT NULL,
    [IDObject]         UNIQUEIDENTIFIER NOT NULL,
    [SeenDateTime]     SMALLDATETIME    NOT NULL,
    [SearchKeyWords]   NVARCHAR (150)   NULL,
    [OtherInfo]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_LastSeenObjects] PRIMARY KEY CLUSTERED ([IDLastSeenObject] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_IDUser]
    ON [dbo].[LastSeenObjects]([IDUser] ASC);


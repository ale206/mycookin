CREATE TABLE [dbo].[MediaAlternativesSizes] (
    [IDMediaAlternativeSize] UNIQUEIDENTIFIER CONSTRAINT [DF_Table_1_IDMediaAlternativeSizes] DEFAULT (newid()) NOT NULL,
    [IDMedia]                UNIQUEIDENTIFIER NOT NULL,
    [MediaSizeType]          NVARCHAR (50)    NOT NULL,
    [MediaServer]            NVARCHAR (150)   NULL,
    [MediaBackupServer]      NVARCHAR (150)   NULL,
    [MediaPath]              NVARCHAR (550)   NOT NULL,
    [MediaMD5Hash]           NVARCHAR (100)   NULL,
    [MediaOnCDN]             BIT              NOT NULL,
    CONSTRAINT [PK_MediaAlternativesSizes] PRIMARY KEY CLUSTERED ([IDMediaAlternativeSize] ASC),
    CONSTRAINT [FK_MediaAlternativesSizes_Media] FOREIGN KEY ([IDMedia]) REFERENCES [dbo].[Media] ([IDMedia]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Unique_IDMedia_AltSize]
    ON [dbo].[MediaAlternativesSizes]([IDMedia] ASC, [MediaSizeType] ASC);


CREATE TABLE [dbo].[Media] (
    [IDMedia]           UNIQUEIDENTIFIER CONSTRAINT [DF_Media_IDMedia] DEFAULT (newid()) NOT NULL,
    [IDMediaOwner]      UNIQUEIDENTIFIER NOT NULL,
    [isImage]           BIT              NOT NULL,
    [isVideo]           BIT              NOT NULL,
    [isLink]            BIT              NOT NULL,
    [isEsternalSource]  BIT              NOT NULL,
    [MediaServer]       NVARCHAR (150)   NULL,
    [MediaBakcupServer] NVARCHAR (150)   NULL,
    [MediaPath]         NVARCHAR (550)   NOT NULL,
    [MediaMD5Hash]      NVARCHAR (100)   NULL,
    [Checked]           BIT              NOT NULL,
    [CheckedByUser]     UNIQUEIDENTIFIER NULL,
    [MediaDisabled]     BIT              NOT NULL,
    [MediaUpdatedOn]    SMALLDATETIME    NOT NULL,
    [MediaDeletedOn]    SMALLDATETIME    NULL,
    [UserIsMediaOwner]  BIT              NOT NULL,
    [MediaOnCDN]        BIT              NOT NULL,
    [MediaType]         NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED ([IDMedia] ASC)
);


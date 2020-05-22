CREATE TABLE [dbo].[IsoLanguages] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Iso6391]      NVARCHAR (7)   NULL,
    [Iso6392]      NVARCHAR (7)   NULL,
    [Iso6393]      NVARCHAR (7)   NULL,
    [LanguageName] NVARCHAR (150) NOT NULL,
    [CreatedAt]    DATETIME       NOT NULL,
    [UpdatedAt]    DATETIME       NOT NULL,
    [DeletedAt]    DATETIME       NULL,
    [Deleted]      BIT            NOT NULL,
    [Enabled]      BIT            NOT NULL,
    CONSTRAINT [PK_dbo.IsoLanguages] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Iso639]
    ON [dbo].[IsoLanguages]([Iso6391] ASC, [Iso6392] ASC, [Iso6393] ASC);


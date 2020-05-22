CREATE TABLE [dbo].[Languages] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [LanguageName] NVARCHAR (MAX) NULL,
    [LanguageCode] NVARCHAR (MAX) NULL,
    [CreatedAt]    DATETIME       NOT NULL,
    [UpdatedAt]    DATETIME       NOT NULL,
    [DeletedAt]    DATETIME       NULL,
    [Deleted]      BIT            NOT NULL,
    [Enabled]      BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Languages] PRIMARY KEY CLUSTERED ([Id] ASC)
);


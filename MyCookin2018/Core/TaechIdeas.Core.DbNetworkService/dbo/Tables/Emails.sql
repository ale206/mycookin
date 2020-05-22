CREATE TABLE [dbo].[Emails] (
    [Id]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [From]         NVARCHAR (MAX)   NULL,
    [To]           NVARCHAR (MAX)   NULL,
    [Cc]           NVARCHAR (MAX)   NULL,
    [Bcc]          NVARCHAR (MAX)   NULL,
    [Subject]      NVARCHAR (MAX)   NULL,
    [Message]      NVARCHAR (MAX)   NULL,
    [HtmlFilePath] NVARCHAR (MAX)   NULL,
    [CreatedAt]    DATETIME         NOT NULL,
    [UpdatedAt]    DATETIME         NULL,
    [DeletedAt]    DATETIME         NULL,
    [Deleted]      BIT              NOT NULL,
    [Enabled]      BIT              NOT NULL,
    CONSTRAINT [PK_dbo.Emails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


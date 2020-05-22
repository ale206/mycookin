CREATE TABLE [dbo].[StatusRecords] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [EmailStatus] INT              NOT NULL,
    [CreatedAt]   DATETIME         NOT NULL,
    [UpdatedAt]   DATETIME         NULL,
    [DeletedAt]   DATETIME         NULL,
    [Deleted]     BIT              NOT NULL,
    [Enabled]     BIT              NOT NULL,
    [Email_Id]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_dbo.StatusRecords] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.StatusRecords_dbo.Emails_Email_Id] FOREIGN KEY ([Email_Id]) REFERENCES [dbo].[Emails] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Email_Id]
    ON [dbo].[StatusRecords]([Email_Id] ASC);


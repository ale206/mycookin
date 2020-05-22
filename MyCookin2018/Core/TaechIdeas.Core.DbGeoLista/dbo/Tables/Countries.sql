CREATE TABLE [dbo].[Countries] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CountryCode] NVARCHAR (250) NOT NULL,
    [CreatedAt]   DATETIME       NOT NULL,
    [UpdatedAt]   DATETIME       NOT NULL,
    [DeletedAt]   DATETIME       NULL,
    [Deleted]     BIT            NOT NULL,
    [Enabled]     BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CountryCode]
    ON [dbo].[Countries]([CountryCode] ASC);


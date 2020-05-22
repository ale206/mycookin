CREATE TABLE [dbo].[TimeZones] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (250) NOT NULL,
    [CreatedAt] DATETIME       NOT NULL,
    [UpdatedAt] DATETIME       NOT NULL,
    [DeletedAt] DATETIME       NULL,
    [Deleted]   BIT            NOT NULL,
    [Enabled]   BIT            NOT NULL,
    CONSTRAINT [PK_dbo.TimeZones] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[TimeZones]([Name] ASC);


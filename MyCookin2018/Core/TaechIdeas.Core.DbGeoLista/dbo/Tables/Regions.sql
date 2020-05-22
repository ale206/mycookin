CREATE TABLE [dbo].[Regions] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (450) NOT NULL,
    [AsciiName]  NVARCHAR (450) NOT NULL,
    [CreatedAt]  DATETIME       NOT NULL,
    [UpdatedAt]  DATETIME       NOT NULL,
    [DeletedAt]  DATETIME       NULL,
    [Deleted]    BIT            NOT NULL,
    [Enabled]    BIT            NOT NULL,
    [Country_Id] BIGINT         NULL,
    CONSTRAINT [PK_dbo.Regions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Regions_dbo.Countries_Country_Id] FOREIGN KEY ([Country_Id]) REFERENCES [dbo].[Countries] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AsciiName]
    ON [dbo].[Regions]([AsciiName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[Regions]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Country_Id]
    ON [dbo].[Regions]([Country_Id] ASC);


CREATE TABLE [dbo].[Provinces] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (450) NOT NULL,
    [AsciiName]  NVARCHAR (450) NOT NULL,
    [CreatedAt]  DATETIME       NOT NULL,
    [UpdatedAt]  DATETIME       NOT NULL,
    [DeletedAt]  DATETIME       NULL,
    [Deleted]    BIT            NOT NULL,
    [Enabled]    BIT            NOT NULL,
    [Country_Id] BIGINT         NULL,
    [Region_Id]  BIGINT         NULL,
    CONSTRAINT [PK_dbo.Provinces] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Provinces_dbo.Countries_Country_Id] FOREIGN KEY ([Country_Id]) REFERENCES [dbo].[Countries] ([Id]),
    CONSTRAINT [FK_dbo.Provinces_dbo.Regions_Region_Id] FOREIGN KEY ([Region_Id]) REFERENCES [dbo].[Regions] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AsciiName]
    ON [dbo].[Provinces]([AsciiName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[Provinces]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Region_Id]
    ON [dbo].[Provinces]([Region_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Country_Id]
    ON [dbo].[Provinces]([Country_Id] ASC);


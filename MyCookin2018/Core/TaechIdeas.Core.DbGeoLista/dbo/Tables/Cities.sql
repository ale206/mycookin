CREATE TABLE [dbo].[Cities] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (200) NOT NULL,
    [AsciiName]      NVARCHAR (200) NULL,
    [AlternateNames] NVARCHAR (MAX) NULL,
    [Latitude]       NVARCHAR (50)  NULL,
    [Longitude]      NVARCHAR (45)  NULL,
    [Population]     BIGINT         NOT NULL,
    [Elevation]      INT            NOT NULL,
    [LastEdit]       DATETIME       NOT NULL,
    [CreatedAt]      DATETIME       NOT NULL,
    [UpdatedAt]      DATETIME       NOT NULL,
    [DeletedAt]      DATETIME       NULL,
    [Deleted]        BIT            NOT NULL,
    [Enabled]        BIT            NOT NULL,
    [Country_Id]     BIGINT         NULL,
    [Province_Id]    BIGINT         NULL,
    [Region_Id]      BIGINT         NULL,
    [TimeZone_Id]    BIGINT         NULL,
    CONSTRAINT [PK_dbo.Cities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Cities_dbo.Countries_Country_Id] FOREIGN KEY ([Country_Id]) REFERENCES [dbo].[Countries] ([Id]),
    CONSTRAINT [FK_dbo.Cities_dbo.Provinces_Province_Id] FOREIGN KEY ([Province_Id]) REFERENCES [dbo].[Provinces] ([Id]),
    CONSTRAINT [FK_dbo.Cities_dbo.Regions_Region_Id] FOREIGN KEY ([Region_Id]) REFERENCES [dbo].[Regions] ([Id]),
    CONSTRAINT [FK_dbo.Cities_dbo.TimeZones_TimeZone_Id] FOREIGN KEY ([TimeZone_Id]) REFERENCES [dbo].[TimeZones] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[Cities]([Name] ASC, [AsciiName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TimeZone_Id]
    ON [dbo].[Cities]([TimeZone_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Region_Id]
    ON [dbo].[Cities]([Region_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Province_Id]
    ON [dbo].[Cities]([Province_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Country_Id]
    ON [dbo].[Cities]([Country_Id] ASC);


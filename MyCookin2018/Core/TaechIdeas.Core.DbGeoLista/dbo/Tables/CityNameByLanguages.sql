CREATE TABLE [dbo].[CityNameByLanguages] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [TranslatedName]  NVARCHAR (250) NULL,
    [IsPreferredName] BIT            NOT NULL,
    [IsShortName]     BIT            NOT NULL,
    [IsColloquial]    BIT            NOT NULL,
    [IsAlternateName] BIT            NOT NULL,
    [CreatedAt]       DATETIME       NOT NULL,
    [UpdatedAt]       DATETIME       NOT NULL,
    [DeletedAt]       DATETIME       NULL,
    [Deleted]         BIT            NOT NULL,
    [Enabled]         BIT            NOT NULL,
    [City_Id]         BIGINT         NULL,
    [IsoLanguage_Id]  BIGINT         NULL,
    CONSTRAINT [PK_dbo.CityNameByLanguages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.CityNameByLanguages_dbo.Cities_City_Id] FOREIGN KEY ([City_Id]) REFERENCES [dbo].[Cities] ([Id]),
    CONSTRAINT [FK_dbo.CityNameByLanguages_dbo.IsoLanguages_IsoLanguage_Id] FOREIGN KEY ([IsoLanguage_Id]) REFERENCES [dbo].[IsoLanguages] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TranslatedName]
    ON [dbo].[CityNameByLanguages]([TranslatedName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IsoLanguage_Id]
    ON [dbo].[CityNameByLanguages]([IsoLanguage_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_City_Id]
    ON [dbo].[CityNameByLanguages]([City_Id] ASC);


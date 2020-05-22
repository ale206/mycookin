CREATE TABLE [dbo].[Languages] (
    [IDLanguage]   INT            IDENTITY (1, 1) NOT NULL,
    [Language]     NVARCHAR (250) NOT NULL,
    [LanguageCode] NVARCHAR (50)  NOT NULL,
    [Enabled]      BIT            NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([IDLanguage] ASC)
);


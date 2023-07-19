CREATE TABLE [dbo].[QuantityNotStdLanguage] (
    [IDQuantityNotStdLanguage] INT            IDENTITY (1, 1) NOT NULL,
    [IDQuantityNotStd]         INT            NOT NULL,
    [IDLanguage]               INT            NOT NULL,
    [QuantityNotStdSingular]   NVARCHAR (150) NULL,
    [QuantityNotStdPlural]     NVARCHAR (150) NULL,
    CONSTRAINT [PK_QuantityNotStdLanguage] PRIMARY KEY CLUSTERED ([IDQuantityNotStdLanguage] ASC),
    CONSTRAINT [FK_QuantityNotStdLanguage_QuantityNotStd] FOREIGN KEY ([IDQuantityNotStd]) REFERENCES [dbo].[QuantityNotStd] ([IDQuantityNotStd]) ON DELETE CASCADE ON UPDATE CASCADE
);


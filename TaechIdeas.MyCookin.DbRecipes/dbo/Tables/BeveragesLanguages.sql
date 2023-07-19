CREATE TABLE [dbo].[BeveragesLanguages] (
    [IDBeverageLanguage]   UNIQUEIDENTIFIER NOT NULL,
    [IDBeverage]           UNIQUEIDENTIFIER NOT NULL,
    [IDLanguage]           INT              NOT NULL,
    [BeverageInfoLanguage] NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_BeveragesLanguages_1] PRIMARY KEY CLUSTERED ([IDBeverageLanguage] ASC),
    CONSTRAINT [FK_BeveragesLanguages_Beverages] FOREIGN KEY ([IDBeverage]) REFERENCES [dbo].[Beverages] ([IDBeverage]) ON DELETE CASCADE ON UPDATE CASCADE
);


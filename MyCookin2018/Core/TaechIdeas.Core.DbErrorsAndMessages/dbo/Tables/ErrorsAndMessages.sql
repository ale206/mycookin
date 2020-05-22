CREATE TABLE [dbo].[ErrorsAndMessages] (
    [IDErrorAndMessage]     UNIQUEIDENTIFIER CONSTRAINT [DF_ErrorsAndMessages_IDErrorAndMessage] DEFAULT (newid()) NOT NULL,
    [IDLanguage]            INT              NOT NULL,
    [ErrorMessageCode]      NVARCHAR (10)    NOT NULL,
    [PreferredForErrorCode] BIT              NOT NULL,
    [ErrorMessage]          NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_ErrorsAndMessages] PRIMARY KEY NONCLUSTERED ([IDErrorAndMessage] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [IX_Unique_IDLang-ErrorMessageCode]
    ON [dbo].[ErrorsAndMessages]([IDLanguage] ASC, [ErrorMessageCode] ASC);


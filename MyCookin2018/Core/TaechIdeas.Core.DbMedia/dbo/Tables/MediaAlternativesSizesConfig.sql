CREATE TABLE [dbo].[MediaAlternativesSizesConfig] (
    [IDMediaAlternativeSizeConfig] INT            IDENTITY (1, 1) NOT NULL,
    [MediaType]                    NVARCHAR (50)  NOT NULL,
    [MediaSizeType]                NVARCHAR (50)  NOT NULL,
    [SavePath]                     NVARCHAR (550) NOT NULL,
    [MediaHeight]                  INT            NULL,
    [MediaWidth]                   INT            NULL,
    CONSTRAINT [PK_MediaAlternativesSizesConfig] PRIMARY KEY CLUSTERED ([IDMediaAlternativeSizeConfig] ASC)
);


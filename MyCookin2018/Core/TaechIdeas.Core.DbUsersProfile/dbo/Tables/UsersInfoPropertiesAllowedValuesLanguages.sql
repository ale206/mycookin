CREATE TABLE [dbo].[UsersInfoPropertiesAllowedValuesLanguages] (
    [IDUserInfoPropertyAllowedValueLanguage] INT            IDENTITY (1, 1) NOT NULL,
    [IDUserInfoPropertyAllowedValue]         INT            NOT NULL,
    [IDLanguage]                             INT            NOT NULL,
    [PropertyAllowedValueLanguage]           NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_UsersInfoPropertiesAllowedValuesLanguages] PRIMARY KEY CLUSTERED ([IDUserInfoPropertyAllowedValueLanguage] ASC),
    CONSTRAINT [FK_UsersInfoPropertiesAllowedValuesLanguages_UsersInfoPropertiesAllowedValues] FOREIGN KEY ([IDUserInfoPropertyAllowedValue]) REFERENCES [dbo].[UsersInfoPropertiesAllowedValues] ([IDUserInfoPropertyAllowedValue]) ON DELETE CASCADE ON UPDATE CASCADE
);


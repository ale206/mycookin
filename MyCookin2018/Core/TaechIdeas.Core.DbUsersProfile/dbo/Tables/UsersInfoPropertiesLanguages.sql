CREATE TABLE [dbo].[UsersInfoPropertiesLanguages] (
    [IDUserInfoPropertyLanguage] INT            IDENTITY (1, 1) NOT NULL,
    [IDUserInfoProperty]         INT            NOT NULL,
    [IDLanguage]                 INT            NOT NULL,
    [UserInfoProperty]           NVARCHAR (100) NOT NULL,
    [UserInfoPropertyToolTip]    NVARCHAR (150) NULL,
    [Description]                NVARCHAR (250) NULL,
    CONSTRAINT [PK_UsersInfoPropertysLanguages] PRIMARY KEY CLUSTERED ([IDUserInfoPropertyLanguage] ASC),
    CONSTRAINT [FK_UsersInfoPropertysLanguages_UserInfoProperties] FOREIGN KEY ([IDUserInfoProperty]) REFERENCES [dbo].[UsersInfoProperties] ([IDUserInfoProperty]) ON UPDATE CASCADE
);


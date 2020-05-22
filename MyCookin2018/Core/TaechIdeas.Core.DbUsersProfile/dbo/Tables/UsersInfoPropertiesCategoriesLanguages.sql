CREATE TABLE [dbo].[UsersInfoPropertiesCategoriesLanguages] (
    [IDUserInfoPropertyCategoryLanguage] INT            IDENTITY (1, 1) NOT NULL,
    [IDUserInfoPropertyCategory]         INT            NOT NULL,
    [IDLanguage]                         INT            NOT NULL,
    [UserInfoPropertyCategory]           NVARCHAR (100) NOT NULL,
    [UserInfoPropertyCategoryToolTip]    NVARCHAR (150) NULL,
    CONSTRAINT [PK_UsersInfoPropertiesCategoriesLanguages] PRIMARY KEY CLUSTERED ([IDUserInfoPropertyCategoryLanguage] ASC),
    CONSTRAINT [FK_UsersInfoPropertiesCategoriesLanguages_UsersInfoPropertiesCategories] FOREIGN KEY ([IDUserInfoPropertyCategory]) REFERENCES [dbo].[UsersInfoPropertiesCategories] ([IDUserInfoPropertyCategory]) ON UPDATE CASCADE
);


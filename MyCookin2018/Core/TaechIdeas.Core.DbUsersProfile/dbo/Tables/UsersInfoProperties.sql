CREATE TABLE [dbo].[UsersInfoProperties] (
    [IDUserInfoProperty]         INT IDENTITY (1, 1) NOT NULL,
    [IDUserInfoPropertyType]     INT NOT NULL,
    [IDUserInfoPropertyCategory] INT NOT NULL,
    [Enabled]                    BIT NOT NULL,
    [Mandatory]                  BIT NOT NULL,
    [PropertySortOrder]          INT NOT NULL,
    CONSTRAINT [PK_UserInfoProperties] PRIMARY KEY CLUSTERED ([IDUserInfoProperty] ASC),
    CONSTRAINT [FK_UserInfoProperties_UserInfoPropertyTypes] FOREIGN KEY ([IDUserInfoPropertyType]) REFERENCES [dbo].[UsersInfoPropertiesTypes] ([IDUserInfoPropertyType]) ON UPDATE CASCADE,
    CONSTRAINT [FK_UsersInfoProperties_UsersInfoPropertiesCategories] FOREIGN KEY ([IDUserInfoPropertyCategory]) REFERENCES [dbo].[UsersInfoPropertiesCategories] ([IDUserInfoPropertyCategory]) ON UPDATE CASCADE
);


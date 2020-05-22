CREATE TABLE [dbo].[UsersInfoPropertiesTypes] (
    [IDUserInfoPropertyType] INT            IDENTITY (1, 1) NOT NULL,
    [UserInfoPropertyType]   NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_UserInfoPropertyTypes] PRIMARY KEY CLUSTERED ([IDUserInfoPropertyType] ASC)
);


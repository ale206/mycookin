CREATE TABLE [dbo].[UsersInfoPropertiesCategories] (
    [IDUserInfoPropertyCategory] INT IDENTITY (1, 1) NOT NULL,
    [Enabled]                    BIT NOT NULL,
    CONSTRAINT [PK_UsersInfoPropertyCategories] PRIMARY KEY CLUSTERED ([IDUserInfoPropertyCategory] ASC)
);


CREATE TABLE [dbo].[UsersInfoPropertiesAllowedValues] (
    [IDUserInfoPropertyAllowedValue] INT IDENTITY (1, 1) NOT NULL,
    [IDUserInfoProperty]             INT NOT NULL,
    [PropertyAllowedValueOrder]      INT NOT NULL,
    [PropertyAllowedValueSelected]   BIT NOT NULL,
    CONSTRAINT [PK_UsersInfoPropertiesAllowedValues] PRIMARY KEY CLUSTERED ([IDUserInfoPropertyAllowedValue] ASC),
    CONSTRAINT [FK_UsersInfoPropertiesAllowedValues_UsersInfoProperties] FOREIGN KEY ([IDUserInfoProperty]) REFERENCES [dbo].[UsersInfoProperties] ([IDUserInfoProperty]) ON DELETE CASCADE ON UPDATE CASCADE
);


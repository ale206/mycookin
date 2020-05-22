CREATE TABLE [dbo].[UsersInfoPropertiesCompiled] (
    [IDUserInfoPropertyCompiled]     UNIQUEIDENTIFIER CONSTRAINT [DF_UsersInfoPropertiesCompiled_IDUserInfoPropertyCompiled] DEFAULT (newid()) NOT NULL,
    [IDUser]                         UNIQUEIDENTIFIER NOT NULL,
    [IDUserInfoProperty]             INT              NOT NULL,
    [IDUserInfoPropertyAllowedValue] INT              NULL,
    [PropertyValue]                  NVARCHAR (MAX)   NULL,
    [LastUpdate]                     SMALLDATETIME    NOT NULL,
    [IDVisibility]                   INT              NULL,
    CONSTRAINT [PK_UsersInfoPropertiesCompiled] PRIMARY KEY CLUSTERED ([IDUserInfoPropertyCompiled] ASC),
    CONSTRAINT [FK_UsersInfoPropertiesCompiled_UserInfoProperties] FOREIGN KEY ([IDUserInfoProperty]) REFERENCES [dbo].[UsersInfoProperties] ([IDUserInfoProperty]) ON UPDATE CASCADE,
    CONSTRAINT [FK_UsersInfoPropertiesCompiled_Users] FOREIGN KEY ([IDUser]) REFERENCES [dbo].[Users] ([IDUser]) ON DELETE CASCADE ON UPDATE CASCADE
);


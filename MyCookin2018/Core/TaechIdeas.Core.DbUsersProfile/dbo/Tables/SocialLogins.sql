CREATE TABLE [dbo].[SocialLogins] (
    [IDSocialLogin]      UNIQUEIDENTIFIER NOT NULL,
    [IDSocialNetwork]    INT              NOT NULL,
    [IDUser]             UNIQUEIDENTIFIER NOT NULL,
    [IDUserSocial]       NVARCHAR (250)   NOT NULL,
    [Link]               NVARCHAR (500)   NULL,
    [VerifiedEmail]      BIT              NULL,
    [PictureUrl]         NVARCHAR (500)   NULL,
    [Locale]             NVARCHAR (50)    NULL,
    [AccessToken]        NVARCHAR (500)   NULL,
    [FriendsRetrievedOn] SMALLDATETIME    NULL,
    [RefreshToken]       NVARCHAR (500)   NULL,
    CONSTRAINT [PK_GoogleLogins] PRIMARY KEY CLUSTERED ([IDSocialLogin] ASC),
    CONSTRAINT [FK_SocialLogins_Users] FOREIGN KEY ([IDUser]) REFERENCES [dbo].[Users] ([IDUser]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IDUser_IDUserSocial]
    ON [dbo].[SocialLogins]([IDUser] ASC, [IDUserSocial] ASC);


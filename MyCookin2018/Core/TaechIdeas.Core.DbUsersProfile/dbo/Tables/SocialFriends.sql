CREATE TABLE [dbo].[SocialFriends] (
    [IDSocialFriend]    UNIQUEIDENTIFIER NOT NULL,
    [IDSocialNetwork]   INT              NOT NULL,
    [LastTimeContacted] DATETIME         NULL,
    [ContactAgain]      BIT              NULL,
    [FullName]          NVARCHAR (150)   NULL,
    [GivenName]         NVARCHAR (150)   NULL,
    [FamilyName]        NVARCHAR (150)   NULL,
    [Emails]            NVARCHAR (MAX)   NULL,
    [Phones]            NVARCHAR (MAX)   NULL,
    [PhotoUrl]          NVARCHAR (MAX)   NULL,
    [IDUserOnSocial]    NVARCHAR (150)   NULL,
    CONSTRAINT [PK_SocialFriends] PRIMARY KEY CLUSTERED ([IDSocialFriend] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IDUserOnSocial]
    ON [dbo].[SocialFriends]([IDUserOnSocial] ASC);


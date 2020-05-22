CREATE TABLE [dbo].[SocialUserFriends] (
    [IDSocialUserFriend] UNIQUEIDENTIFIER NOT NULL,
    [IDUser]             UNIQUEIDENTIFIER NOT NULL,
    [IDSocialFriend]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SocialUserFriends] PRIMARY KEY CLUSTERED ([IDSocialUserFriend] ASC),
    CONSTRAINT [FK_SocialUserFriends_Users] FOREIGN KEY ([IDUser]) REFERENCES [dbo].[Users] ([IDUser]) ON DELETE CASCADE ON UPDATE CASCADE
);


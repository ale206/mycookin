CREATE TABLE [dbo].[UsersFriendsListsMembers] (
    [IDUserFriendListMember] UNIQUEIDENTIFIER CONSTRAINT [DF_UsersFriendsListsMembers_IDUserFriendListMember] DEFAULT (newid()) NOT NULL,
    [IDUserFriendList]       UNIQUEIDENTIFIER NOT NULL,
    [IDUserMember]           UNIQUEIDENTIFIER NOT NULL,
    [UserAddedToListOn]      SMALLDATETIME    NOT NULL,
    CONSTRAINT [PK_UsersFriendsListsMembers] PRIMARY KEY CLUSTERED ([IDUserFriendListMember] ASC),
    CONSTRAINT [FK_UsersFriendsListsMembers_Users] FOREIGN KEY ([IDUserMember]) REFERENCES [dbo].[Users] ([IDUser]),
    CONSTRAINT [FK_UsersFriendsListsMembers_UsersFriendsLists] FOREIGN KEY ([IDUserFriendList]) REFERENCES [dbo].[UsersFriendsLists] ([IDUserFriendList])
);


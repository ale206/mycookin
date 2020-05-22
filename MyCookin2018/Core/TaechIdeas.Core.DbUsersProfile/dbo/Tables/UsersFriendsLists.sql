CREATE TABLE [dbo].[UsersFriendsLists] (
    [IDUserFriendList]      UNIQUEIDENTIFIER CONSTRAINT [DF_Table_1_IDUserFriendList] DEFAULT (newid()) NOT NULL,
    [IDUserFriendListOwner] UNIQUEIDENTIFIER NOT NULL,
    [UserFriendListName]    NVARCHAR (50)    NOT NULL,
    [UserFriendListAddedOn] SMALLDATETIME    NOT NULL,
    CONSTRAINT [PK_UsersFriendsLists] PRIMARY KEY CLUSTERED ([IDUserFriendList] ASC),
    CONSTRAINT [FK_UsersFriendsLists_Users] FOREIGN KEY ([IDUserFriendListOwner]) REFERENCES [dbo].[Users] ([IDUser])
);


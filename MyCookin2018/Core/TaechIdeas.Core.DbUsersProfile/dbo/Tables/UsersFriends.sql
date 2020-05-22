CREATE TABLE [dbo].[UsersFriends] (
    [IDUserFriend]            UNIQUEIDENTIFIER CONSTRAINT [DF_UsersFriends_IDUserFriend] DEFAULT (newid()) NOT NULL,
    [IDUser1]                 UNIQUEIDENTIFIER NOT NULL,
    [IDUser2]                 UNIQUEIDENTIFIER NOT NULL,
    [FriendshipCompletedDate] SMALLDATETIME    NULL,
    [UserBlocked]             SMALLDATETIME    NULL,
    [FriendshipRequestedDate] SMALLDATETIME    NULL,
    CONSTRAINT [PK_UsersFriends] PRIMARY KEY CLUSTERED ([IDUserFriend] ASC),
    CONSTRAINT [FK_UsersFriends_Users] FOREIGN KEY ([IDUser1]) REFERENCES [dbo].[Users] ([IDUser]),
    CONSTRAINT [FK_UsersFriends_Users1] FOREIGN KEY ([IDUser2]) REFERENCES [dbo].[Users] ([IDUser])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IDUser1-IDUser2]
    ON [dbo].[UsersFriends]([IDUser1] ASC, [IDUser2] ASC)
    INCLUDE([FriendshipCompletedDate], [UserBlocked], [FriendshipRequestedDate]);


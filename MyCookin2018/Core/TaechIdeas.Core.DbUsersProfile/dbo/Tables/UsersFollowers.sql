CREATE TABLE [dbo].[UsersFollowers] (
    [IDUserFollower]   UNIQUEIDENTIFIER CONSTRAINT [DF_UsersFollowers_IDUserFollower] DEFAULT (newid()) NOT NULL,
    [IDUser]           UNIQUEIDENTIFIER NOT NULL,
    [IDUserFollowed]   UNIQUEIDENTIFIER NOT NULL,
    [UserFollowerFrom] SMALLDATETIME    NOT NULL,
    CONSTRAINT [PK_UsersFollowers] PRIMARY KEY CLUSTERED ([IDUserFollower] ASC),
    CONSTRAINT [FK_UsersFollowers_Users] FOREIGN KEY ([IDUser]) REFERENCES [dbo].[Users] ([IDUser])
);


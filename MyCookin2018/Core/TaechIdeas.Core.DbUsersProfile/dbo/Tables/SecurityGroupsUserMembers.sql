CREATE TABLE [dbo].[SecurityGroupsUserMembers] (
    [IDSecurityGroupUserMember] UNIQUEIDENTIFIER CONSTRAINT [DF_SecurityGroupsUserMemebers_IDSecurityGroupUserMemeber] DEFAULT (newid()) NOT NULL,
    [IDSecurityGroup]           UNIQUEIDENTIFIER NOT NULL,
    [IDUser]                    UNIQUEIDENTIFIER NOT NULL,
    [MembershipDate]            SMALLDATETIME    NOT NULL,
    CONSTRAINT [PK_SecurityGroupsUserMemebers] PRIMARY KEY CLUSTERED ([IDSecurityGroupUserMember] ASC),
    CONSTRAINT [FK_SecurityGroupsUserMemebers_SecurityGroups] FOREIGN KEY ([IDSecurityGroup]) REFERENCES [dbo].[SecurityGroups] ([IDSecurityGroup]) ON UPDATE CASCADE,
    CONSTRAINT [FK_SecurityGroupsUserMemebers_Users] FOREIGN KEY ([IDUser]) REFERENCES [dbo].[Users] ([IDUser]) ON DELETE CASCADE ON UPDATE CASCADE
);


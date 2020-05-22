CREATE TABLE [dbo].[SecurityGroups] (
    [IDSecurityGroup] UNIQUEIDENTIFIER CONSTRAINT [DF_SecurityGroups_IDSecurityGroup] DEFAULT (newid()) NOT NULL,
    [SecurityGroup]   NVARCHAR (100)   NULL,
    [Enabled]         BIT              NOT NULL,
    [Description]     NVARCHAR (250)   NULL,
    CONSTRAINT [PK_SecurityGroups] PRIMARY KEY CLUSTERED ([IDSecurityGroup] ASC)
);


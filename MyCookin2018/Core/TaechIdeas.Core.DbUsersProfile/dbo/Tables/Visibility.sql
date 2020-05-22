CREATE TABLE [dbo].[Visibility] (
    [IDVisibility]     INT            IDENTITY (1, 1) NOT NULL,
    [Visibility]       NVARCHAR (50)  NULL,
    [Description]      NVARCHAR (250) NULL,
    [Enabled]          BIT            NOT NULL,
    [VisibleToAll]     BIT            NOT NULL,
    [VisibleToMe]      BIT            NOT NULL,
    [VisibleToFriends] BIT            NOT NULL,
    CONSTRAINT [PK_Visibility] PRIMARY KEY CLUSTERED ([IDVisibility] ASC)
);


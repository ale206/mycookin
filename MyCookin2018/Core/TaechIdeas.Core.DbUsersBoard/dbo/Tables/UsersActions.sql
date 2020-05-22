CREATE TABLE [dbo].[UsersActions] (
    [IDUserAction]          UNIQUEIDENTIFIER CONSTRAINT [DF_UsersActions_IDUserAction] DEFAULT (newid()) NOT NULL,
    [IDUser]                UNIQUEIDENTIFIER NOT NULL,
    [IDUserActionFather]    UNIQUEIDENTIFIER NULL,
    [IDUserActionType]      INT              NOT NULL,
    [IDActionRelatedObject] UNIQUEIDENTIFIER NULL,
    [UserActionMessage]     NVARCHAR (MAX)   NULL,
    [IDVisibility]          INT              NULL,
    [UserActionDate]        DATETIME         NOT NULL,
    [DeletedOn]             SMALLDATETIME    NULL,
    CONSTRAINT [PK_UsersActions] PRIMARY KEY CLUSTERED ([IDUserAction] ASC),
    CONSTRAINT [FK_UsersActions_UserActionsTypes] FOREIGN KEY ([IDUserActionType]) REFERENCES [dbo].[UserActionsTypes] ([IDUserActionType]) ON UPDATE CASCADE,
    CONSTRAINT [FK_UsersActions_UsersActions] FOREIGN KEY ([IDUserActionFather]) REFERENCES [dbo].[UsersActions] ([IDUserAction])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserActionType]
    ON [dbo].[UsersActions]([IDUserActionType] ASC);


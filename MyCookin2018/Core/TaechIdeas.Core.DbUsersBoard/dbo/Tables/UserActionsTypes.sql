CREATE TABLE [dbo].[UserActionsTypes] (
    [IDUserActionType]               INT IDENTITY (1, 1) NOT NULL,
    [UserActionTypeEnabled]          BIT NOT NULL,
    [UserActionTypeSiteNotice]       BIT NOT NULL,
    [UserActionTypeMailNotice]       BIT NOT NULL,
    [UserActionTypeSmsNotice]        BIT NOT NULL,
    [UserActionTypeMessageMaxLength] INT NULL,
    CONSTRAINT [PK_UserActionsTypes] PRIMARY KEY CLUSTERED ([IDUserActionType] ASC)
);


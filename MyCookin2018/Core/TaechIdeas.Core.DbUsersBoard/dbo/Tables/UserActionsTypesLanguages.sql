CREATE TABLE [dbo].[UserActionsTypesLanguages] (
    [IDUserActionTypeLanguage]     INT            IDENTITY (1, 1) NOT NULL,
    [IDUserActionType]             INT            NOT NULL,
    [IDLanguage]                   INT            NOT NULL,
    [UserActionType]               NVARCHAR (100) NOT NULL,
    [UserActionTypeTemplate]       NVARCHAR (550) NULL,
    [UserActionTypeTemplatePlural] NVARCHAR (550) NULL,
    [UserActionTypeToolTip]        NVARCHAR (150) NULL,
    [NotificationTemplate]         NVARCHAR (550) NULL,
    CONSTRAINT [PK_UserActionsTypesLanguages] PRIMARY KEY CLUSTERED ([IDUserActionTypeLanguage] ASC),
    CONSTRAINT [FK_UserActionsTypesLanguages_UserActionsTypes] FOREIGN KEY ([IDUserActionType]) REFERENCES [dbo].[UserActionsTypes] ([IDUserActionType]) ON UPDATE CASCADE
);


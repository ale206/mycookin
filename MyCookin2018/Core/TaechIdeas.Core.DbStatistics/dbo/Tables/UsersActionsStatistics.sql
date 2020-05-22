CREATE TABLE [dbo].[UsersActionsStatistics] (
    [IDUserActionStatistic] UNIQUEIDENTIFIER NOT NULL,
    [IDUser]                UNIQUEIDENTIFIER NULL,
    [IDRelatedObject]       UNIQUEIDENTIFIER NULL,
    [ActionType]            INT              NOT NULL,
    [Comments]              NVARCHAR (MAX)   NULL,
    [DateAction]            DATETIME         NOT NULL,
    [FileOrigin]            NVARCHAR (MAX)   NULL,
    [SearchString]          NVARCHAR (MAX)   NULL,
    [OtherInfo]             NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_UsersActionsStats] PRIMARY KEY CLUSTERED ([IDUserActionStatistic] ASC)
);


CREATE TABLE [dbo].[ErrorsLogs] (
    [IDErrorLog]             UNIQUEIDENTIFIER NOT NULL,
    [ErrorNumber]            NVARCHAR (MAX)   NULL,
    [ErrorSeverity]          NVARCHAR (MAX)   NULL,
    [ErrorState]             NVARCHAR (MAX)   NULL,
    [ErrorProcedure]         NVARCHAR (MAX)   NULL,
    [ErrorLine]              NVARCHAR (MAX)   NULL,
    [ErrorMessage]           NVARCHAR (MAX)   NULL,
    [FileOrigin]             NVARCHAR (MAX)   NULL,
    [DateError]              SMALLDATETIME    NULL,
    [ErrorMessageCode]       NVARCHAR (MAX)   NULL,
    [isStoredProcedureError] BIT              NULL,
    [isTriggerError]         BIT              NULL,
    [isApplicationError]     BIT              NULL,
    [IDUser]                 NVARCHAR (MAX)   NULL,
    [isApplicationLog]       BIT              NULL
);


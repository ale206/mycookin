CREATE TABLE [dbo].[AuditEvent] (
    [IDAuditEvent]      UNIQUEIDENTIFIER CONSTRAINT [DF_AuditEvent_IDAuditEvent] DEFAULT (newid()) NOT NULL,
    [AuditEventMessage] NVARCHAR (MAX)   NULL,
    [ObjectID]          UNIQUEIDENTIFIER NULL,
    [ObjectType]        NVARCHAR (50)    NULL,
    [ObjectTxtInfo]     NVARCHAR (MAX)   NULL,
    [AuditEventLevel]   INT              NULL,
    [EventInsertedOn]   SMALLDATETIME    NULL,
    [EventUpdatedOn]    SMALLDATETIME    NULL,
    [IDEventUpdatedBy]  UNIQUEIDENTIFIER NULL,
    [AuditEventIsOpen]  BIT              NULL,
    CONSTRAINT [PK_AuditEvent] PRIMARY KEY CLUSTERED ([IDAuditEvent] ASC)
);


CREATE TABLE [dbo].[AutoAuditConfig] (
    [IDAutoAuditConfig] INT           IDENTITY (1, 1) NOT NULL,
    [ObjectType]        NVARCHAR (50) NOT NULL,
    [AuditEventLevel]   INT           NOT NULL,
    [EnableAutoAudit]   BIT           NOT NULL,
    CONSTRAINT [PK_AutoAuditConfig] PRIMARY KEY CLUSTERED ([IDAutoAuditConfig] ASC)
);


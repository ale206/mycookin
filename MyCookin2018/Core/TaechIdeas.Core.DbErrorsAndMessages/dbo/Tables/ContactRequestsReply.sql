CREATE TABLE [dbo].[ContactRequestsReply] (
    [IDContactRequestReply] UNIQUEIDENTIFIER NOT NULL,
    [IDContactRequest]      UNIQUEIDENTIFIER NOT NULL,
    [IDUserWhoReplied]      UNIQUEIDENTIFIER NOT NULL,
    [Reply]                 NVARCHAR (MAX)   NOT NULL,
    [ReplyDate]             DATETIME         NOT NULL,
    [IpAddress]             NVARCHAR (15)    NOT NULL,
    CONSTRAINT [PK_ContactRequestsReply] PRIMARY KEY CLUSTERED ([IDContactRequestReply] ASC)
);


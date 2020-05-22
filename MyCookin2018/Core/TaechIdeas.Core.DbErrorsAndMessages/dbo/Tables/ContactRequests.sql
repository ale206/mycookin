CREATE TABLE [dbo].[ContactRequests] (
    [IDContactRequest]     UNIQUEIDENTIFIER NOT NULL,
    [IDLanguage]           INT              NOT NULL,
    [IDContactRequestType] INT              NOT NULL,
    [FirstName]            NVARCHAR (250)   NOT NULL,
    [LastName]             NVARCHAR (250)   NOT NULL,
    [Email]                NVARCHAR (250)   NOT NULL,
    [RequestText]          NVARCHAR (2000)  NOT NULL,
    [PrivacyAccept]        BIT              NOT NULL,
    [RequestDate]          DATETIME         NOT NULL,
    [IpAddress]            NVARCHAR (15)    NOT NULL,
    [IsRequestClosed]      BIT              NOT NULL,
    CONSTRAINT [PK_ContactRequests] PRIMARY KEY CLUSTERED ([IDContactRequest] ASC)
);


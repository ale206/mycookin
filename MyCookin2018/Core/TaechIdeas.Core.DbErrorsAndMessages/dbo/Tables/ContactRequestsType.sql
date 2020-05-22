CREATE TABLE [dbo].[ContactRequestsType] (
    [IDContactRequestType] INT            IDENTITY (1, 1) NOT NULL,
    [RequestType]          NVARCHAR (150) NOT NULL,
    [RequestTypeAddedOn]   DATETIME       CONSTRAINT [DF_ContactRequestsType_RequestTypeAddedOn] DEFAULT (GETUTCDATE()) NOT NULL,
    [Enabled]              BIT            CONSTRAINT [DF_ContactRequestsType_Enabled] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ContactRequestsType_1] PRIMARY KEY CLUSTERED ([IDContactRequestType] ASC)
);


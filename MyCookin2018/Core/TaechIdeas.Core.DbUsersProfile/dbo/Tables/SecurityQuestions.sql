CREATE TABLE [dbo].[SecurityQuestions] (
    [IDSecurityQuestion]         INT            IDENTITY (1, 1) NOT NULL,
    [SecurityQuestionAdminValue] NVARCHAR (250) NULL,
    [Enabled]                    BIT            NOT NULL,
    [OrderPosition]              INT            CONSTRAINT [DF_SecurityQuestions_OrderPosition] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_SecurityQuestions] PRIMARY KEY CLUSTERED ([IDSecurityQuestion] ASC)
);


CREATE TABLE [dbo].[SecurityQuestionsLanguages] (
    [IDSecurityQuestionLanguage] INT            IDENTITY (1, 1) NOT NULL,
    [IDSecurityQuestion]         INT            NOT NULL,
    [IDLanguage]                 INT            NOT NULL,
    [SecurityQuestion]           NVARCHAR (550) NOT NULL,
    CONSTRAINT [PK_SecurityQuestionsLanguages] PRIMARY KEY CLUSTERED ([IDSecurityQuestionLanguage] ASC),
    CONSTRAINT [FK_SecurityQuestionsLanguages_SecurityQuestions] FOREIGN KEY ([IDSecurityQuestion]) REFERENCES [dbo].[SecurityQuestions] ([IDSecurityQuestion]) ON UPDATE CASCADE
);


CREATE TABLE [dbo].[Users] (
    [IDUser]                  UNIQUEIDENTIFIER CONSTRAINT [DF_Users_IDUser] DEFAULT (newid()) NOT NULL,
    [Name]                    NVARCHAR (250)   NULL,
    [Surname]                 NVARCHAR (250)   NULL,
    [UserName]                NVARCHAR (50)    NOT NULL,
    [UserDomain]              INT              CONSTRAINT [DF_Users_UserDomain] DEFAULT ((0)) NULL,
    [UserType]                INT              NULL,
    [PasswordHash]            NVARCHAR (100)   NOT NULL,
    [LastPasswordChange]      SMALLDATETIME    NULL,
    [PasswordExpireOn]        SMALLDATETIME    NOT NULL,
    [ChangePasswordNextLogon] BIT              CONSTRAINT [DF_Users_ChangePasswordNextLogon] DEFAULT ((0)) NOT NULL,
    [ContractSigned]          BIT              CONSTRAINT [DF_Users_ContractSigned] DEFAULT ((0)) NOT NULL,
    [BirthDate]               SMALLDATETIME    NULL,
    [eMail]                   NVARCHAR (150)   NOT NULL,
    [MailConfirmedOn]         SMALLDATETIME    NULL,
    [Mobile]                  NVARCHAR (50)    NULL,
    [MobileConfirmationCode]  NVARCHAR (50)    NULL,
    [MobileConfirmedOn]       SMALLDATETIME    NULL,
    [IDLanguage]              INT              NOT NULL,
    [IDCity]                  INT              NULL,
    [IDProfilePhoto]          UNIQUEIDENTIFIER NULL,
    [UserEnabled]             BIT              CONSTRAINT [DF_Users_UserEnabled] DEFAULT ((0)) NOT NULL,
    [UserLocked]              BIT              CONSTRAINT [DF_Users_UserLocked] DEFAULT ((0)) NOT NULL,
    [MantainanceMode]         BIT              CONSTRAINT [DF_Users_MantainanceMode] DEFAULT ((0)) NOT NULL,
    [IDSecurityQuestion]      INT              NULL,
    [SecurityAnswer]          NVARCHAR (250)   NULL,
    [DateMembership]          SMALLDATETIME    NOT NULL,
    [AccountExpireOn]         SMALLDATETIME    NOT NULL,
    [LastLogon]               SMALLDATETIME    NULL,
    [LastLogout]              SMALLDATETIME    NULL,
    [Offset]                  INT              CONSTRAINT [DF_Users_Offset] DEFAULT ((0)) NOT NULL,
    [LastProfileUpdate]       SMALLDATETIME    NULL,
    [UserIsOnLine]            BIT              NULL,
    [LastIpAddress]           NVARCHAR (50)    NULL,
    [IDVisibility]            INT              NULL,
    [IDGender]                INT              NULL,
    [AccountDeletedOn]        SMALLDATETIME    NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([IDUser] ASC),
    CONSTRAINT [FK_Users_SecurityQuestions] FOREIGN KEY ([IDSecurityQuestion]) REFERENCES [dbo].[SecurityQuestions] ([IDSecurityQuestion]) ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_MailUnique]
    ON [dbo].[Users]([eMail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_UsernameUnique]
    ON [dbo].[Users]([UserName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Surname_Name]
    ON [dbo].[Users]([Surname] ASC, [Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Name_Surname]
    ON [dbo].[Users]([Name] ASC, [Surname] ASC);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[trDeleteUser] 
   ON  dbo.Users
   INSTEAD OF DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @IDUser uniqueidentifier
	SELECT @IDUser = IDUser from deleted
	DELETE FROM dbo.UsersFriends WHERE IDUser1 = @IDUser OR IDUser2 = @IDUser
	DELETE FROM dbo.UsersFollowers WHERE IDUser = @IDUser OR IDUserFollowed = @IDUser
	DELETE FROM dbo.Users WHERE IDUser = @IDUser
    -- Insert statements for trigger here
END

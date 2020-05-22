-- Batch submitted through debugger: USP_FindUser.sql|0|0|C:\Documents and Settings\zzitibld\Documenti\SQL Server Management Studio\Projects\USP_ActivateUser.sql
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <05/04/2014,,>
-- Last Edit:   <,,>
-- Description:	<Get new Users to update Mailchimp list,,>
-- =============================================
create PROCEDURE [dbo].[USP_GetNewUsersForMailchimp]
	
AS

DECLARE @LastUpdateList datetime

--Select last retrieve date from the Configuration Table
SELECT  @LastUpdateList = ConfigurationValue 
FROM UserConfigurationParameters
WHERE ConfigurationName = 'MailchimpLastUpdateLists'

--Select all users after this date
SELECT eMail, Name, Surname, IDLanguage
FROM Users
WHERE DateMembership > @LastUpdateList

--update Configuration Table
UPDATE UserConfigurationParameters 
SET ConfigurationValue = Convert(nvarchar, GETUTCDATE(), 120)
WHERE ConfigurationName = 'MailchimpLastUpdateLists'

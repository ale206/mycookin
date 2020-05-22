
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <03/02/2013,,>
-- Last Edit: <10/10/2013,,>
-- Description:	<Get Users Board Elements according to IDUserAction,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UsersBoardBlockElement]
	 @IDUserAction uniqueidentifier
	,@IDLanguage int		
AS

BEGIN TRY

SELECT	UAT.IDUserActionType, UAT.UserActionTypeMailNotice, UAT.UserActionTypeMessageMaxLength, 
		UAT.UserActionTypeSiteNotice, UAT.UserActionTypeSmsNotice,
		UATL.IDUserActionTypeLanguage, UATL.UserActionType, UATL.IDLanguage, UATL.UserActionTypeTemplate, UATL.UserActionTypeToolTip,
		UA.IDUser, UA.IDUserActionFather, UA.IDUserActionType, UA.IDActionRelatedObject, 
		UA.UserActionMessage, UA.IDVisibility, UA.UserActionDate
FROM UserActionsTypes UAT
	INNER JOIN UserActionsTypesLanguages UATL ON (UAT.IDUserActionType = UATL.IDUserActionType)
	INNER JOIN UsersActions UA ON (UA.IDUserActionType = UAT.IDUserActionType)
	WHERE  
		UAt.UserActionTypeEnabled = 1
		AND UA.IDUserAction = @IDUserAction
		AND UATL.IDLanguage = @IDLanguage
	ORDER BY UA.UserActionDate DESC
 	
END TRY

BEGIN CATCH
END CATCH

-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <16/02/2013,,>
-- Last Edit: <,,>
-- Description:	<Select Father or Sons to create Users Board,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UsersBoardFatherOrSons]
	@IDUserActionType int,
	@IDUser uniqueidentifier,
	@IDUserActionFather uniqueidentifier,
	@NumberOfResults int,
	@SortOrder nvarchar(20),
	@IDLanguage int
			
AS

BEGIN TRY

 --FATHER
IF @IDUserActionFather IS NULL
	BEGIN
		
		IF @IDUserActionType = 0
			BEGIN
				--ALL FATHERS
				SELECT TOP (@NumberOfResults) UAT.IDUserActionType, UAT.UserActionTypeEnabled, UAT.UserActionTypeMailNotice, UAT.UserActionTypeMessageMaxLength, 
								UAT.UserActionTypeSiteNotice, UAT.UserActionTypeSmsNotice,
								UATL.IDUserActionTypeLanguage, UATL.UserActionType, UATL.UserActionTypeTemplate, UATL.UserActionTypeToolTip,
								UA.IDUser, UA.IDUserAction, UA.IDActionRelatedObject, UA.UserActionMessage, UA.IDVisibility, UA.UserActionDate
							FROM UserActionsTypes UAT
								INNER JOIN UserActionsTypesLanguages UATL ON (UAT.IDUserActionType = UATL.IDUserActionType)
								INNER JOIN UsersActions UA ON (UA.IDUserActionType = UAT.IDUserActionType)
							WHERE  
								UAT.UserActionTypeEnabled = 1
								AND UA.IDUser = @IDUser
								AND UA.DeletedOn IS NULL
								AND UA.IDUserActionFather IS NULL
								AND UATL.IDLanguage = @IDLanguage
							ORDER BY 
								
								CASE @SortOrder
									WHEN 'desc' THEN
										UA.UserActionDate
								END DESC,
								CASE @SortOrder
									WHEN 'asc' THEN
										UA.UserActionDate
								END ASC
			END
		ELSE
			BEGIN
				--ALL FATHER BY ActionType
				SELECT TOP (@NumberOfResults) UAT.IDUserActionType, UAT.UserActionTypeEnabled, UAT.UserActionTypeMailNotice, UAT.UserActionTypeMessageMaxLength, 
								UAT.UserActionTypeSiteNotice, UAT.UserActionTypeSmsNotice,
								UATL.IDUserActionTypeLanguage, UATL.UserActionType, UATL.UserActionTypeTemplate, UATL.UserActionTypeToolTip,
								UA.IDUser, UA.IDUserAction, UA.IDActionRelatedObject, UA.UserActionMessage, UA.IDVisibility, UA.UserActionDate
							FROM UserActionsTypes UAT
								INNER JOIN UserActionsTypesLanguages UATL ON (UAT.IDUserActionType = UATL.IDUserActionType)
								INNER JOIN UsersActions UA ON (UA.IDUserActionType = UAT.IDUserActionType)
							WHERE UAT.IDUserActionType = @IDUserActionType
								AND UAT.UserActionTypeEnabled = 1
								AND UA.IDUser = @IDUser
								AND UA.DeletedOn IS NULL
								AND UA.IDUserActionFather IS NULL
								AND UATL.IDLanguage = @IDLanguage
							ORDER BY 
							
								CASE @SortOrder
										WHEN 'desc' THEN
											UA.UserActionDate
									END DESC,
									CASE @SortOrder
										WHEN 'asc' THEN
											UA.UserActionDate
									END ASC
			END			
	END
ELSE
--SON
	BEGIN
		SELECT TOP (@NumberOfResults) UAT.IDUserActionType, UAT.UserActionTypeEnabled, UAT.UserActionTypeMailNotice, UAT.UserActionTypeMessageMaxLength, 
						UAT.UserActionTypeSiteNotice, UAT.UserActionTypeSmsNotice,
						UATL.IDUserActionTypeLanguage, UATL.UserActionType, UATL.UserActionTypeTemplate, UATL.UserActionTypeToolTip,
						UA.IDUser, UA.IDUserAction, UA.IDActionRelatedObject, UA.UserActionMessage, UA.IDVisibility, UA.UserActionDate
					FROM UserActionsTypes UAT
						INNER JOIN UserActionsTypesLanguages UATL ON (UAT.IDUserActionType = UATL.IDUserActionType)
						INNER JOIN UsersActions UA ON (UA.IDUserActionType = UAT.IDUserActionType)
					WHERE UAT.IDUserActionType = @IDUserActionType
						AND UAT.UserActionTypeEnabled = 1
						AND UATL.IDLanguage = @IDLanguage
						--AND UA.IDUser = @IDUser
						AND UA.DeletedOn IS NULL
						AND UA.IDUserActionFather = @IDUserActionFather
					ORDER BY
					
						CASE @SortOrder
								WHEN 'desc' THEN
									UA.UserActionDate
							END DESC,
							CASE @SortOrder
								WHEN 'asc' THEN
									UA.UserActionDate
							END ASC
		
	END 
 	
END TRY

BEGIN CATCH
END CATCH
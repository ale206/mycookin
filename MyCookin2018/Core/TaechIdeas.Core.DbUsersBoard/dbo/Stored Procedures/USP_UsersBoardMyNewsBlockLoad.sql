
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <15/04/2013,,>
-- Last Edit: <26/04/2013,,>
-- Description:	<Load Following Users Board block (MyNews) for dynamic incremental load,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UsersBoardMyNewsBlockLoad]
	@IDUser uniqueidentifier
	,@SortOrder nvarchar(20)
	,@NumberOfResults int
	,@OtherIDActionsToShow nvarchar(1000)	--Other Actions (such as Likes) that have a Father
	,@IDLanguage int
			
AS

DECLARE @IDUserActionType int = 0
DECLARE @IDUserActionFather uniqueidentifier = null

BEGIN TRY

IF @IDUserActionFather IS NULL
	BEGIN
		
		IF @IDUserActionType = 0
			BEGIN	
				SET NOCOUNT ON;
					SELECT TOP (@NumberOfResults) 
						UAT.IDUserActionType, UAT.UserActionTypeEnabled, UAT.UserActionTypeMailNotice, UAT.UserActionTypeMessageMaxLength, 
						UAT.UserActionTypeSiteNotice, UAT.UserActionTypeSmsNotice,
						UATL.IDUserActionTypeLanguage, UATL.UserActionType, UATL.UserActionTypeTemplate, UATL.UserActionTypeToolTip,
						UA.IDUser, UA.IDUserAction, UA.IDActionRelatedObject, UA.UserActionMessage, UA.IDVisibility, UA.UserActionDate					
					FROM UserActionsTypes UAT
						INNER JOIN UserActionsTypesLanguages UATL ON (UAT.IDUserActionType = UATL.IDUserActionType)
						INNER JOIN UsersActions UA ON (UA.IDUserActionType = UAT.IDUserActionType)
					WHERE  
						UAT.UserActionTypeEnabled = 1
						AND UA.DeletedOn IS NULL
						AND (UA.IDUserActionFather IS NULL
							OR UAT.IDUserActionType IN (SELECT * FROM SDF_SplitString(@OtherIDActionsToShow, ',')))	--Or other actions that have a father
						AND (
							 UA.IDUser = @IDUser OR
						     UA.IDUser IN ( 
											-- PEOPLE THAT USER IS FOLLOWING
											SELECT IDUserFollowed 
											  FROM DBUsersProfile.dbo.UsersFollowers 
											  WHERE IDUser = @IDUser
										  )
						)
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

END TRY

BEGIN CATCH
END CATCH
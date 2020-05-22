
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <24/04/2016,,>
-- Last Edit: <,,>
-- Description:	<Users Board with Pagination>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UsersBoardWithPagination]
	@IDUser uniqueidentifier,
	@offset int,
	@count int,
	@orderBy nvarchar(100),
	@isAscendent bit,			
	@search nvarchar(250)	
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY

	SET NOCOUNT ON;

	WITH Results AS 
	(
		SELECT	
			IDUserActionType, UserActionTypeEnabled, UserActionTypeMailNotice, UserActionTypeMessageMaxLength, 
			UserActionTypeSiteNotice, UserActionTypeSmsNotice,
			IDUserActionTypeLanguage, UserActionType, UserActionTypeTemplate, UserActionTypeToolTip,
			IDUser, IDUserAction, IDActionRelatedObject, UserActionMessage, IDVisibility, UserActionDate,
					
			--FOR ALL SP WITH PAGINATION
			ROW_NUMBER() OVER 
				(	
					--CHANGE ORDERS HERE
					ORDER BY
						CASE WHEN @orderby='UserActionDate' THEN UserActionDate END			
						--CASE WHEN @orderby='CreationDate' THEN CreationDate END,
						--CASE WHEN @orderby='LastUpdate' THEN LastUpdate END
				) as RowNumber,

			--FOR ALL SP WITH PAGINATION
		    COUNT(*) OVER () as TotalCount
		FROM	
		(	
			SELECT
				UAT.IDUserActionType, UAT.UserActionTypeEnabled, UAT.UserActionTypeMailNotice, UAT.UserActionTypeMessageMaxLength, 
				UAT.UserActionTypeSiteNotice, UAT.UserActionTypeSmsNotice,
				UATL.IDUserActionTypeLanguage, UATL.UserActionType, UATL.UserActionTypeTemplate, UATL.UserActionTypeToolTip,
				UA.IDUser, UA.IDUserAction, UA.IDActionRelatedObject, UA.UserActionMessage, UA.IDVisibility, UA.UserActionDate
			FROM UserActionsTypes UAT
						INNER JOIN UserActionsTypesLanguages UATL ON (UAT.IDUserActionType = UATL.IDUserActionType)
						INNER JOIN UsersActions UA ON (UA.IDUserActionType = UAT.IDUserActionType)				
			WHERE 
				UAT.UserActionTypeEnabled = 1
				--AND UATL.IDLanguage = @IDLanguage
				AND UA.IDUser = @IDUser
				AND UA.DeletedOn IS NULL
				AND UA.IDUserActionFather IS NULL
				--and ((RecipeName LIKE '%' + @search + '%') OR (FriendlyId LIKE '%' + @search + '%') )
		) as cpo
	)
	--FOR ALL SP WITH PAGINATION
	SELECT *, COUNT(*) OVER () AS RowsReturned FROM Results
	WHERE
		RowNumber between @offset and (@offset+@count)	
	ORDER BY 
		CASE WHEN @isAscendent = 1 THEN RowNumber END ASC,
		CASE WHEN @isAscendent = 0 THEN RowNumber END DESC

END TRY

BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

  -- There was an error
  IF @@TRANCOUNT > 0
     ROLLBACK

	 SET @ResultExecutionCode = 'RC-ER-9999' --Error in the StoredProcedure
	 SET @isError = 1
	  
	 DECLARE @ErrorNumber nvarchar(MAX)			= ERROR_NUMBER();
	 DECLARE @ErrorSeverity nvarchar(MAX)		= ERROR_SEVERITY();
	 DECLARE @ErrorState nvarchar(MAX)			= ERROR_STATE();
	 DECLARE @ErrorProcedure nvarchar(MAX)		= ERROR_PROCEDURE();
	 DECLARE @ErrorLine nvarchar(MAX)			= ERROR_LINE();
	 DECLARE @ErrorMessage nvarchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime			= GETUTCDATE();
	 DECLARE @ErrorMessageCode nvarchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorMessage
	
	 --SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin nvarchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
																		 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================
END CATCH
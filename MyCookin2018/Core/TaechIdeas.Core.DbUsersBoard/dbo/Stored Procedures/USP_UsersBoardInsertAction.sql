
-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <02/02/2013,,>
-- Last Edit:   <02/11/2013,,>
-- Description:	<Insert Action in Users Board,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_UsersBoardInsertAction]
	@IDUser uniqueidentifier,
	@IDUserActionFather uniqueidentifier,
	@IDUserActionType int,
	@IDActionRelatedObject uniqueidentifier,
	@UserActionMessage nvarchar(MAX),
	@IDVisibility int
	--@UserActionDate DateTime
			
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

DECLARE @IDUserAction uniqueidentifier

BEGIN TRY
	BEGIN TRANSACTION    -- Start the transaction
	
	-- For these @IDUserActionType see USerBoard.cs --> enum ActionTypes
	-- LikeForComment = 3,
	-- PostOnFriendUserBoard = 9,
    -- LikeForPostOnFriendUserBoard = 10,
    -- LikeForNewIngredient = 11,
	-- LikeForPersonalMessage = 12,
	-- LikeForNewRecipe = 13,
	-- LikeForNewFollower = 14,
	-- LikeForProfileUpdate = 15,
	IF (@IDUserActionType IN (3, 9, 10, 11, 12, 13, 14, 15))
		BEGIN
			set @IDUserAction= NEWID()

			INSERT INTO UsersActions (IDUserAction, IDUser, IDUserActionFather, IDUserActionType, IDActionRelatedObject, 
								 UserActionMessage, IDVisibility, UserActionDate)
			  VALUES			(@IDUserAction, @IDUser, @IDUserActionFather, @IDUserActionType, @IDActionRelatedObject,
								 @UserActionMessage, @IDVisibility, GETUTCDATE())
	      
			SET @ResultExecutionCode = 'US-IN-0040' --NEW ACTION INSERTED
			SET @USPReturnValue = @IDUserAction --ID OF THE ACTION
			SET @isError = 0
		END
	ELSE
		BEGIN

			--IF HAS BEEN DELETED, UPDATE WITH NEW INFORMATIONS
			IF EXISTS(
				SELECT	IDUserAction FROM UsersActions 
				WHERE	IDUser = @IDUser AND (IDActionRelatedObject = @IDActionRelatedObject OR IDActionRelatedObject IS NULL) 
						AND UserActionMessage IS NULL 
						AND IDUserActionType = @IDUserActionType
						AND DeletedOn IS NOT NULL
			)
				BEGIN
					--SELECT
					SELECT	@IDUserAction = IDUserAction FROM UsersActions 
					WHERE	IDUser = @IDUser AND (IDActionRelatedObject = @IDActionRelatedObject OR IDActionRelatedObject IS NULL) 
							AND UserActionMessage IS NULL 
							AND IDUserActionType = @IDUserActionType
							AND DeletedOn IS NOT NULL
					
					-- AND UPDATE
					UPDATE UsersActions 
						SET UserActionMessage = @UserActionMessage, 
							UserActionDate = GETUTCDATE(), 
							DeletedOn = NULL   
						WHERE IDUserAction = @IDUserAction
				END
			ELSE
				BEGIN

					IF EXISTS(
						SELECT IDUserAction FROM UsersActions 
						WHERE IDUser = @IDUser AND (IDActionRelatedObject = @IDActionRelatedObject OR IDActionRelatedObject IS NULL) AND UserActionMessage IS NULL AND IDUserActionType = @IDUserActionType
					)
						BEGIN
							SELECT @IDUserAction = IDUserAction FROM UsersActions 
							WHERE IDUser = @IDUser AND (IDActionRelatedObject = @IDActionRelatedObject OR IDActionRelatedObject IS NULL) AND UserActionMessage IS NULL AND IDUserActionType = @IDUserActionType

							UPDATE UsersActions SET UserActionMessage = @UserActionMessage, UserActionDate = GETUTCDATE() 
								WHERE IDUserAction = @IDUserAction
			
							SET @ResultExecutionCode = 'US-IN-0051' --ACTION UPDATED
							SET @USPReturnValue = @IDUserAction		--ID OF THE ACTION
							SET @isError = 0
						END
	
					ELSE
						BEGIN		
							set @IDUserAction= NEWID()
				
							INSERT INTO UsersActions (IDUserAction, IDUser, IDUserActionFather, IDUserActionType, IDActionRelatedObject, 
													 UserActionMessage, IDVisibility, UserActionDate)
								  VALUES			(@IDUserAction, @IDUser, @IDUserActionFather, @IDUserActionType, @IDActionRelatedObject,
													 @UserActionMessage, @IDVisibility, GETUTCDATE())
	      
							SET @ResultExecutionCode = 'US-IN-0040' --NEW ACTION INSERTED
							SET @USPReturnValue = @IDUserAction --ID OF THE ACTION
							SET @isError = 0
						END
				END
		END

-- If we reach here, success!
   COMMIT
   
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
		-- =======================================
		
END TRY

BEGIN CATCH
-- FOR ALL STORED PROCEDURE
-- =======================================

  -- There was an error
  IF @@TRANCOUNT > 0
     ROLLBACK

	 SET @ResultExecutionCode = 'US-ER-9999' --Error in the StoredProcedure
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
	
	 SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
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


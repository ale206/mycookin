
-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,20/05/2014,>
-- Description:	<Description,Manage Recipe Feedback,>
-- =============================================
create PROCEDURE [dbo].[USP_ManageRecipeFeedback]
	@IDRecipeFeedback uniqueidentifier,
	@IDRecipe uniqueidentifier,
	@IDUser uniqueidentifier,
	@FeedbackType int,
	@FeedbackText nvarchar(550),
	@FeedbackDate datetime,
	@isDeleteOperation bit
	
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode varchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue varchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
	IF @isDeleteOperation = 1 AND @FeedbackType = 2 -- Delete only comments
		BEGIN
			DELETE FROM  [dbo].[RecipesFeedbacks]
			WHERE IDRecipeFeedback = @IDRecipeFeedback

			SET @USPReturnValue ='';
		END
	ELSE
		IF @FeedbackType = 1 -- Like feedback
			BEGIN
				IF NOT EXISTS (SELECT IDRecipeFeedback FROM [dbo].[RecipesFeedbacks] WHERE IDRecipe = @IDRecipe AND IDUser=@IDUser AND IDFeedbackType = 1)
					BEGIN
						INSERT INTO [dbo].[RecipesFeedbacks] 
							(IDRecipeFeedback
								,IDRecipe
								,IDUser
								,IDFeedbackType
								,FeedbackText
								,FeedbackDate) 
				
						VALUES (@IDRecipeFeedback
								,@IDRecipe
								,@IDUser
								,@FeedbackType
								,@FeedbackText
								,@FeedbackDate)
						SET @USPReturnValue =@IDRecipeFeedback;
					END
				ELSE
					BEGIN
						DELETE FROM [dbo].[RecipesFeedbacks]
						WHERE IDRecipe = @IDRecipe AND IDUser=@IDUser AND IDFeedbackType = 1

						SET @USPReturnValue ='';
					END
			END
		ELSE -- Comment feedback
			BEGIN
				INSERT INTO [dbo].[RecipesFeedbacks] 
							(IDRecipeFeedback
								,IDRecipe
								,IDUser
								,IDFeedbackType
								,FeedbackText
								,FeedbackDate) 
				
						VALUES (@IDRecipeFeedback
								,@IDRecipe
								,@IDUser
								,@FeedbackType
								,@FeedbackText
								,@FeedbackDate)
				SET @USPReturnValue =@IDRecipeFeedback;
			END
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='';
		
					                            
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

	 SET @ResultExecutionCode = 'RC-ER-0011' --Error in the StoredProcedure
	 SET @isError = 1
	  
	 DECLARE @ErrorNumber varchar(MAX)		= ERROR_NUMBER();
	 DECLARE @ErrorSeverity varchar(MAX)	= ERROR_SEVERITY();
	 DECLARE @ErrorState varchar(MAX)		= ERROR_STATE();
	 DECLARE @ErrorProcedure varchar(MAX)	= ERROR_PROCEDURE();
	 DECLARE @ErrorLine varchar(MAX)		= ERROR_LINE();
	 DECLARE @ErrorMessage varchar(MAX)		= ERROR_MESSAGE();
	 DECLARE @DateError smalldatetime		= GETUTCDATE();
	 DECLARE @ErrorMessageCode varchar(MAX)	= @ResultExecutionCode;

	 SET @USPReturnValue = @ErrorMessage
	
	 SELECT @ResultExecutionCode AS ResultExecutionCode, @USPReturnValue AS USPReturnValue, @isError AS isError
 
	 --WRITE THE NAME OF FILE ORIGIN, NECESSARY IF NOT A STORED PROCEDURE
	 DECLARE @FileOrigin varchar(150) = '';
	 
	 EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														@FileOrigin, @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
															 
	 -- OPTIONAL: THIS SHOW THE ERROR
	 -- RAISERROR(@ErrMsg, @ErrSeverity, 1)

-- =======================================    
END CATCH









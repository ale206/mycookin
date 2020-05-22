
-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,04/05/2013,>
-- Description:	<Description,Manage Recipe Vote,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageRecipeVote]
	@IDRecipeVote uniqueidentifier,
	@IDRecipe uniqueidentifier,
	@IDUser uniqueidentifier,
	@RecipeVoteDate smalldatetime,
	@RecipeVote float,
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
   
	IF @isDeleteOperation = 1 OR @RecipeVote=0
		BEGIN
			DELETE FROM  [dbo].[RecipesVotes]
			WHERE IDRecipe = @IDRecipe AND IDUser=@IDUser
		END
	ELSE
		BEGIN
			IF NOT EXISTS (SELECT IDRecipeVote FROM [dbo].[RecipesVotes] WHERE IDRecipe = @IDRecipe AND IDUser=@IDUser)
				BEGIN
					INSERT INTO [dbo].[RecipesVotes] 
						(IDRecipeVote
							,IDRecipe
							,IDUser
							,RecipeVoteDate
							,RecipeVote) 
				
					VALUES (@IDRecipeVote
							,@IDRecipe
							,@IDUser
							,@RecipeVoteDate
							,@RecipeVote)
				END
			ELSE
				BEGIN
					UPDATE [dbo].[RecipesVotes] 
					SET RecipeVoteDate = @RecipeVoteDate,
						RecipeVote = @RecipeVote
					WHERE IDRecipe = @IDRecipe AND IDUser=@IDUser
				END
		END
		
		DECLARE @avgRate FLOAT;
		DECLARE @numRate INT;

		SELECT @avgRate=AVG([RecipeVote]) FROM [dbo].[RecipesVotes] WITH (INDEX(IX_IDRecipe))
			WHERE [IDRecipe] = @IDRecipe

		SELECT @numRate=COUNT(IDRecipeVote) FROM [dbo].[RecipesVotes] WITH (INDEX(IX_IDRecipe))
			WHERE [IDRecipe] = @IDRecipe

		UPDATE Recipes
		SET [RecipeAvgRating] = COALESCE(@avgRate,0), RecipeRated = @numRate
		WHERE [IDRecipe] = @IDRecipe
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='';
		SET @USPReturnValue =@IDRecipeVote;
					                            
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

	 SET @ResultExecutionCode = 'RC-ER-0008' --Error in the StoredProcedure
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









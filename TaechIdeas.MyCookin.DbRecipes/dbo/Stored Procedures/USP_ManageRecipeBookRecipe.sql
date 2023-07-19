





-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,27/09/2013,>
-- Description:	<Description,Manage Recipe into User RecipeBook,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_ManageRecipeBookRecipe]
	@IDRecipeBookRecipe uniqueidentifier,
	@IDUser uniqueidentifier,
	@IDRecipe uniqueidentifier,
	@RecipeAddedOn smalldatetime,
	@RecipeOrder int,
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
   
	IF @isDeleteOperation = 1
		BEGIN
			DELETE FROM  [dbo].[RecipesBooksRecipes]
			WHERE IDUser = @IDUser AND IDRecipe = @IDRecipe
		END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT IDRecipeBookRecipe FROM RecipesBooksRecipes WHERE IDUser = @IDUser AND IDRecipe = @IDRecipe)
			BEGIN
				INSERT INTO [dbo].RecipesBooksRecipes 
					(IDRecipeBookRecipe
					   ,IDUser
					   ,IDRecipe
					   ,RecipeAddedOn
					   ,RecipeOrder) 
				
				VALUES (@IDRecipeBookRecipe
					   ,@IDUser
					   ,@IDRecipe
					   ,@RecipeAddedOn
					   ,@RecipeOrder)
			END
		ELSE
			BEGIN
				UPDATE [DBRecipes].dbo.RecipesBooksRecipes
				   SET IDUser=@IDUser,
						IDRecipe=@IDRecipe,
						RecipeAddedOn=@RecipeAddedOn,
						RecipeOrder=@RecipeOrder
				 WHERE  IDUser = @IDUser AND IDRecipe = @IDRecipe
			END
	END
	
	   -- If we reach here, success!
		SET @isError = 0
		SET @ResultExecutionCode ='RC-IN-0005';
		SET @USPReturnValue ='';
					                            
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

	 SET @ResultExecutionCode = 'RC-ER-0010' --Error in the StoredProcedure
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








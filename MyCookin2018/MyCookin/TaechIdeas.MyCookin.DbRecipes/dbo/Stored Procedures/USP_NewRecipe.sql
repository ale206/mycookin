-- ================================================
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <26/01/2016,,>
-- Last Edit:   <,,>
-- Description:	<Insert New Recipe,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_NewRecipe]
    @IDLanguage int,
	@IDRecipeImage uniqueidentifier,
	@RecipeName nvarchar(250),
	@IDOwner uniqueidentifier
AS

-- FOR ALL STORED PROCEDURE
-- =======================================
DECLARE @ResultExecutionCode nvarchar(MAX);
DECLARE @isError bit;
DECLARE @USPReturnValue nvarchar(MAX);
-- =======================================

BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
				
	DECLARE @NewRecipeGuid uniqueidentifier
	set @NewRecipeGuid= NEWID()

	DECLARE @NewRecipeLanguageGuid uniqueidentifier
	set @NewRecipeLanguageGuid= NEWID()
		
	--Insert in Recipe table	
	INSERT INTO Recipes
				(IDRecipe, IDRecipeImage, IDOwner, CreationDate, RecipeAvgRating, isStarterRecipe, BaseRecipe, RecipeEnabled)
    VALUES
           (@NewRecipeGuid, @IDRecipeImage, @IDOwner, GETUTCDATE(), 0, 0, 0, 1)

	--Insert in RecipeLanguages table
	INSERT INTO dbo.RecipesLanguages
           (IDRecipeLanguage, IDRecipe, IDLanguage, RecipeName, RecipeLanguageAutoTranslate, RecipeDisabled)
    VALUES
           (@NewRecipeLanguageGuid, @NewRecipeGuid, @IDLanguage, @RecipeName, 0, 0)

	-- If we reach here, success!
   COMMIT

   -- Create FriendlyId calling SP
	DECLARE @FriendlyId nvarchar(500)

	--Use of a Temp Table to avoid return of this Called-Stored Procedure in this one
	CREATE TABLE #Temp1 ( FriendlyId nvarchar( 500 ) )

	INSERT INTO #Temp1 EXECUTE @FriendlyId = USP_GenerateFriendlyId @NewRecipeLanguageGuid
	
	--Return objects
	SELECT @NewRecipeGuid AS RecipeId, @NewRecipeLanguageGuid AS RecipeLanguageId, ( SELECT FriendlyId FROM #Temp1 ) AS FriendlyId
		
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

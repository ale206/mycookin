
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <15/05/2016>
-- Description:	<Search Recipes With Empty Fridge Function>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SearchRecipesWithEmptyFridge]
	@IDLanguage INT,
	@Vegan BIT,
	@Vegetarian BIT,
	@GlutenFree BIT,
	@LightThreshold FLOAT,
	@QuickThreshold INT,
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
	
	--TODO: IMPROVE THIS! THE ONLY CHANGE IS WHICH VIEW MUST BE USED

	--Table for Ingredient IDs to find in recipes
DECLARE @IngrIDsList TABLE
(
	IDIngredient UNIQUEIDENTIFIER,
	IngredientName NVARCHAR(150),
	IngrOrigName NVARCHAR(150)
) 

--Cycle to Search Ingredient IDs
DECLARE IngredientToSearch CURSOR
FOR
	--INGREDIENTS SEPARED BY A COMMA
	SELECT LTRIM(RTRIM(StringPart)) FROM SplitString(@search,',')

	DECLARE @Ingr NVARCHAR(50)

	OPEN IngredientToSearch
	FETCH NEXT FROM IngredientToSearch INTO @Ingr
		WHILE(@@FETCH_STATUS<>-1)
			BEGIN
				-- USE FULLTEXT ??
				INSERT INTO @IngrIDsList
					SELECT I.IDIngredient, IL.IngredientSingular, @Ingr AS IngrOrigName  
						FROM Ingredients I INNER JOIN IngredientsLanguages IL
						ON I.IDIngredient = IL.IDIngredient
						WHERE 
							IL.IDLanguage = @IDLanguage AND
							(
								IngredientSingular = @Ingr	-- Exact Name for Singular
								OR IngredientPlural = @Ingr		-- Exact Name for Plural
								OR IngredientSingular LIKE '%'+ @Ingr +'%'	-- Similar Name for Singular
								OR IngredientPlural LIKE '%'+ @Ingr +'%'		-- Similar Name for Plural
								OR IngredientSingular LIKE @Ingr +'%'			-- Starting Name for Singular
								OR IngredientPlural LIKE @Ingr +'%'				-- Starting Name for Plural
							)
							-- ADD INGR ALTERNATIVES ??
				FETCH NEXT FROM IngredientToSearch INTO @Ingr
			END
	
	CLOSE IngredientToSearch

	DEALLOCATE IngredientToSearch

--Table for UNIQUE Ingredient IDs 
DECLARE @IngrIDsListDistinct TABLE
(
	IDIngredient UNIQUEIDENTIFIER,
	IngredientName NVARCHAR(150),
	IngrOrigName NVARCHAR(150)
) 

--Select Distinct IDs
INSERT INTO @IngrIDsListDistinct
	SELECT DISTINCT IDIngredient, IngredientName, IngrOrigName FROM @IngrIDsList;


--RECIPES		
			WITH Results AS 
				(
					SELECT NumSearchedIngr, NumIngr, IDRecipeLanguage, IDRecipe, RecipeAvgRating, RecipeName, IDRecipeImage, FriendlyId,
			
					--FOR ALL SP WITH PAGINATION
					ROW_NUMBER() OVER 
						(	
							--CHANGE ORDERS HERE
							ORDER BY NumSearchedIngr DESC, NumIngr ASC, RecipeAvgRating DESC
						) as RowNumber,

					--FOR ALL SP WITH PAGINATION
					COUNT(*) OVER () as TotalCount
				FROM	
				(	
					SELECT TOP 36	NumSearchedIngr, COUNT(RI.IDRecipeIngredient) AS NumIngr,
									RL.RecipeName, RL.FriendlyId, RL.IDRecipeLanguage, RL.IDRecipe, 
									R.RecipeAvgRating, R.IDRecipeImage
					FROM
					(
						SELECT IDRecipeLanguage, IDRecipe, COUNT(IngrOrigName) AS NumSearchedIngr 
						FROM
						(
							SELECT RL.IDRecipeLanguage, RL.IDRecipe,
							IngrOrigName
							FROM	@IngrIDsListDistinct tIngr 
										INNER JOIN Ingredients I ON tIngr.IDIngredient = I.IDIngredient 
										INNER JOIN RecipesIngredients RI ON I.IDIngredient = RI.IDIngredient 
										INNER JOIN Recipes R ON RI.IDRecipe = R.IDRecipe 
										INNER JOIN RecipesLanguages RL ON R.IDRecipe = RL.IDRecipe
							WHERE	RecipeEnabled = 1
									AND Draft = 0
									AND (([PreparationTimeMinute] + [CookingTimeMinute]) BETWEEN 0 AND @QuickThreshold )
									AND [RecipePortionKcal] BETWEEN 0 AND @LightThreshold
									AND (vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 1 END
										OR vegan = CASE  WHEN @vegan = 1 THEN 1 ELSE 0 END
										OR isnull(vegan,0)=@vegan)
									AND (Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 1 END
										OR Vegetarian = CASE  WHEN @Vegetarian = 1 THEN 1 ELSE 0 END
										OR isnull(Vegetarian,0)=@Vegetarian)
									AND (GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 1 END
										OR GlutenFree = CASE  WHEN @GlutenFree = 1 THEN 1 ELSE 0 END
										OR isnull(GlutenFree,0)=@GlutenFree)
									AND (IDRecipeFather IS NULL OR IDRecipeFather='00000000-0000-0000-0000-000000000000')
							GROUP BY	RL.IDRecipeLanguage, RL.IDRecipe, RL.RecipeName, RL.FriendlyId, 
										R.RecipeAvgRating, R.IDRecipeImage,
										IngrOrigName
							) t1
							GROUP BY IDRecipeLanguage, IDRecipe
							) t2	INNER JOIN RecipesIngredients RI ON RI.IDRecipe = t2.IDRecipe
									INNER JOIN RecipesLanguages RL ON t2.IDRecipe = RL.IDRecipe
									INNER JOIN Recipes R ON t2.IDRecipe = R.IDRecipe 
							GROUP BY	NumSearchedIngr,
										RL.RecipeName, RL.FriendlyId, RL.IDRecipeLanguage, RL.IDRecipe, 
										R.RecipeAvgRating, R.IDRecipeImage
							ORDER BY NumSearchedIngr DESC, NumIngr ASC, RecipeAvgRating DESC
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
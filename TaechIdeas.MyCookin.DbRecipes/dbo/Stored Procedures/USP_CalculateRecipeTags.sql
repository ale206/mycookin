-- ================================================
-- =============================================
-- Author:		<Author,,Cammarata Saverio>
-- Create date: <Create Date,29/04/2013,>
-- Description:	<Description,Calculate Recipe Tags,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CalculateRecipeTags]
	@IDRecipe uniqueidentifier,
	@IDLanguage int,
	@AddIngredientList bit,
	@AddDynamicPropertyList bit,
	@IncludeIngredientCategory bit
AS

DECLARE @RecipeTagList nvarchar(500)


BEGIN TRY
   BEGIN TRANSACTION    -- Start the transaction
   
	IF @AddIngredientList = 1
		BEGIN
			SELECT @RecipeTagList = COALESCE(@RecipeTagList + ', ','') + [IngredientSingular]
			FROM [dbo].[IngredientsLanguages] INNER JOIN [dbo].[RecipesIngredients]
			ON [dbo].[IngredientsLanguages].IDIngredient = [dbo].[RecipesIngredients].IDIngredient
			WHERE IDRecipe=@IDRecipe AND IDLanguage = @IDLanguage
		END
	IF @AddDynamicPropertyList = 1
		BEGIN
			SELECT @RecipeTagList = COALESCE(@RecipeTagList + ', ','') + [RecipeProperty] + ', ' + [RecipePropertyToolTip]
			FROM [dbo].[RecipePropertiesLanguages] INNER JOIN [dbo].[RecipesPropertiesValues]
			ON [dbo].[RecipePropertiesLanguages].IDRecipeProperty = [dbo].[RecipesPropertiesValues].IDRecipeProperty
			WHERE IDRecipe=@IDRecipe AND IDLanguage = @IDLanguage AND Value=1
		END
	IF @IncludeIngredientCategory = 1
		BEGIN
			DECLARE @IngredientCategoies TABLE
			(
			  IDIngrCategory int,
			  IDIngrCategoryFather int,
			  Category nvarchar(150)
			)

			INSERT INTO @IngredientCategoies
			EXEC [dbo].[USP_GetAllCategoryByIDLanguageIDFatherCategory]
					@IDLanguage = @IDLanguage,
					@IDFatherCategory = 24,
					@MaxDepth = 10

			SELECT @RecipeTagList = COALESCE(@RecipeTagList + ', ','') + [Category]
			FROM [dbo].[RecipesIngredients] INNER JOIN [dbo].[IngredientsIngredientsCategories] 
			ON [dbo].[RecipesIngredients].IDIngredient=[dbo].[IngredientsIngredientsCategories].IDIngredient 
			INNER JOIN [dbo].[IngredientsCategoriesLanguages] 
			ON [dbo].[IngredientsIngredientsCategories].IDIngredientCategory=[dbo].[IngredientsCategoriesLanguages].IDIngredientCategory 
			INNER JOIN @IngredientCategoies as t
			ON t.IDIngrCategory = [dbo].[IngredientsCategoriesLanguages].IDIngredientCategory 
			WHERE IDRecipe=@IDRecipe AND IDLanguage = @IDLanguage
		END
					                            
   COMMIT
		
		-- FOR ALL STORED PROCEDURE
		-- =======================================
		SELECT 'RecipeTags' AS PropKey, @RecipeTagList AS PropValue
		-- =======================================
		
END TRY

BEGIN CATCH
 
END CATCH













-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 19/08/2012>
-- Description:	<Description, [USP_GetNoFatherRecipeByRecipeNameContains]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetRecipeFatherByRecipeNameContains](@RecipeName nvarchar(100),@IDRecipe uniqueidentifier, @IDLanguage INT)
AS
BEGIN
DECLARE @LanguageCode nvarchar(5);
DECLARE @Sql nvarchar(MAX);
DECLARE @PartialRecipeName nvarchar(MAX);
	
	SELECT @LanguageCode = CASE @IDLanguage
		WHEN 1 THEN 'En'
		WHEN 2 THEN 'It'
		WHEN 3 THEN 'Es'
		ELSE 'En'
	END
	
	SET @PartialRecipeName = SUBSTRING ( @RecipeName ,1 ,CONVERT(int,LEN(@RecipeName)/2) )
	
	BEGIN TRY
		
		SET @Sql = 'SELECT NEWID() AS IDRecipeLanguage, ''00000000-0000-0000-0000-000000000000'' AS IDRecipe, ''Select Value'' AS RecipeName 
					UNION
					SELECT IDRecipeLanguage, IDRecipe, RecipeName FROM dbo.vIndexedRecipeLang_'+@LanguageCode+'
					WHERE RecipeName LIKE '''+@PartialRecipeName+'%'' AND IDRecipeFather IS NULL AND IDRecipe <> '''+CONVERT(nvarchar(50),@IDRecipe)+'''
					UNION
					SELECT IDRecipeLanguage, IDRecipe, RecipeName FROM dbo.vIndexedRecipeLang_'+@LanguageCode+'
					WHERE CONTAINS(RecipeName,''"'+@RecipeName+'"'') AND IDRecipeFather IS NULL AND IDRecipe <> '''+CONVERT(nvarchar(50),@IDRecipe)+'''
					UNION
					SELECT TOP 20 IDRecipeLanguage, IDRecipe, RecipeName FROM dbo.vIndexedRecipeLang_'+@LanguageCode+'
					WHERE FREETEXT(RecipeName,'''+@RecipeName+''') AND IDRecipeFather IS NULL AND IDRecipe <> '''+CONVERT(nvarchar(50),@IDRecipe)+'''
					ORDER BY RecipeName'
		EXECUTE sp_executesql @Sql
	END TRY
	
	BEGIN CATCH
		SELECT NEWID() AS IDRecipeLanguage, '00000000-0000-0000-0000-000000000000' AS IDRecipe, 'Select Value' AS RecipeName
		UNION
		SELECT IDRecipeLanguage, Recipes.IDRecipe,RecipeName
		FROM dbo.RecipesLanguages INNER JOIN dbo.Recipes ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
		WHERE RecipeName LIKE @PartialRecipeName+'%' AND RecipeDisabled = 0 AND IDLanguage = @IDLanguage
				AND Recipes.IDRecipeFather IS NULL AND Recipes.IDRecipe <> @IDRecipe AND RecipeEnabled=1
		UNION
		SELECT IDRecipeLanguage, Recipes.IDRecipe,RecipeName
		FROM dbo.RecipesLanguages INNER JOIN dbo.Recipes ON Recipes.IDRecipe = RecipesLanguages.IDRecipe
		WHERE  RecipeName LIKE '%'+@RecipeName+'%' AND RecipeDisabled = 0 AND IDLanguage = @IDLanguage
				AND Recipes.IDRecipeFather IS NULL AND Recipes.IDRecipe <> @IDRecipe AND RecipeEnabled=1
		ORDER BY RecipeName
		
		
		--ERROR CATCH
		 DECLARE @ErrorNumber varchar(MAX)		= ERROR_NUMBER();
		 DECLARE @ErrorSeverity varchar(MAX)	= ERROR_SEVERITY();
		 DECLARE @ErrorState varchar(MAX)		= ERROR_STATE();
		 DECLARE @ErrorProcedure varchar(MAX)	= ERROR_PROCEDURE();
		 DECLARE @ErrorLine varchar(MAX)		= ERROR_LINE();
		 DECLARE @ErrorMessage varchar(MAX)		= ERROR_MESSAGE();
		 DECLARE @DateError smalldatetime		= GETUTCDATE();
		 DECLARE @ErrorMessageCode varchar(MAX)	= 'Possible Error on FULL TEXT INDEX';

		EXECUTE DBErrorsAndMessages.dbo.USP_InsertErrorLog	@ErrorNumber, @ErrorSeverity, @ErrorState, 
														@ErrorProcedure, @ErrorLine, @ErrorMessage,
														'USP_GetNoFatherRecipeByRecipeNameContains', @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
	END CATCH
	
END









-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 27/04/2013>
-- Description:	<Description, [USP_SearchRecipePropertiesNameContains]>
-- =============================================
CREATE PROCEDURE [dbo].[USP_SearchRecipePropertiesNameContains](@IDRecipePropertyType INT, @IDLanguage INT,@RecipeProperty nvarchar(100) )
AS
BEGIN
DECLARE @Sql nvarchar(MAX);
DECLARE @Filter nvarchar(150);

SET @Filter ='"'+@RecipeProperty+'*"'	

	BEGIN TRY
		
		SELECT [IDRecipePropertyLanguage]
		  ,[IDRecipeProperty]
		  ,[IDRecipePropertyType]
		  ,[OrderPosition]
		  ,[Enabled]
		  ,[IDLanguage]
		  ,[RecipeProperty]
		  ,[RecipePropertyToolTip]
		  FROM [DBRecipes].[dbo].[vRecipeProperty]
		  WHERE CONTAINS((RecipeProperty,RecipePropertyToolTip),@Filter)
				AND IDLanguage=@IDLanguage
				AND IDRecipePropertyType=@IDRecipePropertyType
		  ORDER BY IDRecipePropertyType, OrderPosition, IDLanguage
	END TRY
	
	BEGIN CATCH
		
		SELECT [IDRecipePropertyLanguage]
		  ,[IDRecipeProperty]
		  ,[IDRecipePropertyType]
		  ,[OrderPosition]
		  ,[Enabled]
		  ,[IDLanguage]
		  ,[RecipeProperty]
		  ,[RecipePropertyToolTip]
		  FROM [DBRecipes].[dbo].[vRecipeProperty]
		  WHERE (RecipeProperty LIKE @RecipeProperty+'%' 
					OR RecipePropertyToolTip LIKE '%'+@RecipeProperty+'%')
				AND IDLanguage=@IDLanguage
				AND IDRecipePropertyType=@IDRecipePropertyType
		
		
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
														'USP_GetIngredientByIngredientNameContains', @DateError, @ErrorMessageCode,
														1,0,NULL,0,0
	END CATCH
	
END





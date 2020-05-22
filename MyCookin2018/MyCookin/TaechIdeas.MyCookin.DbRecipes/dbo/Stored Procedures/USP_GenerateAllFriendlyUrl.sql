-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <07/12/2015>
-- Description:	<Generate FriendlyUrl for all the recipes that doesn't have one>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GenerateAllFriendlyUrl]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   -- DECLARE @friendlyName nvarchar(500)
	DECLARE @numberOfSameRecipes int

	DECLARE @FriendlyId nvarchar(500)
	DECLARE @RecipeName nvarchar(500)
	DECLARE @IDRecipeLanguage uniqueidentifier

	DECLARE MY_CURSOR CURSOR 
	  LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR 
		SELECT IDRecipeLanguage			
		FROM [dbo].[RecipesLanguages]
		WHERE FriendlyId IS NULL

		DECLARE @i int = 0

	OPEN MY_CURSOR
	FETCH NEXT FROM MY_CURSOR INTO @IDRecipeLanguage
	WHILE @@FETCH_STATUS = 0
	BEGIN 

		  SELECT @RecipeName = RecipeName, @FriendlyId = [dbo].urlencode(RecipeName)
		  FROM [dbo].[RecipesLanguages]
		  WHERE IDRecipeLanguage = @IDRecipeLanguage

		  SELECT @numberOfSameRecipes = COUNT(RecipeName) 
		  FROM [dbo].[RecipesLanguages] 
		  WHERE [RecipeName] = @RecipeName AND IDRecipeLanguage <> @IDRecipeLanguage

		  IF @numberOfSameRecipes > 0
			BEGIN
				DECLARE @NumberOfRecipes int
				SELECT @NumberOfRecipes = COUNT(1) FROM [dbo].[RecipesLanguages]
				SET @FriendlyId += '-' + CONVERT(varchar, @i)
			END

			 UPDATE [dbo].[RecipesLanguages] SET [FriendlyId] = @FriendlyId WHERE IDRecipeLanguage = @IDRecipeLanguage

			SELECT @numberOfSameRecipes = COUNT(FriendlyId) 
			FROM [dbo].[RecipesLanguages] 
			WHERE [FriendlyId] = @FriendlyId AND IDRecipeLanguage <> @IDRecipeLanguage
			
			IF @numberOfSameRecipes > 0
			BEGIN
			  SET @FriendlyId += '-' + CONVERT(varchar, @i)
			  UPDATE [dbo].[RecipesLanguages] SET [FriendlyId] = @FriendlyId WHERE IDRecipeLanguage = @IDRecipeLanguage
			END

			SET @i = @i + 1

		FETCH NEXT FROM MY_CURSOR INTO @IDRecipeLanguage
	END
	CLOSE MY_CURSOR
	DEALLOCATE MY_CURSOR



END
-- =============================================
-- Author:		<Alessio Di Salvo>
-- Create date: <07/12/2015>
-- Description:	<Generate FriendlyUrl for all the Ingredients that doesn't have one>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GenerateAllIngredientFriendlyUrl]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   -- DECLARE @friendlyName nvarchar(500)
	DECLARE @numberOfSameIngredients int

	DECLARE @FriendlyId nvarchar(500)
	DECLARE @IngredientName nvarchar(500)
	DECLARE @IDIngredientLanguage uniqueidentifier

	DECLARE MY_CURSOR CURSOR 
	  LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR 
		SELECT IDIngredientLanguage			
		FROM [dbo].[IngredientsLanguages]
		WHERE FriendlyId IS NULL

		DECLARE @i int = 0

	OPEN MY_CURSOR
	FETCH NEXT FROM MY_CURSOR INTO @IDIngredientLanguage
	WHILE @@FETCH_STATUS = 0
	BEGIN 

		  SELECT @IngredientName = IngredientSingular, @FriendlyId = [dbo].urlencode(IngredientSingular)
		  FROM [dbo].[IngredientsLanguages]
		  WHERE IDIngredientLanguage = @IDIngredientLanguage

		  SELECT @numberOfSameIngredients = COUNT(IngredientSingular) 
		  FROM [dbo].[IngredientsLanguages] 
		  WHERE [IngredientSingular] = @IngredientName AND IDIngredientLanguage <> @IDIngredientLanguage

		  IF @numberOfSameIngredients > 0
			BEGIN
				DECLARE @NumberOfIngredients int
				SELECT @NumberOfIngredients = COUNT(1) FROM [dbo].[IngredientsLanguages]
				SET @FriendlyId += '-' + CONVERT(varchar, @i)
			END

			 UPDATE [dbo].[IngredientsLanguages] SET [FriendlyId] = @FriendlyId WHERE IDIngredientLanguage = @IDIngredientLanguage

			SELECT @numberOfSameIngredients = COUNT(FriendlyId) 
			FROM [dbo].[IngredientsLanguages] 
			WHERE [FriendlyId] = @FriendlyId AND IDIngredientLanguage <> @IDIngredientLanguage
			
			IF @numberOfSameIngredients > 0
			BEGIN
			  SET @FriendlyId += '-' + CONVERT(varchar, @i)
			  UPDATE [dbo].[IngredientsLanguages] SET [FriendlyId] = @FriendlyId WHERE IDIngredientLanguage = @IDIngredientLanguage
			END

			SET @i = @i + 1

		FETCH NEXT FROM MY_CURSOR INTO @IDIngredientLanguage
	END
	CLOSE MY_CURSOR
	DEALLOCATE MY_CURSOR



END
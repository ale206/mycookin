
-- =============================================
-- Author:		<Author, Cammarata Saverio>
-- Create date: <Create Date, 10/07/2012>
-- Description:	<Description, USP_GetAllCategoryByIDLanguage_Plus>
-- =============================================
CREATE PROCEDURE [dbo].[USP_GetAllCategoryByIDLanguageIDFatherCategory](@IDLanguage INT, @IDFatherCategory INT, @MaxDepth INT)
AS
BEGIN
----============Variable for table column========
	DECLARE @IDIngredientCategory INT
	DECLARE @IDIngredientCategoryFather INT
	DECLARE @IngredientCategoryLanguage VARCHAR(100)
	DECLARE @Depth INT

----============Variable for SP Data=============	
	DECLARE @IngredientCategoryStringSeparator VARCHAR(10)
	SET @IngredientCategoryStringSeparator = ' -> '
	DECLARE @TempIngredientCategoryLanguageStringCostructor VARCHAR(500)
	DECLARE @IngredientCategoryTempWork TABLE
			(
			  IDIngredientCategory int,
			  IDIngredientCategoryFather int,
			  IngredientCategoryLanguage VARCHAR(100)
			)
	DECLARE @ChekIDLanguageExist INT
	
	DECLARE @IDFatherTEMP INT
	DECLARE @IDCategoryTEMP INT
----=============================================
	SET @TempIngredientCategoryLanguageStringCostructor = ''
	--Controllo se ho valori per la lingua selezionata
	SELECT TOP 1 @ChekIDLanguageExist = IDIngredientCategoryLanguage 
		FROM dbo.IngredientsCategoriesLanguages
		WHERE IDLanguage = @IDLanguage
	
	--Se la lingua non esiste prendo quella minore
	IF @ChekIDLanguageExist IS NULL
	BEGIN
		SELECT @IDLanguage = MIN(IDLanguage) 
			FROM dbo.IngredientsCategoriesLanguages
	END
	
	DECLARE cur_ReadCategory Cursor
	FOR 
	
		SELECT Res.IDIngredientCategory,Res.IDIngredientCategoryFather, IngredientsCategoriesLanguages.IngredientCategoryLanguage, Res.Depth 
			FROM UDF_GetAllSbucategoryFromFather(@IDFatherCategory) Res
			INNER JOIN dbo.IngredientsCategoriesLanguages
			ON Res.IDIngredientCategory = IngredientsCategoriesLanguages.IDIngredientCategory
			WHERE IngredientsCategoriesLanguages.IDLanguage=@IDLanguage AND Res.Enabled = 1 AND Res.Depth <= @MaxDepth
			ORDER BY Res.Depth

	Open cur_ReadCategory
				
	FETCH NEXT FROM cur_ReadCategory INTO @IDIngredientCategory, @IDIngredientCategoryFather, @IngredientCategoryLanguage, @Depth
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			--Corpo USP
			
			IF @IDIngredientCategory = @IDFatherCategory 
				BEGIN
					--Se l'id padre non è un primitivo ricostruisco l'alberatura sopra
					SET @IDFatherTEMP = @IDIngredientCategoryFather
					WHILE (@IDFatherTEMP IS NOT NULL)
					BEGIN
							SELECT @TempIngredientCategoryLanguageStringCostructor = IngredientCategoryLanguage + @IngredientCategoryStringSeparator + @TempIngredientCategoryLanguageStringCostructor, @IDCategoryTEMP = IDIngredientCategoryFather
								FROM dbo.vIngredientCategoryComplete
								WHERE IDIngredientCategory = @IDFatherTEMP AND IDLanguage = @IDLanguage
							SET @IDFatherTEMP = @IDCategoryTEMP
					END
					INSERT INTO @IngredientCategoryTempWork(IDIngredientCategory,IDIngredientCategoryFather,IngredientCategoryLanguage)
					VALUES(@IDIngredientCategory,@IDIngredientCategoryFather,@TempIngredientCategoryLanguageStringCostructor + @IngredientCategoryLanguage)

				END
			ELSE
				BEGIN
					SELECT @TempIngredientCategoryLanguageStringCostructor = IngredientCategoryLanguage
						FROM @IngredientCategoryTempWork
						WHERE IDIngredientCategory = @IDIngredientCategoryFather
						
					SET @TempIngredientCategoryLanguageStringCostructor = @TempIngredientCategoryLanguageStringCostructor + @IngredientCategoryStringSeparator + @IngredientCategoryLanguage
					
					INSERT INTO @IngredientCategoryTempWork(IDIngredientCategory,IDIngredientCategoryFather,IngredientCategoryLanguage)
					VALUES(@IDIngredientCategory,@IDIngredientCategoryFather,@TempIngredientCategoryLanguageStringCostructor)
				END
			--FINE
		
			FETCH NEXT FROM cur_ReadCategory INTO @IDIngredientCategory, @IDIngredientCategoryFather, @IngredientCategoryLanguage, @Depth 
		END
		CLOSE cur_ReadCategory
		DEALLOCATE cur_ReadCategory
		
		SELECT  IDIngredientCategory, IDIngredientCategoryFather, IngredientCategoryLanguage 
		FROM @IngredientCategoryTempWork
		UNION ALL
		SELECT NULL AS IDIngredientCategory, NULL AS IDIngredientCategoryFather, '' AS IngredientCategoryLanguage
		ORDER BY IngredientCategoryLanguage
	
END


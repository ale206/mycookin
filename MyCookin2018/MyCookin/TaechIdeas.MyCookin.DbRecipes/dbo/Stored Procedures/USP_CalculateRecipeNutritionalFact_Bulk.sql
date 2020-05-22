-- =============================================
-- Author:		<Saverio Cammarata>
-- Create date: <13/09/2012>
-- Description:	<Update all recipes nutrictional facts>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CalculateRecipeNutritionalFact_Bulk] 
	@ConfirmExecution BIT
AS
BEGIN
	IF @ConfirmExecution=1
		BEGIN
			DECLARE @IDRecipeToUpdate UNIQUEIDENTIFIER
			DECLARE AllRecipeCursor CURSOR
			FOR
				select IDRecipe from recipes where Checked=1 and RecipeEnabled=1 and Draft=0

			OPEN AllRecipeCursor
			FETCH NEXT FROM AllRecipeCursor INTO @IDRecipeToUpdate
			WHILE(@@FETCH_STATUS<>-1)
				BEGIN
					BEGIN TRY
					EXEC	[dbo].[USP_CalculateRecipeNutritionalFacts_withTableDirectUpdate]
							@IDRecipe = @IDRecipeToUpdate
					END TRY
					BEGIN CATCH
						--SELECT @IDRecipeToUpdate
					END CATCH
					FETCH NEXT FROM AllRecipeCursor INTO @IDRecipeToUpdate
				END
			CLOSE AllRecipeCursor
			DEALLOCATE AllRecipeCursor
		END
END

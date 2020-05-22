using System.Collections.Generic;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.Core
{
    public interface IRecipeBookManager
    {
        IEnumerable<RecipesInRecipeBookOutput> RecipesInRecipeBook(RecipesInRecipeBookInput recipesInRecipeBookInput);

        NumberOfRecipesInsertedByUserOutput NumberOfRecipesInsertedByUser(
            NumberOfRecipesInsertedByUserInput numberOfRecipesInsertedByUserInput);

        CheckIfRecipeIsInBookOutput CheckIfRecipeIsInBook(CheckIfRecipeIsInBookInput checkIfRecipeIsInBookInput);
        SaveRecipeInBookOutput SaveRecipeInBook(SaveRecipeInBookInput saveRecipeInBookInput);
        DeleteRecipeFromBookOutput DeleteRecipeFromBook(DeleteRecipeFromBookInput deleteRecipeFromBookInput);
    }
}
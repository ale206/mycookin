using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.Core
{
    public interface IRecipeVoteManager
    {
        RecipeVoteOutput RecipeVote(RecipeVoteInput recipeVoteInput);
        RecipeAvgVoteOutput RecipeAvgVote(RecipeAvgVoteInput recipeAvgVoteInput);
        SaveVoteOutput SaveVote(SaveVoteInput saveVoteInput);
        DeleteVoteOutput DeleteVote(DeleteVoteInput deleteVoteInput);
    }
}
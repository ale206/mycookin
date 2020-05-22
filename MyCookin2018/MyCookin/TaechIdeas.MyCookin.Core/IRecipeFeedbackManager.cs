using System.Collections.Generic;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.Core
{
    public interface IRecipeFeedbackManager
    {
        FeedbackInfoOutput FeedbackInfo(FeedbackInfoInput feedbackInfoInput);

        FeedbackByRecipeAndUserOutput
            FeedbackByRecipeAndUser(FeedbackByRecipeAndUserInput feedbackByRecipeAndUserInput);

        SaveFeedbackOutput SaveFeedback(SaveFeedbackInput saveFeedbackInput);
        DeleteFeedbackOutput DeleteFeedback(DeleteFeedbackInput deleteFeedbackInput);
        IEnumerable<LikesForRecipeOutput> LikesForRecipe(LikesForRecipeInput likesForRecipeInput);
        IEnumerable<CommentsForRecipeOutput> CommentsForRecipe(CommentsForRecipeInput commentsForRecipeInput);
    }
}
using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.BusinessLogic
{
    public class RecipeFeedbackManager : IRecipeFeedbackManager
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeFeedbackManager(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        #region FeedbackInfo

        public FeedbackInfoOutput FeedbackInfo(FeedbackInfoInput feedbackInfoInput)
        {
            return _mapper.Map<FeedbackInfoOutput>(_recipeRepository.FeedbackInfo(_mapper.Map<FeedbackInfoIn>(feedbackInfoInput)));
        }

        #endregion

        #region FeedbackByRecipeAndUser

        public FeedbackByRecipeAndUserOutput FeedbackByRecipeAndUser(FeedbackByRecipeAndUserInput feedbackByRecipeAndUserInput)
        {
            return _mapper.Map<FeedbackByRecipeAndUserOutput>(_recipeRepository.FeedbackByRecipeAndUser(_mapper.Map<FeedbackByRecipeAndUserIn>(feedbackByRecipeAndUserInput)));
        }

        #endregion

        #region SaveFeedback

        public SaveFeedbackOutput SaveFeedback(SaveFeedbackInput saveFeedbackInput)
        {
            return _mapper.Map<SaveFeedbackOutput>(_recipeRepository.SaveFeedback(_mapper.Map<SaveFeedbackIn>(saveFeedbackInput)));
        }

        #endregion

        #region DeleteFeedback

        public DeleteFeedbackOutput DeleteFeedback(DeleteFeedbackInput deleteFeedbackInput)
        {
            return _mapper.Map<DeleteFeedbackOutput>(_recipeRepository.DeleteFeedback(_mapper.Map<DeleteFeedbackIn>(deleteFeedbackInput)));
        }

        #endregion

        #region LikesForRecipe

        public IEnumerable<LikesForRecipeOutput> LikesForRecipe(LikesForRecipeInput likesForRecipeInput)
        {
            return _mapper.Map<IEnumerable<LikesForRecipeOutput>>(_recipeRepository.LikesForRecipe(_mapper.Map<LikesForRecipeIn>(likesForRecipeInput)));
        }

        #endregion

        #region CommentsForRecipe

        public IEnumerable<CommentsForRecipeOutput> CommentsForRecipe(CommentsForRecipeInput commentsForRecipeInput)
        {
            return _mapper.Map<IEnumerable<CommentsForRecipeOutput>>(_recipeRepository.CommentsForRecipe(_mapper.Map<CommentsForRecipeIn>(commentsForRecipeInput)));
        }

        #endregion
    }
}
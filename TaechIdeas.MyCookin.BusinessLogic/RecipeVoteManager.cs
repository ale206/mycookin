using AutoMapper;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.BusinessLogic
{
    public class RecipeVoteManager : IRecipeVoteManager
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeVoteManager(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        #region RecipeVote

        public RecipeVoteOutput RecipeVote(RecipeVoteInput recipeVoteInput)
        {
            return _mapper.Map<RecipeVoteOutput>(_recipeRepository.RecipeVote(_mapper.Map<RecipeVoteIn>(recipeVoteInput)));
        }

        #endregion

        #region RecipeAvgVote

        public RecipeAvgVoteOutput RecipeAvgVote(RecipeAvgVoteInput recipeAvgVoteInput)
        {
            return _mapper.Map<RecipeAvgVoteOutput>(_recipeRepository.RecipeAvgVote(_mapper.Map<RecipeAvgVoteIn>(recipeAvgVoteInput)));
        }

        #endregion

        #region SaveVote

        public SaveVoteOutput SaveVote(SaveVoteInput saveVoteInput)
        {
            return _mapper.Map<SaveVoteOutput>(_recipeRepository.SaveVote(_mapper.Map<SaveVoteIn>(saveVoteInput)));
        }

        #endregion

        #region DeleteVote

        public DeleteVoteOutput DeleteVote(DeleteVoteInput deleteVoteInput)
        {
            return _mapper.Map<DeleteVoteOutput>(_recipeRepository.DeleteVote(_mapper.Map<DeleteVoteIn>(deleteVoteInput)));
        }

        #endregion
    }
}
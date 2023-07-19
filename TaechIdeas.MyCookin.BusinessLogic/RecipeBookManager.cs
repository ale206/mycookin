using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.BusinessLogic
{
    public class RecipeBookManager : IRecipeBookManager
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeBookManager(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        #region RecipesInRecipeBook

        public IEnumerable<RecipesInRecipeBookOutput> RecipesInRecipeBook(RecipesInRecipeBookInput recipesInRecipeBookInput)
        {
            return _mapper.Map<IEnumerable<RecipesInRecipeBookOutput>>(_recipeRepository.RecipesInRecipeBook(_mapper.Map<RecipesInRecipeBookIn>(recipesInRecipeBookInput)));
        }

        #endregion

        #region NumberOfRecipesInsertedByUser

        public NumberOfRecipesInsertedByUserOutput NumberOfRecipesInsertedByUser(NumberOfRecipesInsertedByUserInput numberOfRecipesInsertedByUserInput)
        {
            return _mapper.Map<NumberOfRecipesInsertedByUserOutput>(_recipeRepository.NumberOfRecipesInsertedByUser(_mapper.Map<NumberOfRecipesInsertedByUserIn>(numberOfRecipesInsertedByUserInput)));
        }

        #endregion

        #region CheckIfRecipeIsInBook

        public CheckIfRecipeIsInBookOutput CheckIfRecipeIsInBook(CheckIfRecipeIsInBookInput checkIfRecipeIsInBookInput)
        {
            return _mapper.Map<CheckIfRecipeIsInBookOutput>(_recipeRepository.CheckIfRecipeIsInBook(_mapper.Map<CheckIfRecipeIsInBookIn>(checkIfRecipeIsInBookInput)));
        }

        #endregion

        #region SaveRecipeInBook

        public SaveRecipeInBookOutput SaveRecipeInBook(SaveRecipeInBookInput saveRecipeInBookInput)
        {
            return _mapper.Map<SaveRecipeInBookOutput>(_recipeRepository.SaveRecipeInBook(_mapper.Map<SaveRecipeInBookIn>(saveRecipeInBookInput)));
        }

        #endregion

        #region DeleteRecipeFromBook

        public DeleteRecipeFromBookOutput DeleteRecipeFromBook(DeleteRecipeFromBookInput deleteRecipeFromBookInput)
        {
            return _mapper.Map<DeleteRecipeFromBookOutput>(_recipeRepository.DeleteRecipeFromBook(_mapper.Map<DeleteRecipeFromBookIn>(deleteRecipeFromBookInput)));
        }

        #endregion
    }
}
using System.Threading.Tasks;
using MyCookin.Business.Interfaces;
using MyCookin.Domain.Entities;
using MyCookin.Domain.Repositories;

namespace MyCookin.Business.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<Recipe> GetRecipeById(long recipeId)
        {
            return await _recipeRepository.GetRecipeById(recipeId);
        }
    }
}
using System.Threading.Tasks;
using MyCookin.Business.Interfaces;
using MyCookin.Domain.Entities;
using MyCookin.Domain.Repositories;

namespace MyCookin.Business.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<Ingredient> GetIngredientById(long id)
        {
            return await _ingredientRepository.GetIngredientById(id);
        }
    }
}
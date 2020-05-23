using System.Collections.Generic;
using System.Threading.Tasks;
using MyCookin.Business.Interfaces;
using MyCookin.Domain.Entities;
using MyCookin.Domain.Repositories;

namespace MyCookin.Business.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _IngredientRepository;

        public IngredientService(IIngredientRepository IngredientRepository)
        {
            _IngredientRepository = IngredientRepository;
        }

        public async Task<Ingredient> GetIngredientById(long id)
        {
            return await _IngredientRepository.GetIngredientById(id);
        }
    }
}
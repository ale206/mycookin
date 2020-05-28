using System.Threading.Tasks;
using MyCookin.Business.Interfaces;
using MyCookin.Domain.Entities;
using MyCookin.Domain.Repositories;
using Serilog;

namespace MyCookin.Business.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger _logger;

        public IngredientService(IIngredientRepository ingredientRepository, ILogger logger)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
        }

        public async Task<Ingredient> GetIngredientById(long id)
        {
            _logger.Debug("Calling Get Ingredient By ID");
            return await _ingredientRepository.GetIngredientById(id);
        }
    }
}
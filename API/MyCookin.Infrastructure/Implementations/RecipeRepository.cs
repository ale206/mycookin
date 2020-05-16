using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyCookin.Domain.Entities;
using MyCookin.Domain.Repositories;

namespace MyCookin.Infrastructure.Implementations
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RecipeRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("RecipesConnectionString");
        }

        public Task<Recipe> GetRecipeById(long recipeId)
        {
            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                //TODO DAPPER & QRY
            }

            return new Task<Recipe>(() => new Recipe {Id = 1, Name = "Spaghetti"});
        }
    }
}
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyCookin.Domain.Entities;
using MyCookin.Domain.Repositories;
using MyCookin.Infrastructure.DataMappers;

namespace MyCookin.Infrastructure.Implementations
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public IngredientRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("RecipesConnectionString");
        }

        public async Task<Ingredient> GetIngredientById(long id)
        {
            IngredientDataMapper ingredientData;
            var sql = $"SELECT * FROM `recipes`.`ingredient` WHERE id = {id} AND is_enabled = 1;";

            await using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                ingredientData = connection.QuerySingleAsync<IngredientDataMapper>(sql).Result;
            }

            return ingredientData.CovertToEntity();
        }
    }
}
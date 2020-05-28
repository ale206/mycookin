using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyCookin.Domain.Entities;
using MyCookin.Domain.Repositories;
using MyCookin.Infrastructure.DataMappers;
using Serilog;

namespace MyCookin.Infrastructure.Implementations
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger _logger;

        public IngredientRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration,
            ILogger logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
            _connectionString = configuration.GetConnectionString("RecipesConnectionString");
        }

        public async Task<Ingredient> GetIngredientById(long id)
        {
            _logger.Debug("Start query for Get Ingredient By ID");

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
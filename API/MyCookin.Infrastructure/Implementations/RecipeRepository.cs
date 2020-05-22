using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
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

        public async Task<IEnumerable<Language>> GetSupportedLanguages()
        {
            Task<IEnumerable<Language>> languages;
            const string sql = "SELECT * FROM `recipes`.`language` WHERE Enabled = 1;";

            await using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                var multi = connection.QueryMultipleAsync(sql).Result;
                languages = multi.ReadAsync<Language>();
            }

            return await languages;
        }
    }
}
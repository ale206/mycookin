using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MyCookin.Domain.Entities;
using MyCookin.Domain.Repositories;
using MyCookin.Infrastructure.DataMappers;

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

        public Task<Recipe> GetRecipeById(long id)
        {
            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                //TODO DAPPER & QRY
            }

            return new Task<Recipe>(() => new Recipe {Id = 1, Name = "Spaghetti"});
        }

        public async Task<IEnumerable<Language>> GetSupportedLanguages()
        {
            IEnumerable<LanguageDataMapper> languages;
            const string sql = "SELECT * FROM `recipes`.`language` WHERE is_enabled = 1;";

            await using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                var multi = connection.QueryMultipleAsync(sql).Result;
                languages = await multi.ReadAsync<LanguageDataMapper>();
            }

            return languages.Select(x => x.CovertToEntity());
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCookin.Domain.Entities;

namespace MyCookin.Domain.Repositories
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeById(long id);
        Task<IEnumerable<Language>> GetSupportedLanguages();
    }
}
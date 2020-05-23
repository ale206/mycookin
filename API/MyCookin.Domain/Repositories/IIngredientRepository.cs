using System.Threading.Tasks;
using MyCookin.Domain.Entities;

namespace MyCookin.Domain.Repositories
{
    public interface IIngredientRepository
    {
        Task<Ingredient> GetIngredientById(long id);
    }
}
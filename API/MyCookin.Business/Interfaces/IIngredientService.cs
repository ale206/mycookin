using System.Threading.Tasks;
using MyCookin.Domain.Entities;

namespace MyCookin.Business.Interfaces
{
    public interface IIngredientService
    {
        Task<Ingredient> GetIngredientById(long id);
    }
}
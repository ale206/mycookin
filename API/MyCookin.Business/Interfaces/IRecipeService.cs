using System.Threading.Tasks;
using MyCookin.Domain.Entities;

namespace MyCookin.Business.Interfaces
{
    public interface IRecipeService
    {
        Task<Recipe> GetRecipeById(long recipeId);
    }
}
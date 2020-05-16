using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCookin.Business.Interfaces;
using MyCookin.Domain.Entities;

namespace MyCookin.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("{id}", Name = "Get Recipe By ID")]
        [ProducesResponseType(typeof(Recipe), 200)]
        public async Task<IActionResult> GetRecipeById(long id)
        {
            var recipe = await _recipeService.GetRecipeById(id);

            return Ok(recipe);
        }
    }
}
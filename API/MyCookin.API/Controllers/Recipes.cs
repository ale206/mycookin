using System.Collections.Generic;
using System.ComponentModel;
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

        [HttpGet("{id}", Name = "GetRecipeById")]
        [Description("Get Recipe By Id")]
        [ProducesResponseType(typeof(Recipe), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetRecipeById(long id)
        {
            var recipe = await _recipeService.GetRecipeById(id);

            return Ok(recipe);
        }

        [HttpGet("languages", Name = "Get Supported Languages")]
        [ProducesResponseType(typeof(IEnumerable<Language>), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetSupportedLanguages()
        {
            var supportedLanguages = await _recipeService.GetSupportedLanguages();

            return Ok(supportedLanguages);
        }
    }
}
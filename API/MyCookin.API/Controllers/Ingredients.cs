using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyCookin.Business.Interfaces;
using MyCookin.Domain.Entities;

namespace MyCookin.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("{id}", Name = "GetIngredientById")]
        [Description("Get Ingredient By Id")]
        [ProducesResponseType(typeof(Ingredient), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetIngredientById(long id)
        {
            var ingredient = await _ingredientService.GetIngredientById(id);

            return Ok(ingredient);
        }
    }
}
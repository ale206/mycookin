using System;
using Microsoft.AspNetCore.Mvc;

namespace MyCookin.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return Ok($"Alive! {DateTime.UtcNow}");
        }
    }
}
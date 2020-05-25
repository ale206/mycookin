using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCookin.Domain.Entities;

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
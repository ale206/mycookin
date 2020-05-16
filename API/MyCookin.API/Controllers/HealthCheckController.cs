using System;
using Microsoft.AspNetCore.Mvc;

namespace MyCookin.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return $"Alive! {DateTime.UtcNow}";
        }
    }
}
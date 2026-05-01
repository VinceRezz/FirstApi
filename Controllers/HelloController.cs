using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello from Controller";
        }

        [HttpGet("{name}")]
        public string GetByName(string name)
        {
            return $"Hello {name}, welcome to .NET API";
        }

    }
}


using FirstApi.DTOs;
using FirstApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authService.Register(dto);

            if (token == null)
                return BadRequest("Username already exists");


            return Ok(new { token });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authService.Login(dto);

            if (token == null)
                return BadRequest("Username or password is invalid");

            return Ok(new { token });

        }
    }
}

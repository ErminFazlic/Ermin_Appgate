using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginModel login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var result = _service.Register(login.Username, login.Password);

            if (result)
            {
                return Ok("User registered successfully.");
            }
            else
            {
                return BadRequest("User already exists.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var token = _service.Login(login.Username, login.Password);

            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }
    }

    public class LoginModel(string username, string password)
    {
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;
    }
}

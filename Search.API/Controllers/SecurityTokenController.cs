using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Search.Infrastructure.Services;
using System.Threading.Tasks;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityTokenController : ControllerBase
    {
        private readonly IJWTTokenService _jwtTokenService;
        public SecurityTokenController(IJWTTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Generates JWT token
        /// </summary>
        /// <param name="username"> user name </param>
        /// <param name="password"> password </param>
        /// <returns></returns>

        [HttpGet("/api/v1.0/token/create")]
        public async Task<IActionResult> GenerateToken([FromQuery] string username, [FromQuery] string password)
        {
            string token = string.Empty;
            var user = HttpContext.Items["User"] as string;
            if (user == null)
            {
                token = _jwtTokenService.GenerateToken(username, password);

            }
            return Ok(token);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Search.Domain.Dto;
using Search.Domain.Entities;
using Search.Infrastructure.Services;

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

        [HttpGet("/api/v1.0/token")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> Authenticate([FromQuery] string username, [FromQuery] string password)
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

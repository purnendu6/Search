using System.Security.Claims;

namespace Search.Infrastructure.Services
{
    public interface IJWTTokenService
    {
        public string GenerateToken(string userId, string role);
        public ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}

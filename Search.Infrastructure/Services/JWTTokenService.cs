using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Search.Infrastructure.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly int _expiryInMinutes;

        public JWTTokenService(string secretKey, string issuer, int expiryInMinutes)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _expiryInMinutes = expiryInMinutes;
        }

        /// <summary>
        /// Generates Token
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="role">role </param>
        /// <returns></returns>
        public string GenerateToken(string userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role)
            }),
                Expires = DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Gets principal from token 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidIssuer = _issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey)),
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                return claimsPrincipal;
            }
            catch
            {
                // Token validation failed
                return null;
            }
        }
    }
}

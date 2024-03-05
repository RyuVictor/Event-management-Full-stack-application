
using Bajaj.Events.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bajaj.Events.Api.JWT
{
    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Bajaj:TokenSecret"]));
        }
        public string GenerateAccessToken(string userName, string roleName)
        {
            var claims = new List<Claim>{
            new Claim(ClaimTypes.Email,userName),
            new Claim(ClaimTypes.Name,userName),
            new Claim(ClaimTypes.Role,roleName)
        };
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Bajaj:TokenSecret"])),
                ValidateLifetime = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(accessToken, tokenValidationParameters, out _);
        }

    }
}

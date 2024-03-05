using Bajaj.Events.Models;
using System.Security.Claims;

namespace Bajaj.Events.Api.JWT
{
    public interface ITokenManager
    {
        string GenerateAccessToken(string userName, string roleName);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    }
}

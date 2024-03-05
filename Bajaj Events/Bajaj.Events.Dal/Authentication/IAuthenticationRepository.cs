using Bajaj.Events.Models;

namespace Bajaj.Events.Dal.Authentication
{
    public interface IAuthenticationRepository
    {
        Task<int> RegisterUser(User user);
        Task<AuthenticationResponse> AuthenticateCredentials(User user);
        Role GetUserRole(int userRoleId);
        Task<int> ModifyUserRefreshToken(string userName, string refreshToken, DateTime refreshTokenExpiryDateTime);
        Task<User> GetUser(string  userName);
    }
}

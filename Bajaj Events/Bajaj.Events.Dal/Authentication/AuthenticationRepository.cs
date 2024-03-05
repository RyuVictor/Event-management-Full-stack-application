using Bajaj.Events.Models;
using Microsoft.EntityFrameworkCore;

namespace Bajaj.Events.Dal.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly BajajEventsDbContext _context;

        public AuthenticationRepository(BajajEventsDbContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationResponse> AuthenticateCredentials(User user)
        {
            var existingUser = await _context.User.SingleOrDefaultAsync(u => u.UserName == user.UserName);
            if (existingUser != null)
            {
                var isValidUser = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);
                if (isValidUser)
                {
                    return new AuthenticationResponse
                    {
                        Email = existingUser.UserName,
                        IsAuthenticated = true,
                        Token = "No Token",
                        RefreshToken = "No Refresh Token",
                        Message = "You are now authenticated User!",
                        RoleName = GetUserRole(existingUser.RoleId).RoleName
                    };
                }
                else
                {
                    return new AuthenticationResponse
                    {
                        IsAuthenticated = false,
                        Message = "Please check your password!"
                    };
                }
            }
            else
            {
                return new AuthenticationResponse
                {
                    IsAuthenticated = false,
                    Message = "Please check your User Name/Email Id!"
                };
            }
        }

        public Role GetUserRole(int userRoleId)
        {
            return _context.Roles.Single(user => user.RoleId == userRoleId);
        }

        public async Task<int> ModifyUserRefreshToken(string userName, string refreshToken, DateTime refreshTokenExpiryDateTime)
        {
            var user = await _context.User.SingleOrDefaultAsync(u => u.UserName == userName);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = refreshTokenExpiryDateTime;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RegisterUser(User user)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            await _context.User.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(string userName)
        {
            return await _context.User.FirstAsync(u=>u.UserName == userName);
        }
    }
}

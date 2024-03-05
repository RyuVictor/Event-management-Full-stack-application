namespace Bajaj.Events.Models
{
    public class AuthenticationResponse
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

    }
}

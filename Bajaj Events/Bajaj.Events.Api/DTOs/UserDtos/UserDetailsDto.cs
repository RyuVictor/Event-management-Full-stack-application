namespace Bajaj.Events.Api.DTOs.UserDtos
{
    public class UserDetailsDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public int RoleId { get; set; }
    }
}

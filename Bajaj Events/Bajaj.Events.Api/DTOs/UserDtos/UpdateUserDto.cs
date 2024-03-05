namespace Bajaj.Events.Api.DTOs.UserDtos
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? Password { get; set; }
        public int RoleId { get; set; }
    }
}

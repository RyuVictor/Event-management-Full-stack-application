namespace Bajaj.Events.Api.DTOs.UserDtos
{
    public class NewUserDto
    {
        public string UserName { get; set; } = string.Empty;

        public string? Password { get; set; }
        public int RoleId { get; set; }
    }
}

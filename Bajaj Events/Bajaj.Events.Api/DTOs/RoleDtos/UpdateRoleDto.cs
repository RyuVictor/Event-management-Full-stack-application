namespace Bajaj.Events.Api.DTOs.RoleDtos
{
    public class UpdateRoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
    }
}

using Bajaj.Events.Models;
using System.ComponentModel.DataAnnotations;

namespace Bajaj.Events.Api.DTOs.UserDtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

    }
}

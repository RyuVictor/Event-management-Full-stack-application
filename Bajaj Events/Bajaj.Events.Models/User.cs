using System.ComponentModel.DataAnnotations;

namespace Bajaj.Events.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name (Email) is required field!")]
        [EmailAddress(ErrorMessage = "Please enter valid email Id!")]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Password can not exceed 100 characters!")]
        [Required(ErrorMessage = "Password is required field!")]
        public string? Password { get; set; }
        public int RoleId { get; set; }
        [MaxLength(500)]
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiry { get; set; }
        public virtual Role? Role { get; set; }
        public virtual EventRegistration? EventRegistration { get; set; }
    }
}

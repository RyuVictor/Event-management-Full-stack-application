using System.ComponentModel.DataAnnotations;
namespace Bajaj.Events.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Employee name is required")]
        [MaxLength(100,ErrorMessage ="Employee name cannot exceed 100 characters")]
        public string EmployeeName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Employee address is required")]
        [MaxLength(500, ErrorMessage = "Employee address cannot exceed 500 characters")]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Employee City is required")]
        [MaxLength(50, ErrorMessage = "Employee City cannot exceed 50 characters")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Employee Country is required")]
        [MaxLength(50, ErrorMessage = "Employee name cannot exceed 50 characters")]
        public string Country { get; set; } = string.Empty;
        [Required(ErrorMessage = "Employee Zipcode is required")]
        [MaxLength(6, ErrorMessage = "Zipcode cannot exceed 6 characters")]
        [MinLength(6, ErrorMessage = "Zipcode cannot be less than 6 characters")]
        public int Zipcode { get; set; }
        [Required(ErrorMessage = "Employee skillset is required")]
        [MaxLength(100, ErrorMessage = "Employee skillset cannot exceed 100 characters")]
        public string Skillsets { get; set; } = string.Empty;
        [Required(ErrorMessage = "Employee name is required")]
        [MaxLength(100, ErrorMessage = "Employee name cannot exceed 100 characters")]
        [EmailAddress(ErrorMessage ="Employee Email is invalid")]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20, ErrorMessage = "Employee phone number cannot exceed 20 characters")]
        public string Phone { get; set; } = string.Empty;
        [MaxLength(50, ErrorMessage = "Employee Department cannot exceed 50 characters")]
        public string Department { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Employee profile pic path cannot exceed 100 characters")]
        public string Avatar { get; set; } = string.Empty;


    }
}

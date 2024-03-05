namespace Bajaj.Events.Api.DTOs.EmployeeDTOs
{
	public class NewEmployeeDto
	{
        public string EmployeeName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Zipcode { get; set; }
        public string Skillsets { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;

    }
}


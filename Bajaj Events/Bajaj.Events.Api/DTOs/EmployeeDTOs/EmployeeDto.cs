namespace Bajaj.Events.Api.DTOs.EmployeeDTOs
{
	public class EmployeeDto
	{
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;


		public string City { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } =string.Empty;
	}
}


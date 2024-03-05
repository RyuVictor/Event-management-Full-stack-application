namespace Bajaj.Events.Api.DTOs.EventRegistrationDTOs
{
	public class UpdateEventRegistrationDto
	{
		public int EventRegistrationId { get; set; }
		public DateTime RegistrationDate { get; set; }
		public int EmployeeId { get; set; }
		public int EventId { get; set; }
		public int UserId { get; set; }
	}
}

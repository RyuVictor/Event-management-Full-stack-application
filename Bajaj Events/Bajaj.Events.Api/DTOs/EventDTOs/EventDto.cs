using System.ComponentModel.DataAnnotations;

namespace Bajaj.Events.Api.DTOs.EventDTOs
{
	public class EventDto
	{
        public int EventId { get; set; }
        public string? EventCode { get; set; }
		
		public string EventName { get; set; } = string.Empty;
		
		public string Venue { get; set; } = string.Empty;

		public DateTime StartDate { get; set; }
		public int Fees { get; set; }
	}
}

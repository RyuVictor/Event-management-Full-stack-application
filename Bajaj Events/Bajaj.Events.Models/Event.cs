using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bajaj.Events.Models
{
    public class Event
    {
        public int EventId { get; set; }
        [MaxLength(10, ErrorMessage = "Event Code can not exceed 10 characters!")]
        [Required(ErrorMessage = "Event Code is required field!")]
        public string EventCode { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Event Name can not exceed 100 characters!")]
        [Required(ErrorMessage = "Event Name is required field!")]
        public string EventName { get; set; } = string.Empty;
        [MaxLength(500, ErrorMessage = "Event description can not exceed 500 characters!")]
        [Required(ErrorMessage = "Event Description is required field!")]
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "Decimal(10,2)")]
        [Required(ErrorMessage = "Event Code is required field!")]
        public decimal Fees { get; set; }
        [Range(0, 100, ErrorMessage = "Total seats must be between 0-100!")]
        public int SeatsFilled { get; set; }
        [MaxLength(200, ErrorMessage = "Logo length can not exceed 200 characters!")]
        public string Logo { get; set; } = string.Empty;
        [MaxLength(200, ErrorMessage = "Venue length can not exceed 200 characters!")]
        [Required(ErrorMessage = "Venue is required field!")]
        public string? Venue { get; set; }
        [MaxLength(20, ErrorMessage = "Organizer name can not exceed 200 characters!")]
        [Required(ErrorMessage = "Organizer name is required field!")]
        public string OrganizedBy { get; set; } = string.Empty;
    }
}

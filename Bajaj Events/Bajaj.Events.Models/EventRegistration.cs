namespace Bajaj.Events.Models
{
    public class EventRegistration
    {
        public int EventRegistrationId { get; set; }
        public int EmployeeId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual Event? Event { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual User? User { get; set; }
    }
}

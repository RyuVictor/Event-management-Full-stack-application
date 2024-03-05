using Microsoft.EntityFrameworkCore;
using Bajaj.Events.Models;

namespace Bajaj.Events.Dal
{
    public class BajajEventsDbContext : DbContext
    {
        public BajajEventsDbContext()
        {
            
        }
        public BajajEventsDbContext(DbContextOptions<BajajEventsDbContext> options ) : base(options)
        {
               
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured != true)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ByteJan24EventsDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}

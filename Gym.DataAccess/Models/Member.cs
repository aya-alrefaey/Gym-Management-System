using System.Net;

namespace Gym.Models
{
    public class Member:Person
    {
        
        public DateTime JoinDate { get; set; }

        public string? Photo { get; set; }

      
        // Relationships
       public HealthRecord HealthRecord { get; set; }
        public ICollection<Booking> Bookings { get; set; } = [];
        public ICollection<Membership> Memberships { get; set; } = [];
    }
}

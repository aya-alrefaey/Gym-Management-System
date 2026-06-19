using Gym.enums;

namespace Gym.Models
{
    public class HealthRecord:BaseEntity
    {
   
        public double Height { get; set; }

        public double Weight { get; set; }

        public BloodType BloodType { get; set; }

        public string? Note { get; set; }

        public DateTime LastUpdate { get; set; }
        public int MemberId { get; set; }

        public Member Member { get; set; }
    }
}

namespace Gym.Models
{
    public class Plan:BaseEntity
    {
       

        public string Name { get; set; }

        public string Description { get; set; }

        public int DurationDays { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;
       

        public ICollection<Membership> Memberships { get; set; } = [];
    }
}

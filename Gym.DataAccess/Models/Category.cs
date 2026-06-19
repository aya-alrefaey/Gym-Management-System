namespace Gym.Models
{
    public class Category:BaseEntity
    {

        public string Name { get; set; }

        public ICollection<Session> Sessions { get; set; } = [];
    }
}

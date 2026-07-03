using Gym.enums;

namespace Gym.Models
{
    public class Trainer:Person
    {
       
        public Specialties Specialties { get; set; }

        public DateTime HireDate { get; set; }

        public ICollection<Session> Sessions { get; set; } = [];
        

       
    }
}

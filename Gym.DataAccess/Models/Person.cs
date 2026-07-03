using Gym.enums;

namespace Gym.Models
{
    public class Person:BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public Address Address { get; set; }
    }
}

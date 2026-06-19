using Gym.enums;
using System.ComponentModel.DataAnnotations;

namespace Gym.BusinessLogic.ViewModels.Trainer
{
    public class TrainerCreateViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(010|011|012|015)\d{8}$",
            ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select a gender.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Please select a Building Number.")]
        [Display(Name = "Building Number")]
        public string BuildingNo { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Street cannot exceed 50 characters.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter a city.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select a specialty.")]
        public Specialties Specialties { get; set; }
    }
}
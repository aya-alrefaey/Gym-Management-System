using System.ComponentModel.DataAnnotations;

namespace Gym.BusinessLogic.ViewModels.Plan
{
    public class PlanEditViewModel
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(1, 365)]
        public int DurationDays { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }
       
    }
}

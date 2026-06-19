using System.ComponentModel.DataAnnotations;

namespace Gym.BusinessLogic.ViewModels.Plan
{
    public class PlanDetailsViewModel
    {
       
           
            public string Name { get; set; }

            
            public string Description { get; set; }

          
            public int DurationDays { get; set; }

            public decimal Price { get; set; }
            public bool IsActive { get; set; }
        }
    }


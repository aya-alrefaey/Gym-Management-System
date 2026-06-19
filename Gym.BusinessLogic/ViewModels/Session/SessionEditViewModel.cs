using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.ViewModels.Session
{
    public class SessionEditViewModel
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Category Is Required.")]
        public int TrainerId { get; set; }
        [Required(ErrorMessage = "Start Date Is Required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date Is Required.")]
        public DateTime EndDate { get; set; }
        
        [Required(ErrorMessage = "Description Is Required.")]
        [StringLength(500, ErrorMessage = "Description must be less than or equal to 500 characters.")]
        public string Description { get; set; }
    }
}

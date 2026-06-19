using Gym.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.ViewModels.Member
{
    public class EditMemberViewModel
    {
        public int Id { get; set; }

    
       
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^01[0125]\d{8}$",
            ErrorMessage = "Invalid Egyptian phone number")]
        public string Phone { get; set; }

        [Required]
        public string BuildingNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        public string? PhotoUrl { get; set; }
    }
}

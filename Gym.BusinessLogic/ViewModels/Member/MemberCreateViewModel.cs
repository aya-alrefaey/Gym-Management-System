using Gym.enums;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.ViewModels.Member
{
    public class MemberCreateViewModel
    {



        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^01[0125]\d{8}$",
            ErrorMessage = "Invalid Egyptian phone number")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]

        public Gender Gender { get; set; }


        [Required]
        public string BuildingNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }


        [Required (ErrorMessage= "Height is required")]
        [Range(50, 250)]
        public double Height { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(20, 300)]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Blood Type is required")]
        public BloodType BloodType { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.ViewModels.Booking
{
    public class CreateBookingViewModel
    {
       
        [Range (1, int.MaxValue, ErrorMessage = "Session is Required")]
        public int SessionId { get; set; }
        [Range (1, int.MaxValue, ErrorMessage = "Member is Required")]
        public int MemberId { get; set; }
    }
}

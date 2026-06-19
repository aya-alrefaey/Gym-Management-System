using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.ViewModels.Booking
{
    public class MembersAtSessionViewModel
    {
        public int BookingId { get; set; }
        public int MemberId { get; set; }
        public int SessionId { get; set; }
        public bool IsAttended { get; set; }
        public string MemberName { get; set; }
        public DateTime BookingDate { get; set; }
    }
}

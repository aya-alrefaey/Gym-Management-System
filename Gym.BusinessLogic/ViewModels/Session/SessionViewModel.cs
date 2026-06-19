using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.ViewModels.Session
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string TrainerName { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }

        public string Status { get; set; }
        public string DateDisplay { get; set; }

        public string TimeRangeDisplay { get; set; }

        //public string Duration { get; set; }
        public TimeSpan Duration { get; set; }
    }
}

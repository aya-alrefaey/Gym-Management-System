using Gym.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.ViewModels.HealthRecord
{
    public class HealthRecordViewModel
    {
        public double Height { get; set; }

        public double Weight { get; set; }

        public BloodType BloodType { get; set; }

        public string? Note { get; set; }
    }
}

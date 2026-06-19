using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.ViewModels.Membership
{
    public class MembershipCreateViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Member Is Required.")]
        public int MemberId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Plan Is Required.")]
        public int PlanId { get; set; }
        
    }
}

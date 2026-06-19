using AutoMapper;
using Gym.BusinessLogic.ViewModels.Plan;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Mapper
{
    public class PlanProfile:Profile
    {
        public PlanProfile()
        {
            CreateMap<Plan, PlanListViewModel>();
            CreateMap<Plan, PlanDetailsViewModel>();
            CreateMap<Plan, PlanEditViewModel>();

        }
    }
}

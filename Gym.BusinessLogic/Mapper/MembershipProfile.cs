using AutoMapper;
using Gym.BusinessLogic.ViewModels.Membership;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Mapper
{
    public class MembershipProfile : Profile
    {
        public MembershipProfile() {
            CreateMap<Membership, MembershipViewModel>()
        .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
        .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.Name));
            CreateMap<MembershipCreateViewModel, Membership>()
    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}

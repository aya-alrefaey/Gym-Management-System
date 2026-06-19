using AutoMapper;
using Gym.BusinessLogic.ViewModels.Member;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gym.BusinessLogic.Mapper
{
    public class MemberProfile:Profile
    {
        public MemberProfile() {
        CreateMap<Member, MemberViewModel>()
                .ForMember(mvm=> mvm.PhotoUrl, opt=> opt.MapFrom(m=> m.Photo));
        CreateMap<MemberCreateViewModel, Member>()
                .ForMember(m=> m.JoinDate, opt=> opt.MapFrom(vm=> DateTime.Now))
                .ForMember(m=> m.Address, opt=> opt.MapFrom(vm=> new Address
                {
                    BuildingNo = vm.BuildingNo,
                    Street = vm.Street,
                    City = vm.City
                }))
                .ForMember(m=> m.HealthRecord, opt=> opt.MapFrom(vm=> new HealthRecord
                {
                    Height = vm.Height,
                    Weight = vm.Weight,
                    BloodType = vm.BloodType,
                    Note = vm.Note,
                    LastUpdate = DateTime.Now
                }));

            CreateMap<Member, MemberDetailsViewModel>()
                .ForMember(mvm => mvm.PhotoUrl, opt => opt.MapFrom(m => m.Photo))
                .ForMember(mvm => mvm.Gender, opt => opt.MapFrom(m => m.Gender.ToString()))
                .ForMember(mvm => mvm.Address, opt => opt.MapFrom(m => $"{m.Address.BuildingNo} - {m.Address.City} - {m.Address.Street} "))
                .ForMember(mvm => mvm.Plan, opt => opt.MapFrom(m =>
                 GetActiveMembership(m) == null ? "No Active Plan" : GetActiveMembership(m).Plan.Name))
                .ForMember(mvm => mvm.MembershipStartDate, opt => opt.MapFrom(m =>
                GetActiveMembership(m) == null ? (DateTime?)null : GetActiveMembership(m).StartDate))
                .ForMember(mvm => mvm.MembershipEndDate, opt => opt.MapFrom(m =>
                GetActiveMembership(m) == null ? (DateTime?)null : GetActiveMembership(m).EndDate));
            CreateMap<Member, EditMemberViewModel>()
                .ForMember(mevm => mevm.PhotoUrl, opt => opt.MapFrom(m => m.Photo))
                .ForMember(mevm => mevm.BuildingNo, opt => opt.MapFrom(m => m.Address.BuildingNo))
                .ForMember(mevm => mevm.City, opt => opt.MapFrom(m => m.Address.City))
                .ForMember(mevm => mevm.Street, opt => opt.MapFrom(m => m.Address.Street));

            CreateMap<EditMemberViewModel, Member>()
           .ForPath(m => m.Address.BuildingNo, opt => opt.MapFrom(vm => vm.BuildingNo))
           .ForPath(m => m.Address.Street, opt => opt.MapFrom(vm => vm.Street))
           .ForPath(m => m.Address.City, opt => opt.MapFrom(vm => vm.City))
           .ForMember(m => m.Photo, opt => opt.Ignore())
           .ForMember(m => m.Name, opt => opt.Ignore())
           .ForMember(m => m.Id, opt => opt.Ignore());



        }
        private Membership? GetActiveMembership(Member member)
        {
            return member.Memberships
         .OrderByDescending(m => m.EndDate)
         .FirstOrDefault(m => m.EndDate >= DateTime.Now);

        }
    }
}

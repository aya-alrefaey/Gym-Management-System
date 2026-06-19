using AutoMapper;
using Gym.BusinessLogic.ViewModels.Session;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Mapper
{
    public class SessionProfile:Profile
    {
        public SessionProfile()
        {
            CreateMap<Models.Session, ViewModels.Session.SessionViewModel>()
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))

    .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))

    .ForMember(dest => dest.DateDisplay, opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))

    .ForMember(dest => dest.TimeRangeDisplay, opt => opt.MapFrom(src => $"{src.StartDate:hh:mm tt} - {src.EndDate:hh:mm tt}"))

    //.ForMember(dest => dest.Duration, opt => opt.MapFrom(src => $"{(src.EndDate - src.StartDate).TotalHours:F1} hrs"))

    .ForMember(dest => dest.AvailableSlots,opt => opt.MapFrom(src => src.Capacity - src.Bookings.Count))
    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.EndDate - src.StartDate))

   .ForMember(dest => dest.Status, opt => opt.MapFrom(src => GetStatus(src.StartDate, src.EndDate)));

            CreateMap<SessionCreateViewModel,Session>();
            CreateMap<Session, SessionEditViewModel>();

        }

        private string GetStatus(DateTime start, DateTime end)
        {
            var now = DateTime.Now;

            if (end < now)
                return "Finished";

            if (start > now)
                return "Upcoming";

            return "Ongoing";
        }
    }
}

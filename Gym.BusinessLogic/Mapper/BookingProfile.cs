using AutoMapper;
using Gym.BusinessLogic.ViewModels.Booking;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Mapper
{
    public class BookingProfile: Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, MembersAtSessionViewModel>()
    .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name));
            CreateMap<CreateBookingViewModel, Booking>()
    .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => DateTime.Now))
    .ForMember(dest => dest.IsAttended, opt => opt.MapFrom(src => false));
        }
}
}

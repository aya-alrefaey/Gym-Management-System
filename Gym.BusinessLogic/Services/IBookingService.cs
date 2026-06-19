using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Booking;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public interface IBookingService:IService<Booking>
    {
        public Task<List<MembersAtSessionViewModel>> GetMembersForSessionAsync(int id, CancellationToken cancellationToken = default);
        public Task<Result> CreateBookingAsync(CreateBookingViewModel vm, CancellationToken cancellationToken);
        public  Task<Result> CancelBookingAsync(int id, CancellationToken cancellationToken = default);
        Task<Result> MarkAsAttendedAsync(int bookingId, CancellationToken cancellationToken);
    }
}

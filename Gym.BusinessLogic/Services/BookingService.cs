using AutoMapper;
using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Booking;
using Gym.DataAccess.UnitOfWork;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public class BookingService : Service<Booking>, IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public BookingService(IUnitOfWork unitOfWork, IMapper _mapper ) : base(unitOfWork.Bookings)
        {
            _unitOfWork = unitOfWork;
            mapper = _mapper;
        }
        public async Task<List<MembersAtSessionViewModel>> GetMembersForSessionAsync(int id, CancellationToken cancellationToken = default)
        {
            var bookings = await _unitOfWork.Bookings
        .GetBookingsWithMemberForSessionAsync(id, cancellationToken);

            //var members = bookings.Select(b => new MembersAtSessionViewModel
            //{
            //    BookingId = b.Id,
            //    MemberId = b.MemberId,
            //    SessionId = b.SessionId,
            //    MemberName = b.Member.Name,
            //    BookingDate = b.BookingDate,
            //    IsAttended = b.IsAttended
            //}).ToList();
            var members = bookings.Select(b => mapper.Map<MembersAtSessionViewModel>(b)).ToList();

            return members;

        }
        public async Task<Result> CreateBookingAsync(CreateBookingViewModel vm, CancellationToken cancellationToken) {
            var session = await _unitOfWork.Sessions.SessionsWithBookingsAsync(vm.SessionId, cancellationToken);
            if (session == null)
            {
                return Result.Failure("Session not found.");
            }
            if (session.Bookings.Count >= session.Capacity)
            {
                return Result.Failure("Session is fully booked.");
            }
            if (session.StartDate <= DateTime.Now || session.EndDate <= DateTime.Now)
            {
                return Result.Failure("Cannot book a session that has already started or finished .");
            }
            //var member = await _unitOfWork.Members.MemberWithMembershipOnlyAsync(vm.MemberId, cancellationToken);
            //if (member == null)
            //{
            //    return Result.Failure("Member not found.");
            //}
            if (!await _unitOfWork.Members.HasValidMembershipAsync(vm.MemberId, cancellationToken))
            {
                return Result.Failure("Member does not have a valid membership.");
            }
            var exists = await _unitOfWork.Bookings.
                ExistAsync(b => b.MemberId == vm.MemberId && b.SessionId == vm.SessionId, cancellationToken);

            if (exists)
            {
                return Result.Failure("Member already booked this session.");
            }
            //var booking = new Booking
            //{

            //    MemberId = vm.MemberId,
            //    SessionId = vm.SessionId,
            //    BookingDate = DateTime.Now,
            //    IsAttended = false
            //};
            var booking =mapper.Map<Booking>(vm);

            await _unitOfWork.Bookings.AddAsync(booking, cancellationToken);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
        public async Task<Result> CancelBookingAsync(int id, CancellationToken cancellationToken = default)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id, cancellationToken);
            if (booking is null)
            {
                return Result.Failure("Booking not found.");
            }
            
          var session = await _unitOfWork.Sessions.GetByIdAsync(booking.SessionId, cancellationToken);
            if(session is null)
            {
                return Result.Failure("Session not found.");
            }
            if(session.StartDate <= DateTime.Now)
            {
                return Result.Failure("Cannot cancel a booking for a session that has already started.");
            }  
            await _unitOfWork.Bookings.Delete(booking, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> MarkAsAttendedAsync(int bookingId, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId, cancellationToken);

            if (booking is null)
                return Result.Failure("Booking not found.");

            var session = await _unitOfWork.Sessions.GetByIdAsync(booking.SessionId, cancellationToken);

            if (session is null)
                return Result.Failure("Session not found.");

            if (DateTime.Now < session.StartDate)
                return Result.Failure("Session has not started yet.");

            if (DateTime.Now > session.EndDate)
                return Result.Failure("Session already finished.");

            booking.IsAttended = true;

            await _unitOfWork.Bookings.Update(booking, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}

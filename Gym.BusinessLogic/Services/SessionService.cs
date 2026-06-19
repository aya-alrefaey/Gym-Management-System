using AutoMapper;
using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.Mapper;
using Gym.BusinessLogic.ViewModels.Session;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.DataAccess.UnitOfWork;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Gym.BusinessLogic.Services
{
    public class SessionService : Service<Session>, ISessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;

        public SessionService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork.Sessions)
        {
            unitOfWork = _unitOfWork;
            Mapper = _mapper;
        }

       
        public async Task<List<SessionViewModel>> GetAllSessions(CancellationToken cancellationToken)
        {
            var data = await unitOfWork.Sessions
                .EntireSessionsAsync(cancellationToken);

            //return data.Select(s => new SessionViewModel
            //{
            //    Id = s.Id,
            //    CategoryName = s.Category.Name,
            //    TrainerName = s.Trainer.Name,
            //    Description = s.Description,
            //    StartDate = s.StartDate,
            //    EndDate = s.EndDate,
            //    Capacity = s.Capacity,
            //    DateDisplay = s.StartDate.ToString("yyyy-MM-dd"),
            //    TimeRangeDisplay = $"{s.StartDate:HH:mm} - {s.EndDate:HH:mm}",
            //    Duration = (s.EndDate - s.StartDate).TotalHours + " hrs",

            //   AvailableSlots = s.Capacity - s.Bookings.Count,

            //    Status = s.EndDate < DateTime.Now
            //        ? "Finished"
            //        : s.StartDate > DateTime.Now
            //            ? "Upcoming"
            //            : "Ongoing"

            //}).ToList();
            return data.Select(s => Mapper.Map<SessionViewModel>(s)).ToList();
        }

        public async Task<Result> CreateSessionAsync(SessionCreateViewModel vm, CancellationToken cancellationToken)
        {
           if(vm.Capacity <=0|| vm.Capacity>25)
                return Result.Failure("Capacity must be between 1 and 25");

            if (vm.EndDate <= vm.StartDate)
                return Result.Failure("End date must be after start date");

            var categoryExists = await unitOfWork.Categories
    .GetByIdAsync(vm.CategoryId, cancellationToken);

            var trainerExists = await unitOfWork.Trainers
                .GetByIdAsync(vm.TrainerId, cancellationToken);

            if (categoryExists is null)
                return Result.Failure("Invalid Category");

            if (trainerExists is null)
                return Result.Failure("Invalid Trainer");

            //var Session = new Session
            //{
            //    CategoryId = vm.CategoryId,
            //    TrainerId = vm.TrainerId,
            //    StartDate = vm.StartDate,
            //    EndDate = vm.EndDate,
            //    Capacity = vm.Capacity,
            //    Description = vm.Description
            //};
            var Session = Mapper.Map<Session>(vm);
            await unitOfWork.Sessions.AddAsync(Session, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result<SessionViewModel>> GetSessionByIdAsync(int id, CancellationToken cancellationToken)
        {
            var session = await unitOfWork.Sessions
                .SessionsWithCategoryAndTrainerAsync(id, cancellationToken);
            if (session == null)
                return Result<SessionViewModel>.Failure("Session not found");
            
            return Result<SessionViewModel>.Success(Mapper.Map<SessionViewModel>(session));
        }

        //public async Task<Result> EditSession(EditSessionViewModel vm, CancellationToken cancellationToken)
        //{

        //    var trainer = await unitofwork.Trainers.GetByIdAsync(vm.Id, cancellationToken);

        //    if (trainer == null)
        //        return Result.Failure("Trainer not found");

        //    var emailExists = await unitofwork.Users.ExistAsync(
        //        m => m.Email == vm.Email && m.Id != vm.Id,
        //        cancellationToken);

        //    if (emailExists)
        //        return Result.Failure("Email already exists");

        //    var phoneExists = await unitofwork.Users.ExistAsync(
        //        m => m.Phone == vm.Phone && m.Id != vm.Id,
        //        cancellationToken);

        //    if (phoneExists)
        //        return Result.Failure("Phone already exists");


        //    trainer.Email = vm.Email;
        //    trainer.Phone = vm.Phone;

        //    trainer.Address.BuildingNo = vm.BuildingNo;
        //    trainer.Address.Street = vm.Street;
        //    trainer.Address.City = vm.City;
        //    trainer.Specialties = vm.Specialties;



        //    await unitofwork.Trainers.Update(trainer, cancellationToken);
        //    await unitofwork.CommitAsync(cancellationToken);

        //    return Result.Success();
        //}

        public async Task<Result<SessionEditViewModel>> GetDataForEditAsync(int id, CancellationToken cancellationToken)
        {
            var data = await unitOfWork.Sessions.GetByIdAsync(id, cancellationToken);
            if (data is null)
            {
                return Result<SessionEditViewModel>.Failure("No Such Data");
            }

            var editModel = Mapper.Map<SessionEditViewModel>(data);
            return Result<SessionEditViewModel>.Success(editModel);

           
        }

        public async Task<Result> EditSession(SessionEditViewModel vm, CancellationToken cancellationToken)
        {

            var session = await unitOfWork.Sessions.GetByIdAsync( vm.Id, cancellationToken);

            if (session == null)
                return Result.Failure("Session not found");

            if (vm.EndDate <= vm.StartDate)
                return Result.Failure("End date must be after start date");
            var trainerExists = await unitOfWork.Trainers
        .GetByIdAsync(vm.TrainerId, cancellationToken);

            if (trainerExists is null)
                return Result.Failure("Invalid trainer");

           
            session.TrainerId = vm.TrainerId;
            session.StartDate = vm.StartDate;
            session.EndDate = vm.EndDate;
            session.Description = vm.Description;



            await unitOfWork.Sessions.Update(session, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteSession(int id, CancellationToken cancellationToken)
        {
            var data= await unitOfWork.Sessions.SessionsWithBookingsAsync(id, cancellationToken);
            if(data is null)
                return Result.Failure("No Such Data");
            if(data.Bookings.Any())
                return Result.Failure("Cannot delete session with existing bookings");
            if( data.EndDate > DateTime.Now)
                return Result.Failure("Cannot delete upcoming or ongoing sessions");
            await unitOfWork.Sessions.SoftDelete(data, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();

        }
}

    }
    



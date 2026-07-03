using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.Mapper;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.DataAccess.Repositories;
using Gym.DataAccess.UnitOfWork;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public class TrainerService:Service<Trainer>,ITrainerService
    {
        //private readonly ITrainerRepository repo;
        //private readonly IRepository<User> userrepo;
        private readonly IUnitOfWork unitofwork;
        public TrainerService( IUnitOfWork _unitofwork) : base(_unitofwork.Trainers)
        {
            //repo = _repo;
            //userrepo = _userrepo;
            unitofwork = _unitofwork;
        }

        public async Task<List<TrainerViewModel>?> GetAllTrainers(CancellationToken cancellationToken)
        {
            var data = await unitofwork.Trainers.GetAllAsync(cancellationToken);


            //var viewmodel = data.Select(t => new TrainerViewModel
            //{
            //    Id = t.Id,
            //    Name = t.Name,
            //    Phone = t.Phone,
            //    Email = t.Email,
            //    Specialization = t.Specialties.ToString()
            //}
            //    ).ToList();
            var viewmodel = data.Select(t => t.ToTrainerViewModel()).ToList();
            return viewmodel;
        }
        public async Task<TrainerDetailsViewModel?> GetTrainerDetails(int id,CancellationToken cancellationToken=default)
        {
            var data = await unitofwork.Trainers.GetByIdAsync(id, cancellationToken);
            if (data == null) return null;
            //var viewmodel = new TrainerDetailsViewModel
            //{ 
            //Id=data.Id,
            //Name=data.Name,
            //Email=data.Email,
            //DateOfBirth=data.DateOfBirth,
            //Phone=data.Phone,
            //Specialization=data.Specialties.ToString(),
            //Address= $"{data.Address.BuildingNo} - {data.Address.Street} - {data.Address.City}"

            //};
            var viewmodel = data.ToTrainerDetailsViewModel();
            return viewmodel;
        }

        public async Task<Result> CreateTrainerAsync(TrainerCreateViewModel vm, CancellationToken cancellationToken)
        {
            var emailExists = await unitofwork.Persons.ExistAsync(
               m => m.Email == vm.Email,
               cancellationToken);

            if (emailExists)
                return Result.Failure("Email already exists");

            var phoneExists = await unitofwork.Persons.ExistAsync(
                m => m.Phone == vm.Phone,
                cancellationToken);

            if (phoneExists)
                return Result.Failure("Phone already exists");
            //var trainer = new Trainer
            //{
            //    Name = vm.Name,
            //    Email = vm.Email,
            //    Phone = vm.Phone,
            //    DateOfBirth = vm.DateOfBirth,
            //    Gender = vm.Gender,
            //    HireDate = DateTime.Now,
            //    Specialties = vm.Specialties,
            //    Address = new Address
            //    {
            //        BuildingNo = vm.BuildingNo,
            //        Street = vm.Street,
            //        City = vm.City
            //    }
            //};

           var trainer = vm.ToTrainer();

            await unitofwork.Trainers.AddAsync(trainer, cancellationToken);
            await unitofwork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteTrainer(int id, CancellationToken cancellationToken)
        {
            var data = await unitofwork.Trainers.TrainerWithSessionAsync(id, cancellationToken);
            if (data is null)
                return Result.Failure("Trainer is Not Available");
            var ActiveSessions = data.Sessions.Any(b => b.EndDate >= DateTime.Now);
            if (ActiveSessions)
                return Result.Failure("Can't Delete Trainer With Active Sessions");
            await unitofwork.Trainers.SoftDelete(data, cancellationToken);
            await unitofwork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> EditTrainer(EditTrainerViewModel vm, CancellationToken cancellationToken)
        {

            var trainer = await unitofwork.Trainers.GetByIdAsync(vm.Id, cancellationToken);

            if (trainer == null)
                return Result.Failure("Trainer not found");

            var emailExists = await unitofwork.Persons.ExistAsync(
                m => m.Email == vm.Email && m.Id != vm.Id,
                cancellationToken);

            if (emailExists)
                return Result.Failure("Email already exists");

            var phoneExists = await unitofwork.Persons.ExistAsync(
                m => m.Phone == vm.Phone && m.Id != vm.Id,
                cancellationToken);

            if (phoneExists)
                return Result.Failure("Phone already exists");


            trainer.Email = vm.Email;
            trainer.Phone = vm.Phone;

            trainer.Address.BuildingNo = vm.BuildingNo;
            trainer.Address.Street = vm.Street;
            trainer.Address.City = vm.City;
            trainer.Specialties = vm.Specialties;



            await unitofwork.Trainers.Update(trainer, cancellationToken);
            await unitofwork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}

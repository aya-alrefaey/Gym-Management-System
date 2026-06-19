using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gym.BusinessLogic.Mapper
{
    public static class TrainerMappingExtensions
    {
        public static TrainerViewModel ToTrainerViewModel(this Trainer t)
        {
            return new TrainerViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Phone = t.Phone,
                Email = t.Email,
                Specialization = t.Specialties.ToString()
            };
        }
        public static TrainerDetailsViewModel ToTrainerDetailsViewModel(this Trainer t)
        {
            return new TrainerDetailsViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                DateOfBirth = t.DateOfBirth,
                Phone = t.Phone,
                Specialization = t.Specialties.ToString(),
                Address = $"{t.Address.BuildingNo} - {t.Address.Street} - {t.Address.City}"

            };
        }
        public static Trainer ToTrainer(this  TrainerCreateViewModel vm)
        {
            return new Trainer
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                DateOfBirth = vm.DateOfBirth,
                Gender = vm.Gender,
                HireDate = DateTime.Now,
                Specialties = vm.Specialties,
                Address = new Address
                {
                    BuildingNo = vm.BuildingNo,
                    Street = vm.Street,
                    City = vm.City
                }
            };
        }

       
    }
}

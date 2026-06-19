using Gym.Data.contexts;
using Gym.enums;
using Gym.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.DataSeeder
{
    public class TrainerSeeder
    {
        public static async Task SeedTrainers(GymDbcontext dbcontext)
        {
            if (await dbcontext.Trainers.AnyAsync())
            {
                return;
            }

            var trainers = new List<Trainer>
            {
                new Trainer
                {
                    Name = "Ahmed Hassan",
                    Email = "ahmedhassan@gmail.com",
                    Phone = "01012345678",
                    DateOfBirth = new DateTime(1990, 5, 12),
                    Gender = Gender.Male,
                    Specialties = Specialties.GeneralFitness,
                    HireDate = DateTime.Now.AddMonths(-18),
                    Address = new Address
                    {
                        BuildingNo  = "12",
                        Street = "El Horreya Road",
                        City = "Alexandria"
                    }
                },
                new Trainer
                {
                    Name = "Mohamed Ali",
                    Email = "mohamedali@gmail.com",
                    Phone = "01123456789",
                    DateOfBirth = new DateTime(1988, 8, 20),
                    Gender = Gender.Male,
                    Specialties = Specialties.CrossFit,
                    HireDate = DateTime.Now.AddMonths(-12),
                    Address = new Address
                    {
                        BuildingNo  = "45",
                        Street = "Mostafa Kamel Street",
                        City = "Alexandria"
                    }
                },
                new Trainer
                {
                    Name = "Sara Mahmoud",
                    Email = "saramahmoud@gmail.com",
                    Phone = "01234567890",
                    DateOfBirth = new DateTime(1994, 3, 15),
                    Gender = Gender.Female,
                    Specialties = Specialties.Yoga,
                    HireDate = DateTime.Now.AddMonths(-10),
                    Address = new Address
                    {
                        BuildingNo  = "7",
                        Street = "Abou Qir Street",
                        City = "Alexandria"
                    }
                },
                new Trainer
                {
                    Name = "Mariam Adel",
                    Email = "mariamadel@gmail.com",
                    Phone = "01512345678",
                    DateOfBirth = new DateTime(1992, 11, 8),
                    Gender = Gender.Female,
                    Specialties = Specialties.Boxing,
                    HireDate = DateTime.Now.AddMonths(-8),
                    Address = new Address
                    {
                        BuildingNo  = "22",
                        Street = "Fouad Street",
                        City = "Alexandria"
                    }
                },
                new Trainer
                {
                    Name = "Omar Khaled",
                    Email = "omarkhaled@gmail.com",
                    Phone = "01087654321",
                    DateOfBirth = new DateTime(1991, 1, 25),
                    Gender = Gender.Male,
                    Specialties = Specialties.GeneralFitness,
                    HireDate = DateTime.Now.AddMonths(-6),
                    Address = new Address
                    {
                        BuildingNo = "30",
                        Street = "Victor Emmanuel Street",
                        City = "Alexandria"
                    }
                }
            };

            await dbcontext.Trainers.AddRangeAsync(trainers);
            await dbcontext.SaveChangesAsync();
        }
    }
}

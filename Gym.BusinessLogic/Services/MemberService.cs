using AutoMapper;
using Gym.BusinessLogic.Attachment;
using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Member;
using Gym.DataAccess.Repositories;
using Gym.DataAccess.UnitOfWork;
using Gym.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public class MemberService : Service<Member>, IMemberService
    {
        //private readonly IMemberRepository repo;
        //private readonly IRepository<User> userrepo;
        //private readonly IRepository<HealthRecord> recordrepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IAttachment attachment;
        public MemberService(IUnitOfWork _unitofwork,IMapper mapper,IAttachment _attachment ):base(_unitofwork.Members){
            
            unitOfWork = _unitofwork ;
            Mapper = mapper;
            attachment = _attachment;
        }
        public async Task<List<MemberViewModel>> GetAllMembers(CancellationToken cancellationToken)
        {
            var data = await unitOfWork.Members.GetAllAsync(cancellationToken);

            return data.Select(m => Mapper.Map<MemberViewModel>(m)).ToList();
        }

        public async Task<Result> CreateAsync(MemberCreateViewModel vm, CancellationToken cancellationToken)
        {

            var emailExists = await unitOfWork.Users.ExistAsync(
                m => m.Email == vm.Email,
                cancellationToken);

            if (emailExists)
                return Result.Failure("Email already exists");

            var phoneExists = await unitOfWork.Users.ExistAsync(
                m => m.Phone == vm.Phone,
                cancellationToken);

            if (phoneExists)
                return Result.Failure("Phone already exists");

            if (vm.Photo == null || vm.Photo.Length == 0)
                return Result.Failure("Photo is required");

            var photoName = await attachment.UploadFileAsync(
                vm.Photo.OpenReadStream(),
                vm.Photo.FileName,
                "Uploads/Members",
                cancellationToken);

            if (photoName == null)
                return Result.Failure("Failed to upload photo");
            //var member = new Member
            //{
            //    Name = vm.Name,
            //    Email = vm.Email,
            //    Phone = vm.Phone,
            //    DateOfBirth = vm.DateOfBirth,
            //    Gender = vm.Gender,
            //    JoinDate = DateTime.Now,

            //    Address = new Address
            //    {
            //        BuildingNo = vm.BuildingNo,
            //        Street = vm.Street,
            //        City = vm.City
            //    },

            //    HealthRecord = new HealthRecord
            //    {
            //        Height = vm.Height,
            //        Weight = vm.Weight,
            //        BloodType = vm.BloodType,
            //        Note = vm.Note,
            //        LastUpdate = DateTime.Now
            //    }
            //};
            var member = Mapper.Map<Member>(vm);
            member.Photo = photoName;
            await unitOfWork.Members.AddAsync(member, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<MemberDetailsViewModel?> MemberDetails(int id, CancellationToken cancellationToken)
        {
            var model = await unitOfWork.Members.MemberWithMembershipAsync(id, cancellationToken);

            if (model == null)
                return null;
            //var activeMembership = model.Memberships
            // .OrderByDescending(m => m.EndDate)
            //.FirstOrDefault(m => m.EndDate >= DateTime.Now);
            //var viewmodel = new MemberDetailsViewModel
            //{
            //    Name = model.Name,
            //    Phone = model.Phone,
            //    PhotoUrl = model.Photo,
            //    Email = model.Email,
            //    Gender = model.Gender.ToString(),
            //    DateOfBirth = model.DateOfBirth,
            //    Address = $"{model.Address.BuildingNo} - {model.Address.Street} - {model.Address.City}",

            //    Plan = activeMembership?.Plan.Name ?? "No Active Plan",
            //    MembershipStartDate = activeMembership?.StartDate,
            //    MembershipEndDate = activeMembership?.EndDate



            //};
            var viewmodel = Mapper.Map<MemberDetailsViewModel>(model);
            return viewmodel;
        }

        public async Task<Result> DeleteMember(int id, CancellationToken cancellationToken)
        {
            var data = await unitOfWork.Members.MemberWithBookingAsync(id, cancellationToken);
            if (data is null)
                return Result.Failure("Member is Not Available");
            var ActiveBookings = data.Bookings.Any(b => b.Session.EndDate >= DateTime.Now);
            if(ActiveBookings)
                return Result.Failure("Can't Delete Member With Active Bookings");
           var healthrecord= await unitOfWork.HealthRecords.FindItemAsync(r=>r.MemberId==id, cancellationToken);
            if (healthrecord is not null)
            {
                await unitOfWork.HealthRecords.SoftDelete(healthrecord, cancellationToken);
            }
            if (data.Photo is not null) {
                await attachment.DeleteFileAsync(data.Photo, "Uploads/Members", cancellationToken);
            }
            await unitOfWork.Members.SoftDelete(data, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<EditMemberViewModel?> GetMemberForEdit( int id,CancellationToken cancellationToken)
        {
            var data = await unitOfWork.Members.GetByIdAsync(id, cancellationToken);

            if (data == null)
                return null;

            return new EditMemberViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Phone = data.Phone,
                PhotoUrl = data.Photo,
                BuildingNo = data.Address.BuildingNo,
                City = data.Address.City,
                Street = data.Address.Street
            };
            //var viewmodel = Mapper.Map<EditMemberViewModel>(data);
            //return viewmodel;
        }
        public async Task<Result> EditMember(EditMemberViewModel vm, CancellationToken cancellationToken)
        {

            var member = await unitOfWork.Members.GetByIdAsync(vm.Id, cancellationToken);

            if (member == null)
                return Result.Failure("Member not found");

            var emailExists = await unitOfWork.Users.ExistAsync(
                m => m.Email == vm.Email && m.Id != vm.Id,
                cancellationToken);

            if (emailExists)
                return Result.Failure("Email already exists");

            var phoneExists = await unitOfWork.Users.ExistAsync(
                m => m.Phone == vm.Phone && m.Id != vm.Id,
                cancellationToken);

            if (phoneExists)
                return Result.Failure("Phone already exists");


            member.Email = vm.Email;
            member.Phone = vm.Phone;

            member.Address.BuildingNo = vm.BuildingNo;
            member.Address.Street = vm.Street;
            member.Address.City = vm.City;
            //Mapper.Map(vm, member);

            await unitOfWork.Members.Update(member, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using AutoMapper;
using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Membership;
using Gym.DataAccess.UnitOfWork;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public class MembershipService:Service<Membership>, IMembershipService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public MembershipService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork.Memberships)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<List<MembershipViewModel>> GetAllMembershipsAsync(CancellationToken cancellationToken = default)
        {
            var data= await unitOfWork.Memberships.GetAllMembershipswithDetailsAsync(cancellationToken);
            //var memberships = data.Select(m => new MembershipViewModel
            //{
            //    Id = m.Id,
            //    StartDate = m.StartDate,
            //    EndDate = m.EndDate,
            //    MemberId = m.MemberId,
            //    MemberName = m.Member.Name,
            //    PlanName = m.Plan.Name


            //}).ToList();
            var memberships = data.Select(m => mapper.Map<MembershipViewModel>(m)).ToList();
            return memberships;
        }
        public async Task<Result> CreateMembershipAsync(MembershipCreateViewModel vm, CancellationToken cancellationToken = default)
        {
            var member = await unitOfWork.Members.MemberWithMembershipAsync(vm.MemberId, cancellationToken);
            if (member is null)
            {
                return Result.Failure("Member not found.");
            }
            if (member.Memberships.Any(m => m.EndDate >= DateTime.Now))
            {
                return Result.Failure("Member already has an active membership.");
            }

            var plan = await unitOfWork.Plans.GetByIdAsync(vm.PlanId, cancellationToken);
            if (plan is null)
            {
                return Result.Failure("Plan not found.");
            }

            //var membership = new Membership
            //{
            //    MemberId = vm.MemberId,
            //    PlanId = vm.PlanId,
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now .AddDays(plan.DurationDays)

            //};
            var membership = mapper.Map<Membership>(vm);
            membership.EndDate = membership.StartDate.AddDays(plan.DurationDays);


            await unitOfWork.Memberships.AddAsync(membership, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> CancelMembershipAsync(int id, CancellationToken cancellationToken = default)
        {
           var membership = await unitOfWork.Memberships.GetByIdAsync(id, cancellationToken);
            if (membership is null)
            {
                return Result.Failure("Membership not found.");
            }
            if (membership.EndDate <= DateTime.Now)
            {
                return Result.Failure("Membership is already expired.");
            }
            membership.EndDate = DateTime.Now;
            await unitOfWork.Memberships.Update(membership, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success();
        }
    }
}

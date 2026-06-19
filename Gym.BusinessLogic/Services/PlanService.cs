using AutoMapper;
using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Plan;
using Gym.DataAccess.Repositories;
using Gym.DataAccess.UnitOfWork;
using Gym.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace Gym.BusinessLogic.Services
{
    public class PlanService : Service<Plan>, IPlanService
    {
        //private readonly IPlanRepository _repo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        public PlanService(IUnitOfWork _unitOfWork,IMapper _mapper)
       : base(_unitOfWork.Plans)
        {
            unitOfWork = _unitOfWork;
            Mapper = _mapper;
        }

        public async Task<List<PlanListViewModel>> GetAllPlansAsync(CancellationToken cancellationToken)
        {
            var plans = await unitOfWork.Plans.GetAllAsync(cancellationToken);

            //return plans.Select(p => new PlanListViewModel
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Price = p.Price,
            //    Description = p.Description,
            //    DurationDays = p.DurationDays,
            //    IsActive = p.IsActive
            //}).ToList();
            return plans.Select(p => Mapper.Map<PlanListViewModel>(p)).ToList();
        }
        public async Task<PlanDetailsViewModel?> GetPlanDetailsAsync(int id, CancellationToken cancellationToken)
        {
            var plan = await unitOfWork.Plans.GetByIdAsync(id, cancellationToken);

            if (plan == null)
                return null;

            //return new PlanDetailsViewModel
            //{
            //    Name = plan.Name,
            //    Description = plan.Description,
            //    DurationDays = plan.DurationDays,
            //    Price = plan.Price,
            //    IsActive = plan.IsActive
            //};
            return Mapper.Map<PlanDetailsViewModel>(plan);
        }
        public async Task<Result> ActivationControl(int id,CancellationToken cancellationToken)
        {
            var plan = await unitOfWork.Plans.GetPlanWithMembershipsAsync(id,cancellationToken);
            if (plan == null)
            {
                return Result.Failure("Plan not Found");
            }
            if (!plan.IsActive)
            {
                plan.IsActive = true;
            }
            else
            {
                var hasActiveMemberships = plan.Memberships
                    .Any(m => m.EndDate > DateTime.Now);

                if (hasActiveMemberships)
                {
                   return Result.Failure("Can't Deactivate a plan with active memberships");
                  
                }

                plan.IsActive = false;
            }
            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<PlanEditViewModel?> GetPlanForEdit(int id, CancellationToken cancellationToken)
        {
            var plan = await unitOfWork.Plans.GetByIdAsync(id, cancellationToken);

            if (plan == null)
                return null;

            //return new PlanEditViewModel
            //{
            //    Name = plan.Name,
            //    Description = plan.Description,
            //    Price = plan.Price,
            //    DurationDays = plan.DurationDays
            //};
            return Mapper.Map<PlanEditViewModel>(plan);
        }
        public async Task<Result> Edit(int id, PlanEditViewModel dto, CancellationToken cancellationToken)
        {
            var plan = await unitOfWork.Plans.GetPlanWithMembershipsAsync(id, cancellationToken);

            if (plan == null)
                return Result.Failure("Plan not Found");

            var hasActiveMemberships = plan.Memberships
                .Any(m => m.EndDate > DateTime.Now);

            if (hasActiveMemberships)
                return Result.Failure("Can't Edit a plan with active memberships");

            plan.Name = dto.Name;
            plan.Description = dto.Description;
            plan.Price = dto.Price;
            plan.DurationDays = dto.DurationDays;
            await unitOfWork.Plans.Update(plan, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }

       
    }
}

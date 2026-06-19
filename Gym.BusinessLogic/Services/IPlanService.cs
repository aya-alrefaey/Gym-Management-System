using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Plan;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gym.BusinessLogic.Services
{
    public interface IPlanService:IService<Plan>
    {
        public Task<List<PlanListViewModel>> GetAllPlansAsync(CancellationToken cancellationToken);
        public Task<Result> ActivationControl(int id, CancellationToken cancellationToken);
        Task<PlanDetailsViewModel?> GetPlanDetailsAsync(int id, CancellationToken cancellationToken);
        public Task<PlanEditViewModel?> GetPlanForEdit(int id, CancellationToken cancellationToken);
        public Task<Result> Edit(int id, PlanEditViewModel olddata, CancellationToken cancellationToken);
        
    }
}

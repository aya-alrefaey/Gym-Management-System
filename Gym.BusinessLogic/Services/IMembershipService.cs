using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Membership;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public interface IMembershipService: IService<Membership>
    {
        public Task<List<MembershipViewModel>> GetAllMembershipsAsync(CancellationToken cancellationToken = default);
        public Task<Result> CreateMembershipAsync(MembershipCreateViewModel model, CancellationToken cancellationToken = default);
        public  Task<Result> CancelMembershipAsync(int id, CancellationToken cancellationToken = default);
    }
}

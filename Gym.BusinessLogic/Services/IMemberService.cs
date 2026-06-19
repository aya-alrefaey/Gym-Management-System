using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Member;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public interface IMemberService:IService<Member>
    {
        public Task<List<MemberViewModel>> GetAllMembers(CancellationToken cancellationToken);
        public Task<Result> CreateAsync(MemberCreateViewModel member , CancellationToken cancellationToken);
        public Task<MemberDetailsViewModel?> MemberDetails(int id, CancellationToken cancellationToken);
        public Task<Result> DeleteMember(int id, CancellationToken cancellationToken);
        Task<EditMemberViewModel?> GetMemberForEdit(int id, CancellationToken cancellationToken);
        public Task<Result> EditMember(EditMemberViewModel member, CancellationToken cancellationToken);
    }
}

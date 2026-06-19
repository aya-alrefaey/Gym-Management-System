using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.Repositories
{
    public interface IMemberRepository:IRepository<Member>
    {
        public Task<Member> MemberWithMembershipAsync(int id, CancellationToken cancellationToken);
        public Task<Member> MemberWithBookingAsync(int id, CancellationToken cancellationToken);
        public  Task<Member?> MemberWithMembershipOnlyAsync(int id, CancellationToken cancellationToken);
        public Task<bool> HasValidMembershipAsync(int memberId, CancellationToken cancellationToken = default);
    }
}

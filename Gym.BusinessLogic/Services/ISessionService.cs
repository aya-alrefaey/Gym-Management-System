using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Session;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public interface ISessionService:IService<Session>
    {
        public  Task<List<SessionViewModel>> GetAllSessions(CancellationToken cancellationToken);
        public  Task<Result> CreateSessionAsync(SessionCreateViewModel vm, CancellationToken cancellationToken);
        public  Task<Result<SessionViewModel>> GetSessionByIdAsync(int id, CancellationToken cancellationToken);
        public  Task<Result<SessionEditViewModel>> GetDataForEditAsync(int id, CancellationToken cancellationToken);
        public Task<Result> EditSession(SessionEditViewModel vm, CancellationToken cancellationToken);
        public Task<Result> DeleteSession(int id, CancellationToken cancellationToken);
      
    }
}

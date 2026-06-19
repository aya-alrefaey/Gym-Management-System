using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public interface ITrainerService:IService<Trainer>
    {
        public Task<List<TrainerViewModel>?> GetAllTrainers(CancellationToken cancellationToken);
        public  Task<TrainerDetailsViewModel?> GetTrainerDetails(int id, CancellationToken cancellationToken);
        public Task<Result> CreateTrainerAsync(TrainerCreateViewModel member, CancellationToken cancellationToken);
        public Task<Result> DeleteTrainer(int id, CancellationToken cancellationToken);
        public  Task<Result> EditTrainer(EditTrainerViewModel vm, CancellationToken cancellationToken);
    }
}

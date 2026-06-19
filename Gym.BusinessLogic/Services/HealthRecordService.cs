using Gym.BusinessLogic.ViewModels.HealthRecord;
using Gym.DataAccess.Repositories;
using Gym.DataAccess.UnitOfWork;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public class HealthRecordService : Service<HealthRecord>, IHealthRecordService
    {
        //private readonly IRepository<HealthRecord> repo;
        private readonly IUnitOfWork unitOfWork;
        public HealthRecordService(IUnitOfWork _unitOfWork) : base(_unitOfWork.HealthRecords)
        {
                //repo = repository;
                unitOfWork = _unitOfWork;
        }
        public async Task<HealthRecordViewModel?> GetHealthRecord(int id, CancellationToken cancellationToken)
        {
            var data = await unitOfWork.HealthRecords.FindItemAsync(h=> h.MemberId==id,cancellationToken);
            if(data==null) return null;
            var viewmodel= new HealthRecordViewModel
            {
                Weight=data.Weight,
                Height=data.Height,
                Note=data.Note,
                BloodType=data.BloodType

            };
            return viewmodel;
        }

    }
}

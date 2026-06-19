using Gym.BusinessLogic.ViewModels.HealthRecord;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.BusinessLogic.Services
{
    public interface IHealthRecordService:IService<HealthRecord>
    {
        public Task<HealthRecordViewModel?> GetHealthRecord(int id,CancellationToken cancellationToken);
    }
}

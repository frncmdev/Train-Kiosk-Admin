using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.models.DbEntities;
namespace core.services
{
    public interface ITrainStationService
    {
        public Task<IEnumerable<TrainStation>> Get();
        // gets all close stations using toListAsync();
        public Task<TrainStation> Get(int id);
        // gets one station.
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.models;
using core.models.RequestResults;
namespace core.services
{
    public interface IStationService
    {
        public Task<Campus> GetSelectedStation();
        public Task<IEnumerable<Campus>> GetAllStations();
        public Task<bool> ChangeSelectedStation(Campus _campus);
        public Task<TrainStation> GetStation(CampusDTO _campus);
    }
}
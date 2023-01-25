using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.dal;
using core.models.DbEntities;
namespace core.services
{
    public class TrainStationService: ITrainStationService
    {
        private readonly KioskAdminContext _context; 
        public TrainStationService()
        {
            _context = new KioskAdminContext();
        }
        public async Task<IEnumerable<TrainStation>> Get()
        {
            return await _context.TrainStations.ToListAsync();
        }
        public async Task<TrainStation> Get(int _id)
        {
            TrainStation _trainStation = await _context.TrainStations.SingleOrDefaultAsync(_trainS => _trainS.TrainstationId == _id);
            if(_trainStation is not null)
                return _trainStation;
            return null;

        }
    }
}
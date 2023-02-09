using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.models;
using core.dal;
namespace core.services
{
    public class StationService: IStationService
    {
        private readonly KioskAdminContext _context;
        public StationService()
        {
            _context = new KioskAdminContext();
        }

        public async Task<bool> ChangeSelectedStation(Campus _campus)
        {
            Campus _newSelected = await _context.Campuses.SingleOrDefaultAsync(_item => _item.CampusId == _campus.CampusId);
            Campus _currentlySelected = await _context.Campuses.SingleOrDefaultAsync(_item => _item.IsSelected == true);
            if(_currentlySelected.CampusId == _newSelected.CampusId)
                return true;
            _currentlySelected.IsSelected = false;
            _newSelected.IsSelected = true;
            // _context.UpdateRange(new Campus[] {_currentlySelected, _campus});
            _context.Entry(_currentlySelected).State = EntityState.Modified;
            _context.Entry(_newSelected).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            if(await _checkChanges(_campus))
                return true;
            return false;
        }
        
        public async Task<IEnumerable<Campus>> GetAllStations()
        {
            return await _context.Campuses.ToListAsync();
        }

        public async Task<Campus> GetSelectedStation()
        {
            Campus _value = await _context.Campuses.SingleOrDefaultAsync(_campus => _campus.IsSelected == true);
            if(_value is not null)
                return _value;
            return null;
        }
        private async Task<bool> _checkChanges(Campus _campus)
        {
            Campus _campusToCheck = await _context.Campuses.SingleOrDefaultAsync(_camp => _camp.CampusId == _campus.CampusId);
            if(_campusToCheck.IsSelected)
                return true;
            return false;
        }
    }
}
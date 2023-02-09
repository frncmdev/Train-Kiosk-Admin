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
        /*
            This is a method in C# that performs an asynchronous operation to change the selected station in a given context. The method takes a single input argument of type Campus named _campus.
            The method starts by fetching two instances of the Campus entity from the database context, one for the newly selected station (_newSelected) and one for the currently selected station (_currentlySelected).
            Next, the method checks if the CampusId of the new station is the same as the CampusId of the currently selected station. If they are the same, the method returns true immediately, indicating that the station is already selected and no change is necessary.
            If the CampusIds are different, the method sets the IsSelected property of the currently selected station to false and sets the IsSelected property of the new station to true.
            The method then updates the state of both Campus instances in the database context to EntityState.Modified to indicate that they have been modified and need to be saved.
            Finally, the method calls the SaveChangesAsync() method to save the changes to the database, and calls another method named _checkChanges with the new station as an argument. If _checkChanges returns true, the method returns true to indicate that the change was successful. If _checkChanges returns false, the method returns false to indicate that the change was not successful.
        */
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
        // gets all campuses
        public async Task<IEnumerable<Campus>> GetAllStations()
        {
            return await _context.Campuses.ToListAsync();
        }
        // gets the campus that is currently selected
        public async Task<Campus> GetSelectedStation()
        {
            Campus _value = await _context.Campuses.SingleOrDefaultAsync(_campus => _campus.IsSelected == true);
            if(_value is not null)
                return _value;
            return null;
        }
        // Checks if the campus provided is selected
        private async Task<bool> _checkChanges(Campus _campus)
        {
            Campus _campusToCheck = await _context.Campuses.SingleOrDefaultAsync(_camp => _camp.CampusId == _campus.CampusId);
            if(_campusToCheck.IsSelected)
                return true;
            return false;
        }
    }
}
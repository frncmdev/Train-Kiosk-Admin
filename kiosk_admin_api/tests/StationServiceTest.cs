using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace tests
{
    public class StationServiceTest
    {
        private readonly StationService _service = new StationService();
        [Fact]
        public async Task TestGetAll()
        {
            IEnumerable<Campus> _stations = await _service.GetAllStations();
            String[] _campusNames = new string[] {
                "Cremorne",
                "Broadmeadows",
                "Docklands",
                "Essendon"
            };
            int successes = 0;
            int idx = 0;
            foreach(Campus _campus in _stations)
            {
                if(_campus.CampusName == _campusNames[idx])
                {
                    successes +=1;
                }
                idx += 1;
            }
            if(successes == 4)
                Assert.True(true);
            else 
                Assert.True(false);
        }
        [Fact]
        public async Task TestGetSelected()
        {
            int successes = 0;
            Campus _selectedCampus = await _service.GetSelectedStation();
            if(_selectedCampus.CampusName.ToLower() == "cremorne")
                successes +=1;
            if(_selectedCampus.CampusName.ToLower() != "broadmeadows")
                successes +=1;
            if(successes == 2)
                Assert.True(true);
            else 
                Assert.True(false);
        }
        [Fact]
        public async Task TestChangeSelected()
        {
            int successes = 0;
            await _service.ChangeSelectedStation(_campusBuilder(2,"Broadmeadows", false, 1028));
            Campus _sc = await _service.GetSelectedStation();
            if(_sc.CampusName.ToLower() == "broadmeadows")
                successes +=1;
            await _service.ChangeSelectedStation(_campusBuilder(1,"Cremorne", false, 1162));
            _sc = await _service.GetSelectedStation();
            if(_sc.CampusName.ToLower() != "broadmeadows" && _sc.CampusName.ToLower() == "cremorne")
                successes += 1;
            if(successes == 2)
                Assert.True(true);
            else 
                Assert.True(false);
        }
        private Campus _campusBuilder(int _id, string _campusName, bool _isSelected, int _trainStationId)
        {
            Campus _campus = new Campus();
            _campus.CampusId = _id;
            _campus.CampusName = _campusName;
            _campus.IsSelected = _isSelected;
            _campus.TrainstationId = _trainStationId;
            return _campus;
        }
    }
}
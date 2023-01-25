using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.models.DbEntities;
namespace core.services
{
    public interface IDirectionService
    {
        public Task<IEnumerable<Direction>> Get();
        public Task<Direction> Get(int _directionId);
    }
}
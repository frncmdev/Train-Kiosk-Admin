using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.services;
using core.models.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainStationController : ControllerBase
    {
        private readonly ITrainStationService _service;
        public TrainStationController(ITrainStationService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainStation>>> Get()
        {
            return Ok(await _service.Get());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TrainStation>> Get(int _id)
        {
            TrainStation _trainStation = await _service.Get(_id);
            if(_trainStation is null)
                return NotFound();
            return Ok(_trainStation);

        }
    }
}
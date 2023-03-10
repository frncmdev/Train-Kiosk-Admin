using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core.services;
using core.models;
using core.models.RequestResults;
namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StationController : ControllerBase
    {
        private readonly IStationService _service;
        public StationController(IStationService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campus>>> GetAll()
        {
            return Ok(await _service.GetAllStations());
        }
        [HttpGet]
        [Route("getSelected")]
        public async Task<ActionResult<Campus>> GetSelected()
        {
            return Ok(await _service.GetSelectedStation());
        }
        [HttpGet]
        [Route("getStationToSelected")]
        public async Task<ActionResult<TrainStation>> GetStation([FromQuery]CampusDTO _campus)
        {
            return Ok(await _service.GetStation(_campus));
        }
        [HttpPost]
        [Route("changeStation")]
        public async Task<IActionResult> ChangeCampus(Campus _campus)
        {
            if(await _service.ChangeSelectedStation(_campus))
                return Ok();
            else 
                return BadRequest();
        }
    }
}
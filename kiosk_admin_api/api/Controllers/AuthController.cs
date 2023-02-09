using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.models.RequestResults;
using core.services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest _loginRequest)
        {
            var tuple = await _service.Login(_loginRequest);
            if(tuple.Item1)
            {
                return Ok(tuple.Item2);
            }
            return Forbid();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest _registerRequest)
        {
            RegisterResult _result = await _service.AddUser(_registerRequest);
            RegisterResultEnum _resultEnum = _result.result;
            switch (_resultEnum)
            {
                case RegisterResultEnum.Successful:
                    return NoContent();
                case RegisterResultEnum.NotAuthorizedError:
                    return Unauthorized();
                case RegisterResultEnum.DatabaseError:
                    return ValidationProblem();
                case RegisterResultEnum.UserAlreadyExistsError:
                    return BadRequest();
                default: 
                    return StatusCode(418);
            }
        }
    }
}
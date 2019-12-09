using Dto;
using Logic.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationLogic _authenticationLogic;

        public AuthenticationController(IAuthenticationLogic authenticationLogic)
        {
            _authenticationLogic = authenticationLogic;
        }

        // POST api/users/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserLoginDto user)
        {
            var token = await _authenticationLogic.LoginAsync(user);
            if (token == null)
                return BadRequest();

            return Ok(token);
        }

        // POST api/users/refresh
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest user)
        {
            var token = await _authenticationLogic.RefreshTokenAsync(user);
            if (token == null)
                return BadRequest();

            return Ok(token);
        }
    }
}

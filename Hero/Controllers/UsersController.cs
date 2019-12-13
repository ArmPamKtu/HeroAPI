using Dto;
using Hero.Extensions;
using Logic.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Exceptions;

namespace Hero.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UsersController(IUserLogic logic)
        {
            _userLogic = logic;
        }


        // GET: api/users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ICollection<UserDto>> Get()
        {
            var users = await _userLogic.GetAsync();
            return users;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            var user = await _userLogic.GetByIdAsync(id);

            if (user == null)
                return NotFound();
            return user;
            
            
        }

        // GET api/users/5
        [HttpGet("myUser")]
        public async Task<ActionResult<UserDto>> GetMyUser()
        {
            var userId = HttpContext.GetUserId();
            var user = await _userLogic.GetByIdAsync(userId);
            if (user == null)
                return NotFound();
            return user;
        }

        // PUT api/users/{id}
        [HttpPatch("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> Patch(string id, [FromBody]UserPasswordUpdateDto user)
        {
            var userId = HttpContext.GetUserId();
            if (id != userId)
            {
                return BadRequest();
            }
            var success = await _userLogic.UpdatePasswordAsync(userId, user);
            if (success)
                return Ok();
            return NotFound();
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var success = await _userLogic.DeleteAsync(id);

            if (success)
                return Ok();
            return NotFound();
        }

        // POST api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]UserRegistrationDto user)
        {
            var success = await _userLogic.RegisterAsync(user);
            if (success)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post()
        {
            await _userLogic.UsersExist();

           /* var success = await _userLogic.UsersExist();
            if (success)*/
                return Ok();

           /* return BadRequest();*/
        }

    }
}

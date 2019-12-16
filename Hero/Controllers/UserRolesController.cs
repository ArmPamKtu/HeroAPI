using Dto;
using Hero.Extensions;
using Logic.UserRoles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleLogic _userRoleLogic;

        public UserRolesController(IUserRoleLogic logic)
        {
            _userRoleLogic = logic;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ICollection<string>>> Get(string id)
        {
            var roles = await _userRoleLogic.GetById(id);
            if (roles == null)
                return NotFound();
            return Ok(roles);
        }

        // POST api/userroles
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRoleDto request)
        {
            await _userRoleLogic.Create(request);
            return Ok();
        }

        [HttpGet("isAdmin")]
        public async Task<bool> IsAdmin()
        {
            var userId = HttpContext.GetUserId();

            var isadmin = await _userRoleLogic.IsAdmin(userId);
            if(isadmin)
                return true;
            else
                return false;
        }

        [HttpGet("isManager")]
        public async Task<bool> IsManager()
        {
            var userId = HttpContext.GetUserId();

            var isadmin = await _userRoleLogic.IsManager(userId);
            if (isadmin)
                return true;
            else
                return false;
        }
    }
}

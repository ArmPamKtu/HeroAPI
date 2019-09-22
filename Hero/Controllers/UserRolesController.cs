using Dto;
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

        [HttpGet]
        public ActionResult<ICollection<UserRoleDto>> Get()
        {
            var users = _userRoleLogic.GetAll().ToList();
            return users;
        }

        [HttpPost("logIn")]
        async public Task<ActionResult> Post([FromBody] UserRoleDto userDto)
        {
            // decryptinti reikes db ir susiuziureti ar yra toks vartotojas
            var users = _userRoleLogic.GetAll().ToList();
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<ICollection<UserRoleDto>> GetById(string id)
        {
            var users = _userRoleLogic.GetAll().ToList();
            return users;
        }

    }

}

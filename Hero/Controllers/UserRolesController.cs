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

        [HttpGet("{id}")]
        public ActionResult<ICollection<UserRoleDto>> GetById(Guid id)
        {
            var users = _userRoleLogic.GetUserRoles(id).ToList();
            return users;
        }

        [HttpPost]
        async public Task<ActionResult> Post([FromBody] UserRoleDto userRoleDto)
        {
            // decryptinti reikes db ir susiuziureti ar yra toks vartotojas
            _userRoleLogic.Create(userRoleDto);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] UserRoleDto userDto)
        {
            //patikrinti ar tikrai tas vartotjas
            bool update = _userRoleLogic.Update(userDto.UserGuid, userDto);
            if (update == true)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {

            bool delete = _userRoleLogic.Delete(id);
            if (delete == true)
                return Ok();
            else
                return BadRequest();
        }


    }

}

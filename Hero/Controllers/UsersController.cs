using Dto;
using Logic.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UsersController(IUserLogic logic)
        {
            _userLogic = logic;
        }

        [HttpGet]
        public ActionResult<ICollection<UserDto>> Get()
        {
            var users = _userLogic.GetAll().ToList();
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<ICollection<UserDto>> GetById(Guid id)
        {
            var users = _userLogic.GetUser(id).ToList();
            return users;
        }

        [HttpPost("logIn")]
        async public Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            // decryptinti reikes db ir susiuziureti ar yra toks vartotojas


            _userLogic.Create(userDto);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] UserDto userDto)
        {
            //patikrinti ar tikrai tas vartotjas
            bool update = _userLogic.Update(userDto.Id, userDto);
            if (update == true)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {

            bool delete = _userLogic.Delete(id);
            if (delete == true)
                return Ok();
            else
                return BadRequest();
        }




    }
}

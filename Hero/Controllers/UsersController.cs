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

        [HttpPost("logIn")]
        async public Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            // decryptinti reikes db ir susiuziureti ar yra toks vartotojas


            _userLogic.Create(userDto);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<ICollection<UserDto>> GetById(string id)
        {
            var users = _userLogic.GetAll().ToList();
            return users;
        }

    }
}

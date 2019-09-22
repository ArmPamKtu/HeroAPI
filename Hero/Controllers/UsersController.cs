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
    }
}

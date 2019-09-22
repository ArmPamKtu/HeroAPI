using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Users
{
    public class UserLogic : GenericLogic<IRepository<User>, UserDto, User>, IUserLogic
    {
        public UserLogic(IRepository<User> repository) : base(repository)
        {

        }
        public bool IsStoreManager(int i)
        {
            if(i > 0)
                return false;
            else
                return true;
        }
    }
}

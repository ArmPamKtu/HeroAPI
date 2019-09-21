using Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Users
{
    public class UserLogic : IUserLogic
    {
        public bool IsStoreManager(int i)
        {
            if(i > 0)
                return false;
            else
                return true;
        }
    }
}

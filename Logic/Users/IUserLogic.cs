using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Users
{
    public interface IUserLogic
    {
        bool IsStoreManager(int i);
        ICollection<UserDto> GetAll();
        ICollection<UserDto> GetUser(Guid guid);
    }
}

using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Users
{
    public interface IUserLogic
    {
        bool IsStoreManager(int i);
        void Create(UserDto userDto);
        ICollection<UserDto> GetAll();
        ICollection<UserDto> GetUser(Guid guid);
    }
}

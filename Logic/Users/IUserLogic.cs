using Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Users
{
    public interface IUserLogic
    {
        Task<bool> RegisterAsync(UserRegistrationDto request);

        /*
        bool IsStoreManager(int i);
        void Create(UserDto userDto);
        ICollection<UserDto> GetAll();
        ICollection<UserDto> GetUser(Guid guid);
        bool Update(Guid id, UserDto userRoleDto);
        bool Delete(Guid id);*/
    }
}

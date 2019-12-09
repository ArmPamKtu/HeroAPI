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
        Task<bool> UpdatePasswordAsync(string Id, UserPasswordUpdateDto request);
        Task<ICollection<UserDto>> GetAsync();
        Task<bool> DeleteAsync(string id);
        Task<UserDto> GetByIdAsync(string id);
    }
}

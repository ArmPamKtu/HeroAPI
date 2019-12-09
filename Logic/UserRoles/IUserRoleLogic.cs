using Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UserRoles
{
    public interface IUserRoleLogic
    {
        Task<bool> Create(UserRoleDto request);

        Task<bool> IsAdmin(string id);

        Task<bool> IsManager(string id);
        Task<ICollection<string>> GetById(string id);
    }
}

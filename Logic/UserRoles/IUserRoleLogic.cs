using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.UserRoles
{
    public interface IUserRoleLogic
    {
        ICollection<UserRoleDto> GetAll();
        ICollection<UserRoleDto> GetUserRoles(Guid guid);
        void Create(UserRoleDto userRoleDto);
        bool Update(Guid id, UserRoleDto userRoleDto);
        bool Delete(Guid id);
    }
}

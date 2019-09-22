using DbManager.Generic;
using Logic.Generic;
using System;
using Db.Entities;
using Dto;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Logic.UserRoles
{
    public class UserRoleLogic : GenericLogic<IRepository<UserRole>, UserRoleDto, UserRole>, IUserRoleLogic
    {
        public UserRoleLogic(IRepository<UserRole> repository) : base(repository)
        {

        }


        public ICollection<UserRoleDto> GetUserRoles(Guid guid)
        {
            var userRoles = Repository.GetMany(x => x.UserGuid == guid);
            var mappedRoles= Mapper.Map<List<UserRoleDto>>(userRoles);

            return mappedRoles;
        }

    }
}

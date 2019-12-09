using DbManager.Generic;
using Logic.Generic;
using System;
using Db.Entities;
using Dto;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Logic.Exceptions;

namespace Logic.UserRoles
{
    public class UserRoleLogic : IUserRoleLogic
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserRoleLogic(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<bool> Create(UserRoleDto request)
        {
            var user = await GetUser(request.UserId);


            var role = await GetRole(request.IdentityRoleId);
       
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<bool> Update(UserRoleDto request)
        {    
            var user = await GetUser(request.UserId);
            var role = await GetRole(request.IdentityRoleId);
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<bool> IsAdmin(string id)
        {
            var roles = await GetById(id);
            var isAdmin = roles.Contains(UserRoleTypes.Admin.ToString());
            return isAdmin;
        }

        public async Task<bool> IsManager(string id)
        {
            var roles = await GetById(id);
            var isManager = roles.Contains(UserRoleTypes.Manager.ToString());
            return isManager;
        }

        public async Task<ICollection<string>> GetById(string id)
        {
            var user = await GetUser(id);

            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        private async Task<User> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new BusinessException(ExceptionCode.UserDoesNotExist);
            return user;
        }

        private async Task<IdentityRole> GetRole(string id)
        {
            //roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            //roleResult = await RoleManager.CreateAsync(new IdentityRole("Manager"));
            var roles = _roleManager.Roles;
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                throw new BusinessException(ExceptionCode.RoleDoesNotExist);
            return role;
        }

    }
}

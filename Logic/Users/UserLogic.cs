using AutoMapper;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Exceptions;
using Logic.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Users
{
    public class UserLogic : IUserLogic
    {
        private readonly UserManager<User> _userManager;
        public UserLogic(UserManager<User> userManager) 
        {
            _userManager = userManager;
        }

        public async Task<bool> RegisterAsync(UserRegistrationDto request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new BusinessException(ExceptionCode.EmailAlreadyExists);

            var newUser = new User
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                UserName = request.Email
            };

            var createdUser = await _userManager.CreateAsync(newUser, request.Password);

            if (!createdUser.Succeeded)
                return false;
            return true;
        }

        /*

        public ICollection<UserDto> GetUser(Guid guid)
        {
            var user = Repository.GetMany(x => x.Id == guid);
            var mappedUser = Mapper.Map<List<UserDto>>(user);

            return mappedUser;
        }

        public bool IsStoreManager(int i)
        {
            if(i > 0)
                return false;
            else
                return true;
        }*/
    }
}

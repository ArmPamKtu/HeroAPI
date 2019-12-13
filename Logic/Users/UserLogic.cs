using AutoMapper;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Exceptions;
using Logic.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> UpdatePasswordAsync(string Id, UserPasswordUpdateDto request)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                throw new BusinessException(ExceptionCode.UserDoesNotExist);

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.OldPassword);

            if (!userHasValidPassword)
                throw new BusinessException(ExceptionCode.IncorrectPassword);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            var existingUser = await _userManager.FindByIdAsync(Id);
            if (existingUser == null)
                throw new BusinessException(ExceptionCode.UserDoesNotExist);

            var result = await _userManager.DeleteAsync(existingUser);

            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<ICollection<UserDto>> GetAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var mappedUsers = AutoMapper.Mapper.Map<ICollection<UserDto>>(users);

            return mappedUsers;
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;
            var mappedUser = AutoMapper.Mapper.Map<UserDto>(user);
            return mappedUser;
        }

        public async Task<bool> UsersExist()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users != null)
                return true;
            else
                throw new BusinessException(ExceptionCode.Unhandled);

        }
    }
}

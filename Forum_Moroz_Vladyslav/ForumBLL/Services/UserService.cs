using AutoMapper;
using ForumBLL.DTO;
using ForumBLL.Interfaces;
using ForumDAL.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;

            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<UserDTO, User>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public async Task DeleteUser(string email)
        {
            var userToDelete = await _userManager.FindByEmailAsync(email);
            
            if (userToDelete == null)
            {
                throw new ArgumentException($"User with email '{email}' does not exists");
            }

            var userRoles = await _userManager.GetRolesAsync(userToDelete);

            if (userRoles.Count() > 0)
            {
                foreach (var role in userRoles.ToList())
                {
                    var result = await _userManager.RemoveFromRoleAsync(userToDelete, role);
                }
            }
            await _userManager.DeleteAsync(userToDelete);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetCurrentUserAsync(string email)
        {
            var userToFind = await _userManager.FindByEmailAsync(email);

            if (userToFind == null)
            {
                throw new Exception($"There is no user with email: '{email}' in database");
            }
            else
            {
                return userToFind;
            }
        }

        public async Task UpdateUser(UserDTO user)
        {
            var userToUpdate = await _userManager.FindByEmailAsync(user.Email);
            if (userToUpdate == null)
                throw new ArgumentException($"There is no user with id: {user.Id} in database");

            userToUpdate.Email = user.Email;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;

            await _userManager.UpdateAsync(userToUpdate);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            User userToFind = await _userManager.FindByIdAsync(id);
            
            if (userToFind == null)
            {
                throw new Exception($"There is no user with id: {id} in database");
            }
            return userToFind;
        }

        public async Task<string> GetUserIdAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
          
            if (user == null)
            {
                throw new ArgumentException($"There is no user with email: '{email}' in database");
            }

            return user.Id;
        }
    }
}

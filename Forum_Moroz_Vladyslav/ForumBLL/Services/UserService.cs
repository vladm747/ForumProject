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

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var userList = await _userManager.Users.ToListAsync();

            List<UserDTO> result = new List<UserDTO>();

            foreach (var item in userList)
            {
                result.Add(_mapper.Map<User, UserDTO>(item));
            }
            
            return result;
        }

        public async Task<UserDTO> GetCurrentUserAsync(string email)
        {
            if (email == null)
            {
                throw new ArgumentException("There is no current logged in user from that computer.");
            }
            var currentUser = await _userManager.FindByEmailAsync(email);
            var result = _mapper.Map<User, UserDTO>(currentUser);
            result.Roles = await _userManager.GetRolesAsync(currentUser);
            return result;
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

using ForumBLL.DTO;
using ForumDAL.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task DeleteUser(string email);
        Task<User> GetCurrentUserAsync(string email);
        Task<User> GetUserByIdAsync(string id);
        Task<string> GetUserIdAsync(string email);
        Task UpdateUser(UserDTO user);
    }
}

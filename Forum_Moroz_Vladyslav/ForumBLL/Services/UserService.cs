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
    public sealed class UserService: IUserService 
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userMenager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userMenager;
            _roleManager = roleManager;
        }

        public async Task Register(RegisterDTO user)
        {
            var result = await _userManager.CreateAsync(new User
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                UserName = user.Email,
                Email = user.Email,
            }, user.Password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }

        public async Task<User> SignIn(SignInDTO signIn)
        {
            var user = _userManager.Users.FirstOrDefault(login => login.Email == signIn.Email);

            if (user == null)
            {
                throw new System.Exception($"User not found {signIn.Email}.");
            }
                
            return await _userManager.CheckPasswordAsync(user, signIn.Password) ? user : null;
        }

        public async Task AssignUserToRoles(AssignUserToRoleDTO assignUserToRole)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == assignUserToRole.Email);
            var roles = _roleManager.Roles.ToList()
                .Where(r => assignUserToRole.Roles.Contains(r.Name, StringComparer.OrdinalIgnoreCase))
                .Select(r => r.NormalizedName).ToList();

            var result = await _userManager.AddToRolesAsync(user, roles); // THROWS

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }
       
    }

}


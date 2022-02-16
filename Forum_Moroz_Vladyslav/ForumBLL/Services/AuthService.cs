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
    public sealed class AuthService: IAuthService 
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(UserManager<User> userMenager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userMenager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
            else
            {
                var userToCreate = await _userManager.FindByEmailAsync(user.Email);

                var roleResult = await _userManager.AddToRoleAsync(userToCreate, user.Role);
            }
        }

        public async Task<User> SignIn(SignInDTO signIn)
        {
            var user = _userManager.Users.SingleOrDefault(login => login.UserName == signIn.Email);

            if (user == null)
            {
                throw new System.Exception($"User with email: '{signIn.Email}' is not found.");
            }
                
            return await _userManager.CheckPasswordAsync(user, signIn.Password) ? user : throw new ArgumentException("Wrong Password");
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

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }

}


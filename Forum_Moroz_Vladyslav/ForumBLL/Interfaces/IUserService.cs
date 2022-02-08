using ForumBLL.DTO;
using ForumDAL.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Interfaces
{
    public interface IUserService
    {
        Task Register(RegisterDTO model);
        Task<User> SignIn(SignInDTO model);
        Task AssignUserToRoles(AssignUserToRoleDTO assignUserToRole);
       
    }
}

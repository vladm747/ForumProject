using ForumDAL.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Interfaces
{
    public interface IRoleService
    {
        Task CreateRole(string roleName);
        Task<IEnumerable<IdentityRole>> GetRoles();
        Task<IEnumerable<string>> GetRoles(User user);
    }
}

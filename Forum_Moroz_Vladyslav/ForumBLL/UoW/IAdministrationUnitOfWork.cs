using ForumBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumBLL.UoW
{
    public interface IAdministrationUnitOfWork
    {
        IUserService UserService { get; }
        IRoleService RoleService { get; }
        IAuthService AuthService { get; }
    }
}

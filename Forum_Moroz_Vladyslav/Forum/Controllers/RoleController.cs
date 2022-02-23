using Forum.Filters;
using Forum.Helpers;
using Forum_DAL.UoW;
using ForumBLL.DTO;
using ForumBLL.Interfaces;
using ForumBLL.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ForumExceptionFilter]
 
    public class RoleController : ControllerBase
    {
        private readonly IAdministrationUnitOfWork _UoW;
        private readonly JwtSettings _jwtSettings;

        public RoleController(
            IAdministrationUnitOfWork UoW,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _UoW = UoW;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("createRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO model)
        {
            await _UoW.RoleService.CreateRole(model.RoleName);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _UoW.RoleService.GetRoles());
        }

        [HttpPost("assignUserToRole")]
        public async Task<IActionResult> AssignUserToRole(AssignUserToRoleDTO model)
        {
            await _UoW.AuthService.AssignUserToRoles(new AssignUserToRoleDTO
            {
                Email = model.Email,
                Roles = model.Roles
            });

            return Ok();
        }
    }
}

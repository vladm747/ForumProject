using Forum.Helpers;
using ForumBLL.DTO;
using ForumBLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Forum.Filters;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ForumExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userService;
        private readonly IRoleService _roleService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            IAuthService userService, 
            IRoleService roleService, 
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _roleService = roleService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            await _userService.Register(new RegisterDTO
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Role = model.Role
            });

            return Created(string.Empty, string.Empty);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            var user = await _userService.SignIn(new SignInDTO
            {
                Email = model.Email,
                Password = model.Password
            });

            if (user is null) return BadRequest();

            var roles = await _roleService.GetRoles(user);

            return Ok(JwtHelper.GenerateJwt(user, roles, _jwtSettings));
        }

    }
}

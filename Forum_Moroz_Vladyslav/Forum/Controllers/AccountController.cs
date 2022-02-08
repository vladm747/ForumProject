using ForumBLL.DTO;
using ForumBLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;


        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
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

            return Ok();
        }

    }
}

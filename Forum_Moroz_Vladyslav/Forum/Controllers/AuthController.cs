using Forum.Helpers;
using ForumBLL.DTO;
using ForumBLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Forum.Filters;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ForumExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRoleService _roleService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            IAuthService userService, 
            IRoleService roleService, 
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _authService = userService;
            _roleService = roleService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            await _authService.Register(new RegisterDTO
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Role = model.Role
            });

            return Created(string.Empty, string.Empty);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            var user = await _authService.SignIn(new SignInDTO
            {
                Email = model.Email,
                Password = model.Password
            });

            if (user is null) return BadRequest();
            
            var roles = await _roleService.GetRoles(user);
            var token = JwtHelper.GenerateJwt(user, roles, _jwtSettings);
            HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", token,
              new CookieOptions
              {
                  MaxAge = TimeSpan.FromDays(30),
                  SameSite = SameSiteMode.None,
                  Secure = true
              });

            return Ok(token);
        }

        [HttpPost]
        [Route("SignOut")]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await _authService.SignOut();
            
            return Ok();
        }
    }
}

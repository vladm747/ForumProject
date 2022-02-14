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
using ForumBLL.UoW;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ForumExceptionFilter]
    public class AuthController : ControllerBase
    {
        private readonly IAdministrationUnitOfWork _UoW;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            IAdministrationUnitOfWork UoW,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _UoW = UoW;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            await _UoW.AuthService.Register(new RegisterDTO
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
            var user = await _UoW.AuthService.SignIn(new SignInDTO
            {
                Email = model.Email,
                Password = model.Password
            });

            if (user is null) return BadRequest();
            
            var roles = await _UoW.RoleService.GetRoles(user);
            var token = JwtHelper.GenerateJwt(user, roles, _jwtSettings);
            HttpContext.Response.Cookies.Append("JWT Token", token,
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
            await _UoW.AuthService.SignOut();
            
            return Ok();
        }
    }
}

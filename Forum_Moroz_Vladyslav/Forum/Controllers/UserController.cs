using Forum.Filters;
using ForumBLL.DTO;
using ForumBLL.Interfaces;
using ForumBLL.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ForumExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IAdministrationUnitOfWork _UoW;
        public UserController(IAdministrationUnitOfWork UoW)
        {
            _UoW = UoW;
        }


        [HttpPost]
        [Route("DeleteUser/{email}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            await _UoW.UserService.DeleteUser(email);
            return Ok();
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _UoW.UserService.GetAllUsersAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return Ok(await _UoW.UserService.GetUserByIdAsync(id));
        }

        [HttpGet]
        [Route("UserId/{email}")]
        public async Task<IActionResult> GetUserId(string email)
        {
            return Ok(await _UoW.UserService.GetUserIdAsync(email));
        }

        [HttpGet]
        [Route("UserEmail")]
        public IActionResult GetUserMail()
        {
            return Ok(User.FindFirst(ClaimTypes.Name)?.Value);
        }


        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            await _UoW.UserService.UpdateUser(user);
            return Ok();
        }

        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.Name)?.Value;
               
                return Ok(await _UoW.UserService.GetCurrentUserAsync(email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Forum.Filters;
using Forum_DAL.Entities;
using ForumBLL.Interfaces;
using ForumDAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [ForumExceptionFilter]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        // GET: api/<MessageController>
        [HttpGet]
        [AllowAnonymous]
        [Route("getMessagesByTopicId")]
        public IActionResult GetMessagesOfTopic(int topicId)
        {
            return Ok(_messageService.GetMessageListByTopicIdAsync(topicId));
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getMessagesByUserId")]
        public IActionResult GetMessagesOfUser(string userId)
        {
            return Ok(_messageService.GetMessageListByUserIdAsync(userId));
        }

        // GET api/<MessageController>/5
        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            return Ok(await _messageService.GetMessageByIdAsync(id));
        }

        // POST api/<MessageController>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDTO message)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            await _messageService.AddMessageAsync(message, email);
            return Ok(message);
        }

        // PUT api/<MessageController>/5
        [HttpPut]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> UpdateMessage( [FromBody] Message message)
        {
            await _messageService.UpdateMessageAsync(message);
            return Ok();
        }

        // DELETE api/<MessageController>/5
        [HttpDelete]
        [Authorize(Roles = "admin, user")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMessageById(int id)
        {
            await _messageService.DeleteMessageByIdAsync(id);
            return Ok();
        }
        [HttpDelete]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> DeleteMessage(Message message)
        {
            await _messageService.DeleteMessageAsync(message);
            return Ok();
        }
    }
}

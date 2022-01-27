using Forum.Filters;
using Forum_DAL.Entities;
using ForumBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [ForumExceptionFilter]
    [Route("api/[controller]")]
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
        public IActionResult Get([FromQuery] int topicId)
        {
            return Ok(_messageService.GetMessageListByTopicIdAsync(topicId));
        }

        // GET api/<MessageController>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            return Ok(await _messageService.GetMessageByIdAsync(id));
        }

        // POST api/<MessageController>
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            await _messageService.AddMessageAsync(message);
            return Ok(message);
        }

        // PUT api/<MessageController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateMessage( [FromBody] Message message)
        {
            await _messageService.UpdateMessageAsync(message);
            return Ok();
        }

        // DELETE api/<MessageController>/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMessageById(int id)
        {
            await _messageService.DeleteMessageByIdAsync(id);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMessage(Message message)
        {
            await _messageService.DeleteMessageAsync(message);
            return Ok();
        }
    }
}

using Forum.Filters;
using Forum_DAL.Entities;
using ForumBLL.Interfaces;
using ForumDAL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [ForumExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        // GET: api/<TopicController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetTopics()
        {
            return Ok(_topicService.GetAllTopicsAsync());
        }

        // GET api/<TopicController>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTopicById(int id)
        {
            return Ok(await _topicService.GetTopicByIdAsync(id));
        }

        // POST api/<TopicController>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> CreateTopic([FromBody] TopicDTO topic)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            await _topicService.CreateTopicAsync(topic, email);
            return Created("/topic/" + topic.Id, topic);
        }

        // DELETE api/<TopicController>/5
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTopicById(int id)
        {
            await _topicService.DeleteTopicByIdAsync(id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTopic(Topic topic)
        {
            await _topicService.DeleteTopicAsync(topic);
            return Ok();
        }
    }
}

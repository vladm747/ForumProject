using Forum_DAL.Entities;
using ForumBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        // GET: api/<TopicController>
        [HttpGet]
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
        public async Task<IActionResult> CreateTopic([FromBody] Topic topic)
        {
            await _topicService.CreateTopicAsync(topic);
            return Ok();
        }

        // DELETE api/<TopicController>/5
        [HttpDelete]
        [Route("{id}")]
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

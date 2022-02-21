using Forum_DAL.Entities;
using ForumDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Interfaces
{
    public interface ITopicService
    {
        Task CreateTopicAsync(TopicDTO topic, string email);
        IEnumerable<TopicDTO> GetAllTopicsAsync();
        Task<TopicDTO> GetTopicByIdAsync(int id);
        Task DeleteTopicAsync(Topic topic);
        Task DeleteTopicByIdAsync(int id);
    }
}

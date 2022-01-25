using Forum_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Interfaces
{
    public interface IMessageService
    {
        IEnumerable<Message> GetMessageListByTopicIdAsync(int topicId);
        Task AddMessageAsync(Message message);
        Task DeleteMessageAsync(Message message);
        Task UpdateMessageAsync(Message message);
        Task<Message> GetMessageByIdAsync(int id);
        Task DeleteMessageByIdAsync(int id);
    }
}

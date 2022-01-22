using Forum_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Interfaces
{
    public interface IMessageService
    {
        Task AddMessageAsync();
        Task DeleteMessageAsync(Message message);
        Task UpdateMessageAsync(Message message);
        Task<Message> GetMessageByIdAsync(int id);
        Task DeleteMessageById(int id);
    }
}

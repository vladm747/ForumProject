using Forum_DAL.Entities;
using ForumDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Interfaces
{
    public interface IMessageRepository: IRepository<Message>
    {
        ICollection<Message> GetAll();
        ICollection<Message> GetByUserIdAsync(string userId);
        Task UpdateAsync(Message entity);
        Task CreateAsync(Message entity, string email);
    }
}

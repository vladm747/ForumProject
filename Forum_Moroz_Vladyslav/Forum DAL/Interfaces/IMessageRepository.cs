using Forum_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Interfaces
{
    public interface IMessageRepository: IRepository<Message>
    {
        IEnumerable<Message> GetAll();
        Task UpdateAsync(Message entity);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IMessageRepository MessageRepository { get; }
        ITopicRepository TopicRepository { get; }
        Task<int> SaveAsync();
    }
}

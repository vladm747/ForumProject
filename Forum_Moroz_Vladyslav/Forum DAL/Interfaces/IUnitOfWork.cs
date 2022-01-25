using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IMessageRepository Messages{ get; }
        ITopicRepository Topics { get; }
        Task SaveAsync();
    }
}

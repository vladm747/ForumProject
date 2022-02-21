using Forum_DAL.Entities;
using ForumDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Interfaces
{
    public interface ITopicRepository: IRepository<Topic>
    {
        IEnumerable<Topic> GetAllTopics();
        Task CreateAsync(Topic entity, string email);
    }
}

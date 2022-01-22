using Forum_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum_DAL.Interfaces
{
    public interface ITopicRepository: IRepository<Topic>
    {
        IQueryable<Topic> GetAllTopics();
    }
}

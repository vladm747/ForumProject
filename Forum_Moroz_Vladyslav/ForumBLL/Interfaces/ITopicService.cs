﻿using Forum_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Interfaces
{
    public interface ITopicService
    {
        Task AddTopic(Topic topic);
        IQueryable<Topic> GetAllTopicsAsync();
        Task<Topic> GetTopicByIdAsync(int id);
        Task DeleteTopicAsync(Topic topic);
        Task DeleteTopicByIdAsync(int id);
    }
}
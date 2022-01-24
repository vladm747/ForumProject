using Forum_DAL.Entities;
using Forum_DAL.Interfaces;
using Forum_DAL.UoW;
using ForumBLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBLL.Services
{
    public class TopicService : ITopicService
    {
        private IUnitOfWork _database;

        public IUnitOfWork Database
        {
            get
            {
                if (_database == null)
                    _database = new UnitOfWork();
                return _database;
            }
        }
        public TopicService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task CreateTopicAsync(Topic topic)
        {
            if (_database.Topics.GetAllTopics().Contains(topic))
                throw new ArgumentException($"The topic you wanna add already exists in the database!");
            await _database.Topics.CreateAsync(topic);
            await _database.SaveAsync();    
        }

        public IQueryable<Topic> GetAllTopicsAsync()
        {
            return _database.Topics.GetAllTopics();
        }

        public async Task<Topic> GetTopicByIdAsync(int id)
        {
            if (!_database.Topics.GetAllTopics().Select(x => x.Id).Contains(id))
                throw new ArgumentException($"There is no topic with id: {id}");
            return await _database.Topics.GetByIdAsync(id);
        }

        public async Task DeleteTopicAsync(Topic topic)
        {
            if (!_database.Topics.GetAllTopics().Contains(topic))
                throw new ArgumentException($"There is no topic: {topic.Name}");
            await _database.Topics.DeleteAsync(topic);
            await _database.SaveAsync();
        }

        public async Task DeleteTopicByIdAsync(int id)
        {
            if (!_database.Topics.GetAllTopics().Select(x => x.Id).Contains(id))
                throw new ArgumentException($"There is no topic with id: {id}");
            await _database.Topics.DeleteByIdAsync(id);
            await _database.SaveAsync();
        }
    }
}

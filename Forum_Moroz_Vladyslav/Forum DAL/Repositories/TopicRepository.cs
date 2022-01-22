using Forum_DAL.Context;
using Forum_DAL.Entities;
using Forum_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ForumContext _topicContext;

        public TopicRepository(ForumContext topicContext)
        {
            _topicContext = topicContext;
        }
        public async Task AddAsync(Topic entity)
        {
            await _topicContext.AddAsync(entity);
            await _topicContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Topic entity)
        {
            var item = await GetByIdAsync(entity.Id);
            if (item != null)
            {
                _topicContext.Remove(item);
            }
            await _topicContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _topicContext.Topics.Remove(item);
            }
            await _topicContext.SaveChangesAsync(); 
        }

        public IQueryable<Topic> GetAllTopics()
        {
            return _topicContext.Topics;
        }

        public async Task<Topic> GetByIdAsync(int id)
        {
            var item = await _topicContext.Topics.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                throw new ArgumentException($"There is no topic with id: {id}");
            }
            return item;
        }
    }
}

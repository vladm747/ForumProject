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
    public class MessageRepository : IMessageRepository
    {
        private readonly ForumContext _messageContext;

        public MessageRepository(ForumContext messageContext)
        {
            _messageContext = messageContext;
        }

        public async Task CreateAsync(Message entity)
        {
            await _messageContext.Messages.AddAsync(entity);
            await _messageContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message entity)
        {
            var item = await _messageContext.Messages.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (item != null)
            {
                _messageContext.Messages.Remove(item);
            }

            await _messageContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _messageContext.Messages.Remove(item);
            }
            await _messageContext.SaveChangesAsync();
        }

        public IQueryable<Message> GetAll()
        {
            return _messageContext.Messages;//include hz
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            var message = await _messageContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (message == null)
                throw new ArgumentException($"There is no message with id: {id}");
            return message;
        }

        public async Task UpdateAsync(Message entity)
        {
            var item = _messageContext.Messages.FirstOrDefault(x => x.Id == entity.Id);
            if (item != null)
            {
                item.Content = entity.Content;
                item.Title = entity.Title;
            }
            _messageContext.Entry(item).State = EntityState.Modified;
            await _messageContext.SaveChangesAsync();  
        }
    }
}

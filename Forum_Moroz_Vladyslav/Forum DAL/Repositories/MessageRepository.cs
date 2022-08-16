using Forum_DAL.Context;
using Forum_DAL.Entities;
using Forum_DAL.Interfaces;
using ForumDAL.DTO;
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

        public async Task CreateAsync(Message entity, string email)
        {
            var user = await _messageContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                entity.UserId = user.Id;
            }

            await _messageContext.AddAsync(new Message
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                CreationDateTime = DateTime.Now,
                UserId = entity.UserId,
                TopicId = entity.TopicId
            });
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

        public ICollection<Message> GetAll()
        {
            return _messageContext.Messages
                .ToList();//include hz
        }
        public ICollection<Message> GetByUserIdAsync(string userId)
        {
            var result = _messageContext.Messages.Select(x=>x).Where(x=>x.UserId == userId).ToList();

            return result;
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

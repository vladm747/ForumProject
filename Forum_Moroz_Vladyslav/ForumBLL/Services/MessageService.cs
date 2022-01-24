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
    public class MessageService : IMessageService
    {
        private  IUnitOfWork _database;

        public IUnitOfWork Database
        {
            get
            {
                if (_database == null)
                    _database = new UnitOfWork();
                return _database;
            }
        }

        public MessageService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task AddMessageAsync(Message message)
        {
            if (_database.Messages.GetAll().Contains(message))
            {
                throw new ArgumentException($"Message '{message.Title}' already exists");
            }
            await _database.Messages.CreateAsync(message);
            await _database.SaveAsync();
        }

        public async Task DeleteMessageAsync(Message message)
        {
            if(message == null)
                throw new ArgumentNullException(nameof(message) + "MessageService");
            await _database.Messages.DeleteAsync(message);
            await _database.SaveAsync();
        }

        public async Task DeleteMessageByIdAsync(int id)
        {
            var element = await _database.Messages.GetByIdAsync(id);

            if (!_database.Messages.GetAll().Contains(element))
                throw new ArgumentException($"There is no message with id: {id}");

            await _database.Messages.DeleteAsync(element);
            await _database.SaveAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int id)
        {
            var item = await _database.Messages.GetByIdAsync(id);
            if(item == null)
            {
                throw new ArgumentException($"There is no message with id: {id}");
            }
            return item;
        }

        public async Task UpdateMessageAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message) + "MessageService");
            if (!_database.Messages.GetAll().Contains(message))
                throw new ArgumentException($"There is no message with id: {message.Id}");

            await _database.Messages.UpdateAsync(message);

            await _database.SaveAsync();    
        }

        public IQueryable<Message> GetMessageListByTopicIdAsync(int topicId)
        {
            return _database.Messages.GetAll().Where(x=>x.TopicId==topicId);
        }
    }
}

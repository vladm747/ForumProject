using AutoMapper;
using Forum_DAL.Entities;
using Forum_DAL.Interfaces;
using Forum_DAL.UoW;
using ForumBLL.Interfaces;
using ForumDAL.DTO;
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
        private readonly IMapper _mapper;
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
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<MessageDTO, Message>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public async Task AddMessageAsync(MessageDTO message, string email)
        {
            if (_database.Messages.GetAll().FirstOrDefault(x => x.Id == message.Id) != null)
            {
                throw new ArgumentException($"Message '{message.Title}' already exists");
            }
            var messageBase = _mapper.Map<MessageDTO, Message>(message);

            await _database.Messages.CreateAsync(messageBase, email);
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
           
        public ICollection<MessageDTO> GetMessageListByUserIdAsync(string userId)
        {
            var messageList = _database.Messages.GetByUserIdAsync(userId);
            var result = new List<MessageDTO>();

            foreach (var item in messageList)
            {
                result.Add(_mapper.Map<Message, MessageDTO>(item));
            }
            
            return result;
        }

        public async Task<MessageDTO> GetMessageByIdAsync(int id)
        {
            var item = await _database.Messages.GetByIdAsync(id);
            if(item == null)
            {
                throw new ArgumentException($"There is no message with id: {id}");
            }
            return _mapper.Map<Message, MessageDTO> (item);
        }

        public async Task UpdateMessageAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message) + "MessageService");
            if (_database.Messages.GetAll().FirstOrDefault(x => x.Id == message.Id) == null)
                throw new ArgumentException($"There is no message with id: {message.Id}");

            await _database.Messages.UpdateAsync(message);

            await _database.SaveAsync();    
        }

        public ICollection<MessageDTO> GetMessageListByTopicIdAsync(int topicId)
        {
            var messages = _database.Messages.GetAll().Where(x => x.TopicId == topicId).OrderByDescending(t => t.CreationDateTime);

            List<MessageDTO> result = new List<MessageDTO>();

            foreach (var item in messages)
            {
                result.Add(_mapper.Map<Message, MessageDTO>(item));
            }
            return result;
        }
    }
}

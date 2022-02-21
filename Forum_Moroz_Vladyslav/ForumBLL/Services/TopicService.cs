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
    public class TopicService : ITopicService
    {
        private IUnitOfWork _database;
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
        public TopicService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<TopicDTO, Topic>().ReverseMap();
                c.CreateMap<MessageDTO, Message>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public async Task CreateTopicAsync(TopicDTO topic, string email)
        {
            if (_database.Topics.GetAllTopics().FirstOrDefault(x => x.Id == topic.Id) != null)
                throw new ArgumentException($"The topic you wanna add already exists in the database!");
            var topicBase = _mapper.Map<TopicDTO, Topic>(topic);
            await _database.Topics.CreateAsync(topicBase, email);
            await _database.SaveAsync();    
        }

        public IEnumerable<TopicDTO> GetAllTopicsAsync()
        {

            var topics = _database.Topics.GetAllTopics();

            List<TopicDTO> result = new List<TopicDTO>();

            foreach (var item in topics)
            {
                result.Add(_mapper.Map<Topic, TopicDTO>(item));
            }
            return result;
        }

        public async Task<TopicDTO> GetTopicByIdAsync(int id)
        {
            if (!_database.Topics.GetAllTopics().Select(x => x.Id).Contains(id))
                throw new ArgumentException($"There is no topic with id: {id}");
            var topicToFind = await _database.Topics.GetByIdAsync(id);

            return _mapper.Map<Topic, TopicDTO>(topicToFind);
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

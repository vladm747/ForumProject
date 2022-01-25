using Forum_DAL.Context;
using Forum_DAL.Interfaces;
using Forum_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.UoW
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ForumContext _db = new ForumContext();

        private MessageRepository _messageRepository;
        private TopicRepository _topicRepository;

        public IMessageRepository Messages { 
            get 
            {
                if (_messageRepository == null)
                    _messageRepository = new MessageRepository(_db); 
                return _messageRepository;
            } 
        }
        public ITopicRepository Topics
        {
            get
            {
                if (_topicRepository == null) 
                    _topicRepository = new TopicRepository(_db);
                return _topicRepository;
            }
        }
        public int Save()
        {
            return _db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

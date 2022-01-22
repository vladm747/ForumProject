using Forum_DAL.Context;
using Forum_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.UoW
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ForumContext _db;

        public UnitOfWork(ForumContext db)
        {
            _db = db;
        }

        private IMessageRepository _messageRepository;
        private ITopicRepository _topicRepository;

        public IMessageRepository MessageRepository { 
            get 
            {
                if (_messageRepository == null) { throw new NullReferenceException("UoW null reference from messageRepository"); }
                return _messageRepository;
            } 
        }
        public ITopicRepository TopicRepository
        {
            get
            {
                if (_topicRepository == null) { throw new NullReferenceException("UoW null reference from topicRepository"); }
                return _topicRepository;
            }
        }
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}

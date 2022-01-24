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
        private readonly ForumContext _db = new ForumContext();

        private IMessageRepository _messageRepository;
        private ITopicRepository _topicRepository;

        public IMessageRepository Messages { 
            get 
            {
                if (_messageRepository == null) { throw new NullReferenceException("UoW null reference from messageRepository"); }
                return _messageRepository;
            } 
        }
        public ITopicRepository Topics
        {
            get
            {
                if (_topicRepository == null) { throw new NullReferenceException("UoW null reference from topicRepository"); }
                return _topicRepository;
            }
        }
        public int Save()
        {
            return _db.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}

using Forum_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ForumDAL.Entities.Auth
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Topic> Topics { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}

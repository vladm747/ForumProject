using Forum_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum_DAL.Context
{
    public class ForumContext: DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public ForumContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ForumDB;Trusted_Connection=True;");
        }

        public DbSet<Message> Messages;
        public DbSet<Topic> Topics;
    }
}

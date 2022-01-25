using Forum_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum_DAL.Context
{
    public class ForumContext: DbContext
    {

        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public ForumContext(DbContextOptions<ForumContext> options)
            : base(options)
        {
            /*Database.EnsureDeleted();*/

            Database.EnsureCreated();
        }
        public ForumContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ForumDB;Trusted_Connection=True;");
        }

    }
}

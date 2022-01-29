using Forum_DAL.Entities;
using ForumDAL.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum_DAL.Context
{
    public class ForumContext: IdentityDbContext<User>
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new[]
            {
                 new IdentityRole("user")
                {
                    NormalizedName = "USER"
                },
                new IdentityRole("admin")
                {
                    NormalizedName = "ADMIN"
                }
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ForumDB;Trusted_Connection=True;");
        }

    }
}

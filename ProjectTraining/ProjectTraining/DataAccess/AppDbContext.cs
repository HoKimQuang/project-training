using System;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;

namespace ProjectTraining.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                UserName = "admin",
                Password = "12345",
                CreateDate = DateTime.Now,
                Role = "admin",
                ExpireDate = DateTime.MaxValue
            });
            for (int i = 1; i < 25; i++)
            {
                modelBuilder.Entity<User>().HasData(new User
                {
                    Id = i+1,
                    UserName = "admin"+i,
                    Password = "12345",
                    CreateDate = DateTime.Now,
                    Role = "User",
                    ExpireDate = DateTime.Now
                    
                });
            }
        }
    }
}
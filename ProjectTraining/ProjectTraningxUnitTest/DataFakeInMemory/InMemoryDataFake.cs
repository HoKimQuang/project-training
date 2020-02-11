using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.DataAccess;
using ProjectTraining.Models;

namespace ProjectTraningxUnitTest.DataFakeInMemory
{
    public class InMemoryDataFake
    {
        public InMemoryDataFake()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            ApplicationDbContext = new AppDbContext(options);

            var users = new List<User>
            {
                new User { Id= 1, UserName = "test 1", Password = "123" , Role = "admin", CreateDate = DateTime.Now, ExpireDate = DateTime.Now},
                new User { Id= 2, UserName = "test 2", Password = "123" , Role = "admin", CreateDate = DateTime.Now, ExpireDate = DateTime.Now},
                new User { Id= 3, UserName = "test 3", Password = "123" , Role = "User", CreateDate = DateTime.Now, ExpireDate = DateTime.Now},
                new User { Id= 4, UserName = "test 4", Password = "123" , Role = "User", CreateDate = DateTime.Now, ExpireDate = DateTime.Now}
               
            };

            ApplicationDbContext.AddRange(users);
            ApplicationDbContext.SaveChanges();
        }
        public AppDbContext ApplicationDbContext { get;  set; }
    }
}
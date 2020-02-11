using System;
using System.Linq;
using ProjectTraining.Models;
using ProjectTraining.Repositories;
using ProjectTraningxUnitTest.DataFakeInMemory;
using Xunit;

namespace ProjectTraningxUnitTest.RepositoryTests
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class UserRepositoryTest : IClassFixture<InMemoryDataFake>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IGenericRepository<User> _useRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inMemoryDataFake"></param>
        public UserRepositoryTest(InMemoryDataFake inMemoryDataFake)
        {
            _useRepository = new GenericRepository<User>(inMemoryDataFake.ApplicationDbContext);
        }
        
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserReposioryTest_GetAll_Returns()
        {
            var allUsers = _useRepository.GetAll();
            Assert.Equal(4, allUsers.Count());
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserRepositoryTest_GetById_ReturnsUserId()
        {
            const int userId = 4;
            var user = _useRepository.GetById(userId);
            Assert.Equal(userId, user.Id);
        }
        
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserRepositoryTest_CreateUser_ReturnsUserId()
        {
            var user = new User
            {
                Id = 5,
                UserName = "test5",
                Password = "123",
                Role = "admin",
                CreateDate = DateTime.Now,
                ExpireDate = DateTime.MaxValue
            };

            _useRepository.Add(user);

            var getUser = _useRepository.GetById(user.Id);
            Assert.Equal(user, getUser);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserRepositoryTest_UpdateUser_Returns()
        {
            var user = new User
            {
                Id = 5,
                UserName = "test6",
                Password = "123",
                Role = "User",
                CreateDate = DateTime.Now,
                ExpireDate = DateTime.MaxValue
            };
            
            _useRepository.Update(user);
            var getUser = _useRepository.GetById(user.Id);
            Assert.Equal(user , getUser);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserRepository_DeleteUser_Returns()
        {
            const int userId = 4;
            _useRepository.Delete(userId);
            var result = _useRepository.GetAll();
            Assert.Equal(3, result.Count());
        }
    }
}
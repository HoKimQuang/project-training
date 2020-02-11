using System;
using System.Linq;
using ProjectTraining.Models;
using ProjectTraining.Repositories;
using ProjectTraining.Services;
using ProjectTraningxUnitTest.DataFakeInMemory;
using Xunit;

namespace ProjectTraningxUnitTest.ServiceTests
{
    public class UserServiceTest : IClassFixture<InMemoryDataFake>
    {
        private readonly UserService _userService;

        public UserServiceTest(InMemoryDataFake inMemoryDataFake)
        {
            IUnitOfWork unitOfWork = new UnitOfwork(inMemoryDataFake.ApplicationDbContext);
            AutoMapperConfig.Initialize();
            var mapper = AutoMapperConfig.GetMapper();
            
            _userService = new UserService(unitOfWork, mapper);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserServiceTest_GetAllUser_Returns()
        {
            var users = _userService.GetUserViewModels();
            Assert.Equal(4, users.Count());
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserServiceTest_GetById_Returns()
        {
            const int userId = 4;
            var user = _userService.GetById(userId);
            Assert.Equal(userId, user.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserServiceTest_CreateUser_Returns()
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

            _userService.CreateUser(user);
            var getUser = _userService.GetById(user.Id);
            Assert.Equal(user.Id, getUser.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserServiceTest_DeleteUser_Returns()
        {
            const int userId = 4;
            _userService.Delete(userId);
            var result = _userService.GetUserViewModels();
            Assert.Equal(3, result.Count());
        }
        
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserServiceTest_UpdateExprireDate_Returns()
        {
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UserServiceTest_Login_Returns()
        {
            
        }
    }
}
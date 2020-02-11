using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectTraining.Controllers;
using ProjectTraining.Models;
using ProjectTraining.Services;
using ProjectTraining.ViewModels;
using Xunit;

namespace ProjectTraningxUnitTest.ControllerTests
{
    /// <summary>
    /// This controller test user controller in project: ProjectTraining
    /// </summary>
    public class UserControllerTest
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly List<UserViewModel> _listUserViewModelMock;
        private readonly UserViewModel _userViewModelMock;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
            
            //ARRANGE
            _listUserViewModelMock = new List<UserViewModel>
            {
                new UserViewModel
                {
                    Id = 1,
                    UserName = "test1",
                    Password = "123",
                    Role = "admin"
                }
                ,new UserViewModel
                {
                    Id = 2,
                    UserName = "test2",
                    Password = "123",
                    Role = "admin"
                },
                new UserViewModel
                {
                    Id = 3,
                    UserName = "test3",
                    Password = "123",
                    Role = "admin"
                    
                }
            };
            _userViewModelMock = new UserViewModel
            {
                Id = 1,
                UserName = "test1",
                Password = "123",
                Role = "admin"
            };
        }

        /// <summary>
        /// Function test Indext returns view with list user
        /// </summary>
        [Fact]
        public void IndextTest_ReturnsViewWithUserList()
        {
            _userServiceMock.Setup(usm => usm.GetUserViewModels()).Returns(_listUserViewModelMock);
            
            //ACT
            var result = _userController.JqueryDataTableFrontEnd();
            
            //ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(_listUserViewModelMock,model);
        }

        /// <summary>
        /// Functions test details returns not found when no id provided
        /// </summary>
        [Fact]
        public void DetailsTest_ReturnsNotFound_whenNoIdProvided()
        {
            //ACt
            var result = _userController.Details(null);
            
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Function test details return not found when user does not exist
        /// </summary>
        [Fact]
        public void DetailsTest_ReturnNotFound_WhenUserDoesNotExist()
        {
           //Arrange
            const int mockId = 5;
            _userServiceMock.Setup(usm => usm.GetById(mockId)).Returns(_listUserViewModelMock.Find(m => false));

            //Act
            var result = _userController.Details(mockId);

            //Assert
           Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Function test details ruturn view details when user exist
        /// </summary>
        [Fact]
        public void DetailsTest_ReturnsDetails_WhenUserExist()
        {
            //Arrange
            const int mockId = 1;
            _userServiceMock.Setup(usm => usm.GetById(mockId)).Returns(_userViewModelMock);
            
            //Act
            var result = _userController.Details(mockId);
            
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(_userViewModelMock, viewResult.ViewData.Model);
        }
        
        /// <summary>
        /// Function test [http get] Create returns view
        /// </summary>
        [Fact]
        public void CreateTest_Get_ReturnsView()
        {
            // Act
            var result = _userController.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// Function test [http post] Create returns Redirects to Indext view
        /// </summary>
        [Fact]
        public void CreateTest_Post_ReturnsCreateView_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockUser = new User
            {
                Id = 1,
                UserName = null,
                Password = "123",
                Role = "admin"
            };
            _userController.ModelState.AddModelError("UserName", "This field is required");

            // Act
            
            var result = _userController.Create(mockUser);
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(mockUser, viewResult.ViewData.Model);
        }
        
        /// <summary>
        /// Function test [http post] Create returns Redirects to Indext view
        /// </summary>
        [Fact]
        public void CreateTest_Post_AddsUser_AndRedirectsToIndex()
        {
            
        }
        
   
        [Fact]
        public void DeleteConfirmedTest_RemovesArticleFromRepository_AndRedirectsToIndex()
        {
            // Arrange
            const int mockId = 2;
            var userDelete = _listUserViewModelMock.Find(m => m.Id == mockId);
            _userServiceMock.Setup(usm => usm.GetById(mockId)).Returns(userDelete);

            // Act
            var result = _userController.DeleteConfirmed(mockId);

            // Assert
            _userServiceMock.Verify(repo => repo.Delete(mockId));
           Assert.IsType<RedirectToActionResult>(result);
            
        }
        
        
    }
}
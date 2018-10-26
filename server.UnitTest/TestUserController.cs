using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using server.Controllers;
using server.services;
using server.services.DTOs;

namespace server.UnitTest
{
    [TestClass]
    public class TestUserController
    {
        public TestUserController()
        {
            MappingConfigurations.Initialize();
        }

        [TestMethod]
        public async Task GetByIdReturnsOkResponse()
        {
            // Arrange
            var mockRepository = new Mock<IUserService>();
            mockRepository.Setup(s => s.GetUser(1))
                          .Returns(Task.FromResult(new UserDTO { Id = 1 }));
            var controller = new UserController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.GetUser(1);
            var createdResult = actionResult as OkNegotiatedContentResult<UserDTO>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.AreEqual(typeof(UserDTO), createdResult.Content.GetType());
            Assert.AreEqual(1, createdResult.Content.Id);
        }

        [TestMethod]
        public async Task GetByIdReturnsOkNoResponseBody()
        {
            // Arrange
            var mockRepository = new Mock<IUserService>();
            var controller = new UserController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.GetUser(10);
            var createdResult = actionResult as OkNegotiatedContentResult<UserDTO>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(createdResult.Content);
        }

        [TestMethod]
        public async Task GetByEmailReturnsOkResponse()
        {
            // Arrange
            var mockRepository = new Mock<IUserService>();
            mockRepository.Setup(s => s.GetUser("medina.malisevic@gmail.com"))
                          .Returns(Task.FromResult(new UserDTO { Id = 1, Email = "medina.malisevic@gmail.com" }));
            var controller = new UserController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.GetUser("medina.malisevic@gmail.com");
            var createdResult = actionResult as OkNegotiatedContentResult<UserDTO>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.AreEqual(typeof(UserDTO), createdResult.Content.GetType());
            Assert.AreEqual("medina.malisevic@gmail.com", createdResult.Content.Email);
        }

        [TestMethod]
        public async Task GetByEmailReturnsOkNoResponseBody()
        {
            // Arrange
            var mockRepository = new Mock<IUserService>();
            var controller = new UserController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.GetUser("nadira.malisevic@gmail.com");
            var createdResult = actionResult as OkNegotiatedContentResult<UserDTO>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(createdResult.Content);
        }
    }
}

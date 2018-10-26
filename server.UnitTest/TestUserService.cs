using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using server.data;
using server.data.Tables;
using server.services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace server.UnitTest
{
    [TestClass]
    public class TestUserService
    {
        IQueryable<Users> users = new List<Users>
        {
            new Users
            {
                Id = 5,
                Email = "nadira.malisevic@gmail.com",
                FirstName = "Nadira",
                LastName = "Mališević",
                Password = "nekiPassword",
                IsActive = true,
                CreatedOn = DateTime.Now
            }
        }.AsQueryable();

        public TestUserService()
        {
            MappingConfigurations.Initialize();
        }

        [TestMethod]
        public async Task GetUserByIdReturnsUser()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Users>>();
            mockSet.Setup(s => s.FindAsync(It.IsAny<long>()))
                   .Returns(Task.FromResult(users.First()));
            var mockContext = new Mock<IGlobomanticsContext>();
            mockContext.Setup(s => s.Users)
                       .Returns(mockSet.Object);

            // Act
            var service = new UserService(mockContext.Object);
            var returnedUser = await service.GetUser(5);

            // Assert
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(returnedUser.Id, users.First().Id);
            Assert.AreEqual(returnedUser.FirstName, users.First().FirstName);
        }

        [TestMethod]
        public async Task GetUserByIdReturnsNoUser()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Users>>();
            var mockContext = new Mock<IGlobomanticsContext>();
            mockContext.Setup(s => s.Users)
                       .Returns(mockSet.Object);

            // Act
            var service = new UserService(mockContext.Object);
            var returnedUser = await service.GetUser(1);

            // Assert
            Assert.IsNull(returnedUser);
        }

        [TestMethod]
        public async Task GetUserByEmailReturnsUser()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Users>>();
            mockSet.As<IDbAsyncEnumerable<Users>>()
                   .Setup(s => s.GetAsyncEnumerator())
                   .Returns(new TestDbAsyncEnumerator<Users>(users.GetEnumerator()));
            mockSet.As<IQueryable<Users>>()
                   .Setup(s => s.Provider)
                   .Returns(new TestDbAsyncQueryProvider<Users>(users.Provider));
            mockSet.As<IQueryable<Users>>()
                   .Setup(s => s.Expression)
                   .Returns(users.Expression);
            mockSet.As<IQueryable<Users>>()
                   .Setup(s => s.ElementType)
                   .Returns(users.ElementType);
            mockSet.As<IQueryable<Users>>()
                   .Setup(s => s.GetEnumerator())
                   .Returns(users.GetEnumerator());
            var mockContext = new Mock<IGlobomanticsContext>();
            mockContext.Setup(s => s.Users)
                       .Returns(mockSet.Object);

            // Act
            var service = new UserService(mockContext.Object);
            var returnedUser = await service.GetUser("nadira.malisevic@gmail.com");

            // Assert
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(returnedUser.Id, users.First().Id);
            Assert.AreEqual(returnedUser.Email, users.First().Email);
        }

        [TestMethod]
        public async Task GetUserByEmailReturnsNoUser()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Users>>();
            mockSet.As<IDbAsyncEnumerable<Users>>()
                   .Setup(s => s.GetAsyncEnumerator())
                   .Returns(new TestDbAsyncEnumerator<Users>(users.GetEnumerator()));
            mockSet.As<IQueryable<Users>>()
                   .Setup(s => s.Provider)
                   .Returns(new TestDbAsyncQueryProvider<Users>(users.Provider));
            mockSet.As<IQueryable<Users>>()
                   .Setup(s => s.Expression)
                   .Returns(users.Expression);
            mockSet.As<IQueryable<Users>>()
                   .Setup(s => s.ElementType)
                   .Returns(users.ElementType);
            mockSet.As<IQueryable<Users>>()
                   .Setup(s => s.GetEnumerator())
                   .Returns(users.GetEnumerator());
            var mockContext = new Mock<IGlobomanticsContext>();
            mockContext.Setup(s => s.Users)
                       .Returns(mockSet.Object);

            // Act
            var service = new UserService(mockContext.Object);
            var returnedUser = await service.GetUser("medina.malisevic@gmail.com");

            // Assert
            Assert.IsNull(returnedUser);
        }
    }
}

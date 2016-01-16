using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.Services;

namespace UnitTest
{
    [TestClass]
    public class UserServiceTests
    {

        private readonly UserService userService;

        public UserServiceTests()
        {
            userService = new UserService(User.ImagesPath);
        }

        [TestMethod]
        public void FindWhere_WhenUserExist()
        {
            // expected list
            List<User> expectedList = new List<User>();
            expectedList.Add(new User()
            {
                UserId = 1,
                Name = "trajce",
                LastName = "Meshkov",
                Email = "jovanmeshkov@outlook.com",
                Password = "bit01",
                Username = "dawd",
                Phone = "078218192",
                PhotoFilename = null,
                AddressId = null
            });
            
            var recievedList = userService.FindWhere(u => u.Name == "trajce");

            CollectionAssert.AreEqual(expectedList, recievedList);
        }

        [TestMethod]
        public void Add_AddUserThatExist()
        {
            var userStatus = userService.Add(userService.FindWhere(u => u.Name == "Jovan")[0]);

            Assert.Equals(userStatus, UserStatus.Added);
        }

        [TestMethod]
        public void Add_AddUserThatDoesntExist()
        {

            var userStatus = userService.Add(new User()
            {
                Name = "Jovan",
                LastName = "Meshkov",
                Email = "jovanmeshkov1@outlook.com",
                Password = "dawdwad",
                Username = "bit011",
                Phone = "1",
                PhotoFilename = null,
                AddressId = null
            });

            Assert.Equals(userStatus, UserStatus.Added);
        }
    }
}

using System;
using HackerNewsDataAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNewsDataAPI.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        public UserControllerTests()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestMethod]
        public void UserGetAsyncTest()
        {
            var controller = new UserController();
            var data = controller.GetUser("thefox").Result;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.ID.Equals("thefox", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
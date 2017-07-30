using System.Collections.Generic;
using HackerNewsDataAPI.Controllers;
using HackerNewsDataAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNewsDataAPI.Tests.Controllers
{
    [TestClass()]
    public class HackerNewsControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var controller = new HackerNewsController();
            var data = controller.Get();

            Assert.IsNotNull(data);

            var items = new List<int>(data);

            Assert.AreNotEqual(items.Count, 0);
        }

        [TestMethod()]
        public void GetTestAsync()
        {
            var controller = new HackerNewsController();
            var dataTask = controller.GetAsync();
            var data = dataTask.Result;

            Assert.IsNotNull(data);

            var items = new List<int>(data);

            Assert.AreNotEqual(items.Count, 0);
        }
    }
}
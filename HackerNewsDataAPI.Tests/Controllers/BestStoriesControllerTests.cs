using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNewsDataAPI.Controllers;
using HackerNewsDataAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNewsDataAPI.Tests.Controllers
{
    [TestClass]
    public class BestStoriesControllerTests
    {
        public BestStoriesControllerTests()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestMethod]
        public void BestStoriesGetAsyncTest()
        {
            var controller = new BestStoriesController();
            var data = controller.GetBestStoriesAsync().Result;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count() != 0);
        }

        [TestMethod]
        public void BestStoriesGetAsyncAllTest()
        {
            var controller = new BestStoriesController();
            var controller2 = new ItemController();
            var data = controller.GetBestStoriesAsync().Result;

            var result = Task.WhenAll(data.Select(id => controller2.GetItemAsync(id)).ToArray()).Result;
        }
    }
}
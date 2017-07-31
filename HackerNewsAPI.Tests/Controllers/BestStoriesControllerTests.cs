using System.Linq;
using HackerNewsAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNewsAPI.Tests.Controllers
{
    [TestClass()]
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
        public void GetBestStoryInfoAsync()
        {
            var controller = new BestStoriesController();
            var data = controller.GetBestStoryInfoAsync(89953).Result;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Id == 89953);
        }

        [TestMethod]
        public void GetAllBestStoryInfoAsyncTest()
        {
            var controller = new BestStoriesController();
            var data = controller.GetAllBestStoriesInfoAsync().Result;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any() );
        }
    }
}
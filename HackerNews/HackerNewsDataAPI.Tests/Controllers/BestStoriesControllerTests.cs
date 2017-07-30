using System.Linq;
using HackerNewsDataAPI.Controllers;
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
    }
}
using HackerNewsDataAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNewsDataAPI.Tests.Controllers
{
    [TestClass]
    public class ItemControllerTests
    {
        public ItemControllerTests()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestMethod]
        public void ItemGetAsyncTest()
        {
            var controller = new ItemController();
            var data = controller.GetItem(8863).Result;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.ID == 8863);
        }
    }
}
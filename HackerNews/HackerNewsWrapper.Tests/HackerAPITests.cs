using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackerNewsWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsWrapper.Tests
{
    [TestClass]
    public class HackerAPITests
    {
        public HackerAPITests()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestMethod]
        public void HackerAPIBaseURLTest()
        {
            HackerAPI.HackerAPIBaseURL = "x";
            Assert.AreEqual(HackerAPI.HackerAPIBaseURL, "x");
            HackerAPI.HackerAPIBaseURL = null;
        }

        [TestMethod]
        public void GetItemTest()
        {
            var m = HackerAPI.GetItem(8863);
            var result = m.Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ID  == 8863);
        }

        [TestMethod]
        public void GetUserTest()
        {
            var m = HackerAPI.GetUser("thefox");
            var result = m.Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ID == "thefox");
        }

        [TestMethod]
        public void GetBestStories()
        {
            var m = HackerAPI.GetBestStories();
            var result = m.Result;

            Assert.IsNotNull(result);
        }
    }
}
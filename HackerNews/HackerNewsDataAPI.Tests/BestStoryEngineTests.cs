using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackerNewsDataAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsDataAPI.Tests
{
    [TestClass()]
    public class BestStoryEngineTests
    {
        [TestMethod()]
        public void GetBestStoriesTest()
        {
            var engine = new BestStoryEngine();

            // Get the Best Story Info
            var bestStories = engine.GetBestStories().Result;

            Assert.IsNotNull(bestStories);

            var storyList = new List<int>(bestStories);
            Assert.IsTrue(storyList.Count > 0);
        }

        [TestMethod()]
        public void GetBestStoryInfo()
        {
            var engine = new BestStoryEngine();

            // Get the Best Story Info
            var bestStories = engine.GetBestStories().Result;
            Assert.IsNotNull(bestStories);
            var storyList = new List<int>(bestStories);
            Assert.IsTrue(storyList.Count > 0);

            var data = engine.GetBestStoryInfo(storyList[0]).Result;

            Assert.IsNotNull(data);
            Assert.IsTrue(data.ID == storyList[0]);
        }
    }
}
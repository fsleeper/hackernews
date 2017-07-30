using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HackerNewsDataAPI.Models;

namespace HackerNewsDataAPI
{
    public class BestStoryEngine
    {
        public async Task<IEnumerable<int>> GetBestStories()
        {
            HackerNewsWrapper.HackerAPI.HackerAPIBaseURL = ConfigurationManager.AppSettings["HackerNewBaseURL"];

            var storyIds = await HackerNewsWrapper.HackerAPI.GetBestStories();

            return storyIds;
        }

        public async Task<BestStoryInfo> GetBestStoryInfo(int id)
        {
            BestStoryInfo bestStoryInfo = null;

            HackerNewsWrapper.HackerAPI.HackerAPIBaseURL = ConfigurationManager.AppSettings["HackerNewBaseURL"];

            var storyInfo = await HackerNewsWrapper.HackerAPI.GetItem(id);
            if (storyInfo != null)
            {
                // In case we need additional user info
                // var userInfo = await HackerNewsWrapper.HackerAPI.GetUser(storyInfo.By);

                bestStoryInfo = new BestStoryInfo
                {
                    ID = storyInfo.ID,
                    By = storyInfo.By,
                    Title = storyInfo.Title
                };
            }

            return bestStoryInfo;
        }
    }
}
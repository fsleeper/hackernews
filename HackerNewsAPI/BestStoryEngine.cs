using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using HackerNewsAPI.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HackerNewsDataAPI
{
    public class BestStoryEngine
    {
        public static ConnectionMultiplexer CacheConnection;
        private static readonly ILog Log = LogManager.GetLogger<BestStoryEngine>();
        private static string HackerNewsDataAPI => ConfigurationManager.AppSettings["HackerNewsDataAPI"];

        static BestStoryEngine()
        {
            var cacheConnectionString = ConfigurationManager.AppSettings["CacheConnectionString"];
            CacheConnection = ConnectionMultiplexer.Connect(cacheConnectionString);
        }

        public static async Task<BestStoryInfo> GetBestStoryAsync(int id)
        {
            var cacheDB = CacheConnection.GetDatabase();

            var keyExists = cacheDB.KeyExists(id.ToString());
            if (keyExists)
            {
                var itemFromCache = cacheDB.StringGet(id.ToString());
                var result = JsonConvert.DeserializeObject<BestStoryInfo>(itemFromCache);
                return result;
            }                

            var dataApi = new HackerNewsAPI.HackerNewsDataAPI(new Uri(HackerNewsDataAPI));
            var item = await dataApi.GetItemAsyncWithHttpMessagesAsync(id);

            var bestStoryInfo = new BestStoryInfo
            {
                ID = item.Body.ID.GetValueOrDefault(0),
                By = item.Body.By,
                Title = item.Body.Title
            };

            var objectAsString = JsonConvert.SerializeObject(bestStoryInfo);
            cacheDB.StringSet(id.ToString(), objectAsString);

            return bestStoryInfo;
        }

        public static async Task<IEnumerable<int>> GetBestStoriesAsync()
        {
            var dataApi = new HackerNewsAPI.HackerNewsDataAPI(new Uri(HackerNewsDataAPI));
            var item = await dataApi.GetBestStoriesAsyncWithHttpMessagesAsync();

            var items = new List<int>();

            items.AddRange(item.Body.Where(e => e.GetValueOrDefault(0) > 0).Cast<int>());
            return items;
        }
    }
}
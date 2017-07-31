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
        private static ConnectionMultiplexer CacheConnection;

        private static IDatabase CacheDB = null;

        private static readonly ILog Log = LogManager.GetLogger<BestStoryEngine>();
        private static string HackerNewsDataAPI => ConfigurationManager.AppSettings["HackerNewsDataAPI"];

        static BestStoryEngine()
        {
            var cacheConnectionString = ConfigurationManager.AppSettings["CacheConnectionString"];
            CacheConnection = ConnectionMultiplexer.Connect(cacheConnectionString);
        }

        public static IEnumerable<Task<BestStoryInfo>> GetBestStoriesInfoAsync(IEnumerable<int> ids)
        {
            return ids.Select(GetBestStoryInfoAsync).ToList();
        }

        public static async Task<BestStoryInfo> GetBestStoryInfoAsync(int id)
        {
            try
            {
                if (CacheDB == null)
                    CacheDB = CacheConnection.GetDatabase();

                var keyExists = CacheDB.KeyExists(id.ToString());
                if (keyExists)
                {
                    var itemFromCache = CacheDB.StringGet(id.ToString());
                    var result = JsonConvert.DeserializeObject<BestStoryInfo>(itemFromCache);
                    return result;
                }
            }
            catch (Exception)
            {
            }

            var dataApi = new HackerNewsAPI.HackerNewsDataAPI(new Uri(HackerNewsDataAPI));

            var item = await dataApi.GetItemAsyncWithHttpMessagesAsync(id);

            var bestStoryInfo = new BestStoryInfo
            {
                Id = item.Body.ID.GetValueOrDefault(0),
                By = item.Body.By,
                Title = item.Body.Title
            };

            var objectAsString = JsonConvert.SerializeObject(bestStoryInfo);

            CacheDB?.StringSet(id.ToString(), objectAsString, null, When.Always);

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
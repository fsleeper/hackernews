using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using HackerNewsAPI;
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

        public static async Task<IEnumerable<BestStoryInfo>> GetBestStoriesInfo()
        {
            // Get the list of best stories
            var ids = await GetBestStories();

            var result = await Task.WhenAll(ids.Select(GetBestStoryInfo).ToArray());

            return result.ToList();
        }

        public static async Task<BestStoryInfo> GetBestStoryInfo(int id)
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

            var item = await dataApi.GetItemAsync(id);

            var bestStoryInfo = new BestStoryInfo
            {
                Id = item.ID.GetValueOrDefault(0),
                By = item.By,
                Title = item.Title
            };

            var objectAsString = JsonConvert.SerializeObject(bestStoryInfo);

            CacheDB?.StringSet(id.ToString(), objectAsString, null, When.Always);

            return bestStoryInfo;
        }

        public static async Task<IEnumerable<int>> GetBestStories()
        {
            var dataApi = new HackerNewsAPI.HackerNewsDataAPI(new Uri(HackerNewsDataAPI));
            var item = await dataApi.GetBestStoriesAsync();

            var items = new List<int>();

            items.AddRange(item.Where(e => e.GetValueOrDefault(0) > 0).Cast<int>());
            return items;
        }
    }
}
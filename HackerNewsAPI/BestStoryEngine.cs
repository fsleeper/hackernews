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
        private static readonly ConnectionMultiplexer CacheConnection;
        private static IDatabase _cacheDB;

        private static readonly ILog Log = LogManager.GetLogger<BestStoryEngine>();
        private static string HackerNewsDataAPI => ConfigurationManager.AppSettings["HackerNewsDataAPI"];

        /// <summary>
        /// Here I intentionally just want the error to propogate so it can be viewed or dealt with at a higher level
        /// </summary>
        static BestStoryEngine()
        {
            var cacheConnectionString = ConfigurationManager.AppSettings["CacheConnectionString"];
            CacheConnection = ConnectionMultiplexer.Connect(cacheConnectionString);
        }

        public static async Task<IEnumerable<int>> GetBestStories()
        {
            var items = new List<int>();

            try
            {
                var dataApi = new HackerNewsAPI.HackerNewsDataAPI(new Uri(HackerNewsDataAPI));
                var item = await dataApi.GetBestStoriesAsync();

                items.AddRange(item.Where(e => e.GetValueOrDefault(0) > 0).Cast<int>());
            }
            catch (Exception ex)
            {
                Log.Error($"Error attempting to read the Best Story ID List", ex);
            }

            return items;
        }

        /// <summary>
        /// This is a wrapping function to optimize access to the underlying async methods. This allows me to submit multiple look-ups at the same time versus waiting
        /// in serial fashion
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<BestStoryInfo>> GetBestStoriesInfo()
        {
            // Get the list of best stories
            var ids = await GetBestStories();

            // Request the data for the ids in a parallel fashion
            var result = await Task.WhenAll(ids.Select(GetBestStoryInfo).ToArray());

            // finally return the list
            return result.ToList();
        }

        /// <summary>
        /// This pulls the data for the passed ID.  It intentionally ignores if there was an error attempting to pull from or write to cache
        /// Also if there was a problem attempting to pull the item from the actual source, this error is ignored as well as TBD the reason, so a stub
        /// object will be created in its place
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<BestStoryInfo> GetBestStoryInfo(int id)
        {
            try
            {
                var result = GetFromCache(id);
                if (result != null)
                    return result;

                var dataApi = new HackerNewsAPI.HackerNewsDataAPI(new Uri(HackerNewsDataAPI));

                var item = await dataApi.GetItemAsync(id);

                var bestStoryInfo = new BestStoryInfo
                {
                    Id = item.ID.GetValueOrDefault(0),
                    By = item.By,
                    Title = item.Title
                };

                SaveToCache(bestStoryInfo);

                return bestStoryInfo;
            }
            catch(Exception ex)
            {
                Log.Error($"Error attempting to read information from Hacker News for the item {id}", ex);

                var bestStoryInfo = new BestStoryInfo
                {
                    Id = id,
                    By = "<unknown>",
                    Title = "<Problem encountered attempting to read informtion from Hacker News>"
                };

                return bestStoryInfo;
            }
        }

        private static void SaveToCache(BestStoryInfo bestStoryInfo)
        {
            try
            {
                var objectAsString = JsonConvert.SerializeObject(bestStoryInfo);
                _cacheDB?.StringSet(bestStoryInfo.Id.ToString(), objectAsString);
            }
            catch (Exception ex)
            {
                Log.Error($"Error attempting to save a story to the cache for story {bestStoryInfo.Id}", ex);
            }
        }

        private static BestStoryInfo GetFromCache(int id)
        {
            try
            {
                if (_cacheDB == null)
                    _cacheDB = CacheConnection.GetDatabase();

                var keyExists = _cacheDB.KeyExists(id.ToString());
                if (keyExists)
                {
                    var itemFromCache = _cacheDB.StringGet(id.ToString());
                    var result = JsonConvert.DeserializeObject<BestStoryInfo>(itemFromCache);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error attempting to read from the cache for story {id}", ex);
            }

            return null;
        }
    }
}
using HackerNewsWrapper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Logging;
using Newtonsoft.Json;

namespace HackerNewsWrapper
{
    public class HackerAPI
    {
        private static readonly object Locker = new object();

        private static readonly ILog Log = LogManager.GetLogger<HackerAPI>();

        private static readonly string DefaultHackerAPIBaseURL = "https://hacker-news.firebaseio.com/v0";

        // Allow the caller to override this, making it easy to configure in a WEB client
        private static string _hackerAPIBaseURL;

        // I honestly only care about JSON formatting, so just going to restrict it to that for sanity
        // Not going to add unless I really need
        // private static readonly List<MediaTypeFormatter> Formatters = new List<MediaTypeFormatter> { new JsonMediaTypeFormatter() };

        /// <summary>
        /// The URL to use as the base for all HackerNews calls. This supports using the default URL as well as allowing the caller to configure the uses (static, so ....)
        /// </summary>
        public static string HackerAPIBaseURL
        {
            get
            {
                string url;
                lock(Locker)
                    url = string.IsNullOrWhiteSpace(_hackerAPIBaseURL) ? DefaultHackerAPIBaseURL : _hackerAPIBaseURL;

                return url;
            }
            set
            {
                lock(Locker)
                    _hackerAPIBaseURL = value;
            }
        }

        private static HttpClient GetClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private static string GetItemClientURL(int id)
        {
            var url = $"{HackerAPIBaseURL}/item/{id}.json?print=pretty";

            Log.Debug($"Generated GetItemClientURL of {url}");

            return url;
        }

        private static string GetUserClientURL(string userId)
        {
            var url = $"{HackerAPIBaseURL}/user/{userId}.json?print=pretty";

            Log.Debug($"Generated GetUserClientURL of {url}");

            return url;
        }

        private static string GetBestStoriesURL()
        {
            var url = $"{HackerAPIBaseURL}/beststories.json?print=pretty";

            Log.Debug($"Generated GetBestStoriesURL of {url}");

            return url;
        }

        public static async Task<Item> GetItem(int id)
        {
            Log.Debug($"Request for GetItem({id})");

            Item result;
            var client = GetClient();
            var url = GetItemClientURL(id);

            using (var response = await client.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                var responseBody = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Item>(responseBody);
            }

            return result;
        }

        public static async Task<User> GetUser(string userId)
        {
            Log.Debug($"Request for GetUser({userId})");

            User result;
            var client = GetClient();
            var url = GetUserClientURL(userId);

            using (var response = await client.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                var responseBody = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<User>(responseBody);
            }

            return result;
        }

        public static async Task<IEnumerable<int>> GetBestStories()
        {
            Log.Debug("Request for GetBestStories()");

            List<int> result;
            var client = GetClient();
            var url = GetBestStoriesURL();

            using (var response = await client.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                var responseBody = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<int>>(responseBody);

                Log.Debug($"Retrieved {result.Count} records");
            }

            return result;
        }

        // https://hacker-news.firebaseio.com/v0/maxitem.json?print=pretty - returns largest item, then work backwards (wtf)
        // https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty
        // https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty
        // https://hacker-news.firebaseio.com/v0/beststories.json?print=pretty
        // https://hacker-news.firebaseio.com/v0/askstories.json?print=pretty
        // https://hacker-news.firebaseio.com/v0/showstories.json?print=pretty
        // https://hacker-news.firebaseio.com/v0/jobstories.json?print=pretty
        // https://hacker-news.firebaseio.com/v0/updates.json?print=pretty
    }
}

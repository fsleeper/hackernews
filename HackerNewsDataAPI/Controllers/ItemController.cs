using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Logging;
using Swashbuckle.Swagger.Annotations;
using HackerNewsDataAPI.Models;
using Newtonsoft.Json;
using Omu.ValueInjecter;

namespace HackerNewsDataAPI.Controllers
{
    public class ItemController : BaseController
    {
        private static readonly ILog Log = LogManager.GetLogger<ItemController>();

        [SwaggerOperation("GetItemAsync")]
        [HttpGet]
        public async Task<Item> GetItemAsync(int id)
        {
            Log.Debug($"Request for GetItemAsync({id})");

            var result = new Item();
            var client = Utilities.WebRequest.DefaultWebClient;
            var url = URL(id);

            using (var response = await client.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                var responseBody = await response.Content.ReadAsStringAsync();
                var rawResult = JsonConvert.DeserializeObject<HackerNewsItem>(responseBody);
                result.InjectFrom(rawResult);
            }

            return result;
        }

        private string URL(int id)
        {
            return URLTemplate($"/item/{id}");
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

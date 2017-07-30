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
using HackerNewsAPI.Models;
using HackerNewsDataAPI;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;

namespace HackerNewsAPI.Controllers
{
    public class BestStoriesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger<BestStoriesController>();

        // GET api/values
        [SwaggerOperation("GetBestStoriesAsync")]
        [HttpGet]
        public async Task<IEnumerable<int>> GetBestStoriesAsync()
        {
            Log.Debug("Request for GetBestStories()");

            var result = await BestStoryEngine.GetBestStoriesAsync();

            return result;
        }

        // GET api/values
        [SwaggerOperation("GetBestStoryAsync")]
        [HttpGet]
        public async Task<BestStoryInfo> GetBestStoryAsync(int id)
        {
            Log.Debug("Request for GetBestStory()");

            var result = await BestStoryEngine.GetBestStoryAsync(id);

            return result;
        }
    }
}

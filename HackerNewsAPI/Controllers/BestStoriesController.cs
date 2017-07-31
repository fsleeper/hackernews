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
        [SwaggerOperation("GetBestStories")]
        [HttpGet]
        [Route("api/GetAllStoryIds")]
        public async Task<IEnumerable<int>> GetBestStories()
        {
            Log.Debug("Request for GetBestStories()");

            var result = await BestStoryEngine.GetBestStories();

            return result;
        }

        // GET api/values
        [SwaggerOperation("GetBestStoryInfo")]
        [HttpGet]
        [Route("api/GetStory/{id}")]
        public async Task<BestStoryInfo> GetBestStoryInfo(int id)
        {
            Log.Debug("Request for GetBestStory()");

            var result = await BestStoryEngine.GetBestStoryInfo(id);

            return result;
        }

        // GET api/values
        [SwaggerOperation("GetAllBestStoriesInfo")]
        [HttpGet]
        [Route("api/GetAllStories")]
        public async Task<IEnumerable<BestStoryInfo>> GetAllBestStoriesInfo()
        {
            Log.Debug("Request for GetBestStories()");

            var result = await BestStoryEngine.GetBestStoriesInfo();

            return result;
        }
    }
}
    
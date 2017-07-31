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
    // By the nature of the wrapped requests, in the event of an error the caller will get an empty list back or a default item.

    public class BestStoriesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger<BestStoriesController>();

        /// <summary>
        /// This method retrieves the list of best story ids
        /// </summary>
        /// <returns>The list of story ids</returns>
        [SwaggerOperation("GetBestStories")]
        [HttpGet]
        [Route("api/GetAllStoryIds")]
        public async Task<IEnumerable<int>> GetBestStories()
        {
            Log.Debug("Request for GetBestStories()");

            var result = await BestStoryEngine.GetBestStories();

            return result;
        }

        /// <summary>
        /// This method retrieves the detailed information for the passed story id
        /// </summary>
        /// <returns>The detailed information</returns>
        [SwaggerOperation("GetBestStoryInfo")]
        [HttpGet]
        [Route("api/GetStory/{id}")]
        public async Task<BestStoryInfo> GetBestStoryInfo(int id)
        {
            Log.Debug("Request for GetBestStory()");

            var result = await BestStoryEngine.GetBestStoryInfo(id);

            return result;
        }

        /// <summary>
        /// This method retrieves retrieves the list of detailed story information for the best stories
        /// </summary>
        /// <returns>The list of story ids</returns>
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
    
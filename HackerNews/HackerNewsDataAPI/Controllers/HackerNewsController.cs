using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Logging;
using Swashbuckle.Swagger.Annotations;
using HackerNewsDataAPI.Models;
using HackerNewsWrapper;

namespace HackerNewsDataAPI.Controllers
{
    public class HackerNewsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger<HackerNewsController>();

        // Get the list of best stories, we need the author and the title

        // GET api/values
        [SwaggerOperation("GetAsync")]
        public async Task<IEnumerable<int>> GetAsync()
        {
            var engine = new BestStoryEngine();

            // Get the Best Story Info
            var bestStories = await engine.GetBestStories();

            return bestStories;
        }

        // GET api/values
        [SwaggerOperation("GetAll")]
        public IEnumerable<int> Get()
        {
            var engine = new BestStoryEngine();

            // Get the Best Story Info
            var bestStories = engine.GetBestStories().Result;

            // for IHttpActionResult
            // return Ok(bestStories);

            return bestStories;
        }
    }
}

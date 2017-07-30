using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Logging;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;

namespace HackerNewsDataAPI.Controllers
{
    public class BestStoriesController : BaseController
    {
        private static readonly ILog Log = LogManager.GetLogger<BestStoriesController>();

        [SwaggerOperation("GetBestStoriesAsync")]
        [HttpGet]
        public async Task<IEnumerable<int>> GetBestStoriesAsync()
        {
            Log.Debug("Request for GetBestStoriesAsync()");

            List<int> result;
            var client = Utilities.WebRequest.DefaultWebClient;
            var url = URL();

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

        private string URL()
        {
            return URLTemplate("/beststories");
        }
    }
}

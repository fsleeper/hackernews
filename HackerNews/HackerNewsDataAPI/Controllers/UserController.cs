using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Logging;
using HackerNewsDataAPI.Models;
using Newtonsoft.Json;
using Omu.ValueInjecter;
using Swashbuckle.Swagger.Annotations;

namespace HackerNewsDataAPI.Controllers
{
    public class UserController : BaseController
    {
        private static readonly ILog Log = LogManager.GetLogger<UserController>();

        [SwaggerOperation("GetUserAsync")]
        [HttpGet]
        public async Task<User> GetUserAsync(string userId)
        {
            Log.Debug($"Request for GetUserAsync({userId})");

            var result = new User();
            var client = Utilities.WebRequest.DefaultWebClient;
            var url = URL(userId);

            using (var response = await client.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                var responseBody = await response.Content.ReadAsStringAsync();
                var rawResult = JsonConvert.DeserializeObject<HackerNewsUser>(responseBody);
                result.InjectFrom(rawResult);
            }

            return result;
        }

        private string URL(string userId)
        {
            return URLTemplate($"/user/{userId}");
        }
    }
}

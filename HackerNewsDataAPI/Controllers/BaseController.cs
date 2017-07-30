using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace HackerNewsDataAPI.Controllers
{
    public class BaseController : ApiController
    {
        /// <summary>
        /// The URL to use as the base for all HackerNews calls. This supports using the default URL as well as allowing the caller to configure the uses (static, so ....)
        /// </summary>
        protected static string HackerAPIBaseURL => ConfigurationManager.AppSettings["HackerNewsBaseURL"];

        protected virtual string URLTemplate(string replacementSection)
        {
            var url = $"{HackerAPIBaseURL}/{replacementSection}.json?print=pretty";

            return url;
        }
    }
}

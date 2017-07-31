using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace HackerNewsWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var url = ConfigurationManager.AppSettings["HackerNewsAPI"];
            var request = new HackerNewsAPI(new Uri(url));
            var data = request.GetAllBestStoriesInfo();
            return View(data);
        }
    }
}

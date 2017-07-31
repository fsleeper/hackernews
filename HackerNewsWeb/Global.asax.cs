using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HackerNewsWeb
{
    public class MvcApplication : HttpApplication
    {
        // Yes, calling log4net.Config.XmlConfigurator.Configure(); is lame.  It's a bug in the current log4net universal with common logging

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}

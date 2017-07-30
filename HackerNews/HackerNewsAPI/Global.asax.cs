using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace HackerNewsAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        // Yes, calling log4net.Config.XmlConfigurator.Configure(); is lame.  It's a bug in the current log4net universal with common logging

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_BeginRequest()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}

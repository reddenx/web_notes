using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using WebNotesSite.Data;
using WebNotesSite.Framework;

namespace WebNotesSite
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new ApiCookieAuthorizeAttribute());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new MvcCookieAuthorizeAttribute());

            var config = new SiteConfiguration();
            CachedDataAccess.Initialize(config.TypecacheStorageDirectory);
        }
    }
}
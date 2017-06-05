using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebNotesSite.Framework;

namespace WebNotesSite
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var jsonFormatter = config.Formatters.JsonFormatter;
            if (jsonFormatter != null)
            {
                config.Formatters.Clear();
                config.Formatters.Add(jsonFormatter);
            }

            // Web API routes
            config.MapHttpAttributeRoutes();

            //security

            //GlobalConfiguration.Configuration.Filters.Add(new ApiCookieAuthorizeAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new CustomAuthenticationAttribute());
        }
    }
}

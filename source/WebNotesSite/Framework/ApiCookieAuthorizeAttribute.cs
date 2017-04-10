using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebNotesSite.Framework
{
    public class ApiCookieAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var cookies = actionContext.Request.Headers.GetCookies();

            if (AuthorizationHelper.AuthorizeUserWebapi(cookies))
            {
                return base.IsAuthorized(actionContext);
            }
            else
            {
                return false;
            }
        }
    }
}
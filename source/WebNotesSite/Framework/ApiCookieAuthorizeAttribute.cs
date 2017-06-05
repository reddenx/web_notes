using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebNotesSite.Framework
{
    [Obsolete]
    public class ApiCookieAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var cookies = actionContext.Request.Headers.GetCookies();

            return AuthorizationHelper.AuthorizeUserWebapi(cookies);
        }
    }
}
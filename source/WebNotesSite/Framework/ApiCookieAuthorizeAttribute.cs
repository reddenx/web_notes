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
            //var user = AuthorizationHelper.AuthorizeUserFromContext(actionContext.Request);
            var cookies = actionContext.Request.Headers.GetCookies();
            var user = AuthorizationHelper.AuthorizeUserFromApiContext(cookies);

            if (user != null)
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
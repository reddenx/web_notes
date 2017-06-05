using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Framework
{
    [Obsolete]
    public class MvcCookieAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizationHelper.AuthorizeUserMvc(httpContext.Cache, httpContext.Request);
        }
    }
}
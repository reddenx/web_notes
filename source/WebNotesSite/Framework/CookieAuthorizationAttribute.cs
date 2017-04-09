using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Framework
{
    public class CookieAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = AuthorizationHelper.AuthorizeUserFromContext(httpContext.Cache, httpContext.Request);

            if (user != null)
            {
                return base.AuthorizeCore(httpContext);
            }
            else
            {
                return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNotesSite.Data;
using WebNotesSite.Framework;
using WebNotesSite.Models.Persistence;

namespace WebNotesSite.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        public HomeController()
        { }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [Route("Login/{authToken:guid}")]
        [AllowAnonymous]
        public RedirectResult LoggingIn(Guid authToken)
        {
            var cookie = AuthorizationHelper.GetAuthCookieForToken(HttpContext.Cache, authToken);
            Response.Cookies.Add(cookie);
            
            return Redirect("/Account");
        }

        [HttpGet]
        [Route("Account")]
        public ViewResult Account()
        {
            return View("Account");
        }

        [HttpGet]
        [Route("Note")]
        public ViewResult ManageNotes()
        {
            return View("ManageNotes");
        }

        [HttpGet]
        [Route("Note/{noteId:guid}")]
        public ViewResult TakeNotes(Guid noteId)
        {
            return View("TakeNotes");
        }
    }
}
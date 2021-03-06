﻿using System;
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
    [Obsolete]
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

        //right now this does a roundabout thing and gets the token via an api call, then uses this to plop it into the cookies for the browser
        [HttpGet]
        [Route("Login/{authToken:guid}")]
        [AllowAnonymous]
        public RedirectResult HACK_SetupLoginToken(Guid authToken)
        {
            var cookie = AuthorizationHelper.GetAuthCookieForToken(HttpContext.Cache, authToken);
            if (cookie != null)
            {
                Response.Cookies.Add(cookie);
                return Redirect("/Account");
            }

            return Redirect("/");
        }

        [HttpGet]
        [Route("Logout")]
        public RedirectResult RemoveLoginToken()
        {
            var cookie = AuthorizationHelper.GetUnAuthCookie();
            Response.Cookies.Add(cookie);
            return Redirect("/");
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
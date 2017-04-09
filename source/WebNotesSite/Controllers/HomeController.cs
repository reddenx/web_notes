using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebNotesSite.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        public HomeController()
        { }

        [HttpGet]
        [Route("")]
        public ViewResult Index()
        {
            return View();
        }
    }
}
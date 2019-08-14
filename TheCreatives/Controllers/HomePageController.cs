using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheCreatives.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult HomePage()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheCreatives.Controllers
{
    public class HomeController : Controller
    {

        /*Patients View*/
        public ActionResult Index()
        {
            if(User.Identity.Name == "clerk@clerk.com" || User.Identity.Name == "doc@doc.com")
            {
                return RedirectToAction("Index2");
            }
          return   RedirectToAction("HomePage","HomePage");
        }


        //Clerk and Doctors View
        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simit_Saray.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menyu()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Your Blog page.";

            return View();
        }
        public ActionResult Gallery()
        {
            ViewBag.Message = "Your Gallery page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your Contact page.";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your About page.";

            return View();
        }
        public ActionResult BlogItem()
        {
            ViewBag.Message = "Your About page.";

            return View();
        }

    }
}
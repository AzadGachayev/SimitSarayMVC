using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simit_Saray.Areas.SimitAdmin.Controllers
{
    public class HomeController : Controller
    {
        // GET: SimitAdmin/Home
        public ActionResult Index()
        {
            if (Session["activeAdmin"] == null)
            {
                return RedirectToAction("Login", "AdminAccount");
            }

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simit_Saray.Areas.SimitAdmin.Controllers
{
    [AuthorizationFilterController]
    public class AdminHomeController : Controller
    {
        // GET: SimitAdmin/Home
        public ActionResult Index()
        {
            

            return View();
        }
    }
}
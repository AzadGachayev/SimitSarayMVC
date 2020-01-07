 using Simit_Saray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simit_Saray.Areas.SimitAdmin.Controllers
{

    
    public class AdminAccountController : Controller
    {
        // GET: SimitAdmin/AdminAccountB
        private SimitSarayDB DB = new SimitSarayDB();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(Admin Adm)
        {
            Admin selectAdmin = DB.Admin.FirstOrDefault(slAdmin => slAdmin.Email == Adm.Email);

            if (ModelState.IsValid)
            {
                if (selectAdmin != null)
                {
                    if (selectAdmin.Password == Adm.Password)
                    {
                        Session["activeAdmin"] = selectAdmin;
                        return RedirectToAction("Index", "AdminHome");
                    }
                    else
                    {
                        ViewBag.Error = "Şifrə düzgün deyil";

                    }
                }
                else
                {
                    ViewBag.Error = "Email düzgün deyil!";
                }

            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["activeAdmin"] = null;
            return RedirectToAction("Login", " AdminAccount");
        }
    }
}
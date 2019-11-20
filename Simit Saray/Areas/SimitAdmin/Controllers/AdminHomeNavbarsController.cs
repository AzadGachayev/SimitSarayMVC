using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Simit_Saray.Models;

namespace Simit_Saray.Areas.SimitAdmin.Controllers
{
    [AuthorizationFilterController]

    public class AdminHomeNavbarsController : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();

        // GET: SimitAdmin/AdminHomeNavbars
        public ActionResult Index()
        {
            return View(db.HomeNavbar.ToList());
        }

        // GET: SimitAdmin/AdminHomeNavbars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeNavbar homeNavbar = db.HomeNavbar.Find(id);
            if (homeNavbar == null)
            {
                return HttpNotFound();
            }
            return View(homeNavbar);
        }

        // GET: SimitAdmin/AdminHomeNavbars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimitAdmin/AdminHomeNavbars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BackgroundImage")] HomeNavbar homeNavbar)
        {
            if (ModelState.IsValid)
            {
                db.HomeNavbar.Add(homeNavbar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homeNavbar);
        }

        // GET: SimitAdmin/AdminHomeNavbars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeNavbar homeNavbar = db.HomeNavbar.Find(id);
            if (homeNavbar == null)
            {
                return HttpNotFound();
            }
            return View(homeNavbar);
        }

        // POST: SimitAdmin/AdminHomeNavbars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BackgroundImage")] HomeNavbar homeNavbar, int? id, HttpPostedFileBase BackgroundImage)
        {
            if (ModelState.IsValid)
            {
                HomeNavbar selectedNavbar = db.HomeNavbar.SingleOrDefault(nvb => nvb.ID == id);
                if (BackgroundImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedNavbar.BackgroundImage)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedNavbar.BackgroundImage));
                    }

                    WebImage img = new WebImage(BackgroundImage.InputStream);
                    FileInfo imgInfo = new FileInfo(BackgroundImage.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/Home/" + FileName);
                    selectedNavbar.BackgroundImage = "/Public/uploads/Home/" + FileName;

                }
            
                selectedNavbar.Name = homeNavbar.Name;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homeNavbar);
        }

        // GET: SimitAdmin/AdminHomeNavbars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeNavbar homeNavbar = db.HomeNavbar.Find(id);
            if (homeNavbar == null)
            {
                return HttpNotFound();
            }
            return View(homeNavbar);
        }

        // POST: SimitAdmin/AdminHomeNavbars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeNavbar homeNavbar = db.HomeNavbar.Find(id);
            db.HomeNavbar.Remove(homeNavbar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

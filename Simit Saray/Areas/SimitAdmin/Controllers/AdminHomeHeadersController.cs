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

    public class AdminHomeHeadersController : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();



        // GET: SimitAdmin/AdminHomeHeaders
        public ActionResult Index()
        {
            return View(db.HomeHeader.ToList());
        }

        // GET: SimitAdmin/AdminHomeHeaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeHeader homeHeader = db.HomeHeader.Find(id);
            if (homeHeader == null)
            {
                return HttpNotFound();
            }
            return View(homeHeader);
        }

        // GET: SimitAdmin/AdminHomeHeaders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimitAdmin/AdminHomeHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Head,Description,Logo,BackgroundImage")] HomeHeader homeHeader)
        {
            if (ModelState.IsValid)
            {
                db.HomeHeader.Add(homeHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homeHeader);
        }

        // GET: SimitAdmin/AdminHomeHeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeHeader homeHeader = db.HomeHeader.Find(id);
            if (homeHeader == null)
            {
                return HttpNotFound();
            }
            return View(homeHeader);
        }

        // POST: SimitAdmin/AdminHomeHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Head,Description,Logo,BackgroundImage")] HomeHeader homeHeader, int? id, HttpPostedFileBase Photo,HttpPostedFileBase Logo)
        {
            if (ModelState.IsValid)
            {
                HomeHeader selectedHome  = db.HomeHeader.SingleOrDefault(hm => hm.ID == id);
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedHome.BackgroundImage)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedHome.BackgroundImage));
                    }
                  
                        WebImage img = new WebImage(Photo.InputStream);
                        FileInfo imgInfo = new FileInfo(Photo.FileName);
                        string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Save("~/Public/uploads/Home/" + FileName);
                        selectedHome.BackgroundImage = "/Public/uploads/Home/" + FileName;
                  
                }
                if (Logo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedHome.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedHome.Logo));
                    }

                    WebImage img = new WebImage(Logo.InputStream);
                    FileInfo imgInfo = new FileInfo(Logo.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/Home/" + FileName);
                    selectedHome.Logo = "/Public/uploads/Home/" + FileName;
                }
                selectedHome.Description = homeHeader.Description;
                selectedHome.Head = homeHeader.Head;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homeHeader);
        }

        // GET: SimitAdmin/AdminHomeHeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeHeader homeHeader = db.HomeHeader.Find(id);
            if (homeHeader == null)
            {
                return HttpNotFound();
            }
            return View(homeHeader);
        }

        // POST: SimitAdmin/AdminHomeHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeHeader homeHeader = db.HomeHeader.Find(id);
            db.HomeHeader.Remove(homeHeader);
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

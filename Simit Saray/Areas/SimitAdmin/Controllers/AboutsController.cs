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

    public class AboutsController : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();

        
        // GET: SimitAdmin/Abouts
        public ActionResult Index()
        {
            return View(db.About.ToList());
        }

        // GET: SimitAdmin/Abouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.About.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // GET: SimitAdmin/Abouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimitAdmin/Abouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Header,Photo,BackgroundImage,Description,WorkTime")] About about)
        {
            if (ModelState.IsValid)
            {
                db.About.Add(about);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(about);
        }

        // GET: SimitAdmin/Abouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.About.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: SimitAdmin/Abouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Header,Photo,BackgroundImage,Description,WorkTime")] About about,int? id,HttpPostedFileBase Photo, HttpPostedFileBase BackgroundImage)
        {
            if (ModelState.IsValid)
            {
                About selectedAbout = db.About.SingleOrDefault(ab => ab.ID == id);
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedAbout.Photo)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedAbout.Photo));
                    }
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo imgInfo = new FileInfo(Photo.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/aboutPhoto/" + FileName);
                    selectedAbout.Photo = "/Public/uploads/aboutPhoto/" + FileName;
                }
                if (BackgroundImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedAbout.BackgroundImage)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedAbout.BackgroundImage));
                    }
                    WebImage img = new WebImage(BackgroundImage.InputStream);
                    FileInfo imgInfo = new FileInfo(BackgroundImage.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/aboutPhoto/" + FileName);
                    selectedAbout.BackgroundImage = "/Public/uploads/aboutPhoto/" + FileName;
                }
                selectedAbout.WorkTime = about.WorkTime;
                selectedAbout.Description = about.Description;
                selectedAbout.Header = about.Header;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
        }

        // GET: SimitAdmin/Abouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.About.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: SimitAdmin/Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            About about = db.About.Find(id);
            db.About.Remove(about);
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

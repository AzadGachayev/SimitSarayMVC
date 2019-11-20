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

    public class AdminGallery2Controller : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();



        // GET: SimitAdmin/AdminGallery2
        public ActionResult Index()
        {
            return View(db.Gallery2.ToList());
        }

        // GET: SimitAdmin/AdminGallery2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery2 gallery2 = db.Gallery2.Find(id);
            if (gallery2 == null)
            {
                return HttpNotFound();
            }
            return View(gallery2);
        }

        // GET: SimitAdmin/AdminGallery2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimitAdmin/AdminGallery2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Photo")] Gallery2 gallery2, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                   

                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo imgInfo = new FileInfo(Photo.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/Gallery/" + FileName);
                    gallery2.Photo = "/Public/uploads/Gallery/" + FileName;
                }
                db.Gallery2.Add(gallery2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery2);
        }

        // GET: SimitAdmin/AdminGallery2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery2 gallery2 = db.Gallery2.Find(id);
            if (gallery2 == null)
            {
                return HttpNotFound();
            }
            return View(gallery2);
        }

        // POST: SimitAdmin/AdminGallery2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Photo")] Gallery2 gallery2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gallery2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery2);
        }

        // GET: SimitAdmin/AdminGallery2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery2 gallery2 = db.Gallery2.Find(id);
            if (gallery2 == null)
            {
                return HttpNotFound();
            }
            return View(gallery2);
        }

        // POST: SimitAdmin/AdminGallery2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery2 gallery2 = db.Gallery2.Find(id);
            db.Gallery2.Remove(gallery2);
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
